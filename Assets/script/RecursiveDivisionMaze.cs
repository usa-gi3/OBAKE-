using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDivisionMaze : MonoBehaviour
{
    public int width = 51;
    public int height = 51;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject goalPrefab;

    public Transform player;

    private int[,] maze;

    void Start()
    {
        // 有効な経路が生成されるまで繰り返す
        while (true)
        {
            GenerateMaze();

            if (CheckPath())
            {
                break;
            }
        }

        DrawMaze();

        if (player != null)
        {
            // 左下入口の内側スタート位置
            player.position = new Vector3(1, 1, 1);
        }

        if (goalPrefab != null)
        {
            // 右上出口の内側位置
            Vector3 goalPos = new Vector3(width - 2, 0.5f, height - 2);
            Instantiate(goalPrefab, goalPos, Quaternion.identity);
        }
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // 全て通路で初期化
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                maze[x, y] = 0;

        // 外周を壁にする
        for (int x = 0; x < width; x++)
        {
            maze[x, 0] = 1;
            maze[x, height - 1] = 1;
        }
        for (int y = 0; y < height; y++)
        {
            maze[0, y] = 1;
            maze[width - 1, y] = 1;
        }

        // 左下入口（幅2マス）
        maze[0, 1] = 0;
        maze[0, 2] = 0;
        maze[1, 1] = 0;
        maze[1, 2] = 0;

        // 右上出口（幅2マス）
        maze[width - 1, height - 2] = 0;
        maze[width - 1, height - 3] = 0;
        maze[width - 2, height - 2] = 0;
        maze[width - 2, height - 3] = 0;

        // 再帰分割で迷路生成
        Divide(1, 1, width - 2, height - 2);
    }

    bool CheckPath()
    {
        bool[,] visited = new bool[width, height];
        Queue<Vector2Int> q = new Queue<Vector2Int>();

        Vector2Int start = new Vector2Int(1, 1);
        Vector2Int goal = new Vector2Int(width - 2, height - 2);

        q.Enqueue(start);
        visited[start.x, start.y] = true;

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        while (q.Count > 0)
        {
            Vector2Int now = q.Dequeue();

            if (now == goal)
                return true;

            for (int i = 0; i < 4; i++)
            {
                int nx = now.x + dx[i];
                int ny = now.y + dy[i];

                if (nx < 0 || ny < 0 || nx >= width || ny >= height)
                    continue;

                if (maze[nx, ny] == 1)
                    continue;

                if (visited[nx, ny])
                    continue;

                visited[nx, ny] = true;
                q.Enqueue(new Vector2Int(nx, ny));
            }
        }

        return false;
    }

    void Divide(int x, int y, int w, int h)
    {
        if (w < 5 || h < 5) return; // 道幅を広くするため

        bool horizontal = (w < h) || (w == h && Random.value > 0.5f);

        if (horizontal)
        {
            int wallY = y + 2 * Random.Range(2, (h - 2) / 2);
            int holeX = x + 2 * Random.Range(1, w / 2 - 1) + 1;

            for (int i = x; i < x + w; i++)
            {
                if (i == holeX || i == holeX + 1) continue; // 通路幅2
                maze[i, wallY] = 1;
                maze[i, wallY + 1] = 1;
            }

            Divide(x, y, w, wallY - y);
            Divide(x, wallY + 2, w, y + h - wallY - 2);
        }
        else
        {
            int wallX = x + 2 * Random.Range(2, (w - 2) / 2);
            int holeY = y + 2 * Random.Range(1, h / 2 - 1) + 1;

            for (int i = y; i < y + h; i++)
            {
                if (i == holeY || i == holeY + 1) continue; // 通路幅2
                maze[wallX, i] = 1;
                maze[wallX + 1, i] = 1;
            }

            Divide(x, y, wallX - x, h);
            Divide(wallX + 2, y, x + w - wallX - 2, h);
        }
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);

                if (maze[x, y] == 1)
                {
                    Instantiate(wallPrefab, pos + Vector3.up, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(floorPrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}
