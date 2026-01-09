using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labyrinth : MonoBehaviour
{
    //奇数にする
    public int max_y; //迷路の縦幅
    public int max_x; //迷路の横幅
    int z; //迷路の縦の番号
    int x; //迷路の横の番号
    int r; //乱数
    public GameObject[] itemPrefabs; // 置きたい全種類
    public int maxSpawnCount = 10;   // 最大10個
    public GameObject wallPrefab;//壁の設定
    public GameObject goalPrefab;//ゴールの設定
    GameObject wallgo; //壁のゲームオブジェクト

    void Start()
    {
        int[,] field = new int[max_y, max_x]; //迷路　0＝通路、1＝壁

        //通路の生成
        for (z = 0; z < max_y; z = z + 1) //迷路の縦幅の分だけループ
        {
            for (x = 0; x < max_x; x = x + 1) //迷路の横幅の分だけループ
            {
                field[z, x] = 0;//全て通路にしてみる
            }
        }

        //周囲の壁を生成（横）
        for (x = 0; x < max_x; x = x + 1) //迷路の横幅の分だけループ
        {
            field[0, x] = 1;//一番上
            field[max_y - 1, x] = 1;//一番下
        }

        //周囲の壁の生成（縦）
        for (z = 0; z < max_y; z = z + 1) //迷路の縦幅の分だけループ
        {
            field[z, 0] = 1;//左端
            field[z, max_x - 1] = 1;//右端
        }

        //壁の生成
        // 外周を作ったあと、内側に柱を立てて倒していく
        z = 2; //最初の柱の列（外から２マス目）

        for (x = 2; x < max_x - 1; x = x + 2) // 2マスごとに柱を立てる
        {
            r = Random.Range(1, 13); //棒を倒す方向を乱数で決める
            field[z, x] = 1; //柱を立てる

            //上
            if (r <= 3)
            {
                if (field[z - 1, x] == 0) //上に柱がなければ
                {
                    field[z - 1, x] = 1; //上に倒す
                }
                else if (field[z - 1, x] == 1) //上に柱があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す
                }
            }

            //下
            if (r >= 4 && r <= 6)
            {
                if (field[z + 1, x] == 0) //下に柱がなければ
                {
                    field[z + 1, x] = 1; //下に倒す
                }
                else if (field[z + 1, x] == 1) //下に柱があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す
                }
            }

            //左
            if (r >= 7 && r <= 9)
            {
                if (field[z, x - 1] == 0) //左に柱がなければ
                {
                    field[z, x - 1] = 1; //左に倒す
                }
                else if (field[z, x - 1] == 1) //左に柱があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す
                }
            }

            //右
            if (r >= 10)
            {
                if (field[z, x + 1] == 0) //右に柱がなければ
                {
                    field[z, x + 1] = 1; //右に倒す
                }
                else if (field[z, x + 1] == 1) //右に柱があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す
                }
            }
        }

        //残りの壁の生成
        for (z = 4; z < max_y - 1; z = z + 2)
        {
            for (x = 2; x < max_x - 1; x = x + 2)
            {
                r = Random.Range(1, 13); //棒を倒す方向を乱数で決める
                field[z, x] = 1; //柱を立てる

                //下
                if (r <= 4)
                {
                    if (field[z + 1, x] == 0) //下に柱がなければ
                    {
                        field[z + 1, x] = 1; //下に棒を倒す
                    }
                    else if (field[z + 1, x] == 1) //下に柱があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す
                    }
                }

                //左
                if (r >= 5 && r <= 8) //rが5から8のとき
                {
                    if (field[z, x - 1] == 0) //左に柱がなければ
                    {
                        field[z, x - 1] = 1; //左に倒す
                    }
                    else if (field[z, x - 1] == 1) //左に柱があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す
                    }
                }

                //右
                if (r >= 9)
                {
                    if (field[z, x + 1] == 0) //右に柱がなければ
                    {
                        field[z, x + 1] = 1; //右に倒す
                    }
                    else if (field[z, x + 1] == 1) //右に柱があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す
                    }
                }
            }
        }

        field[0, 1] = 0; //上の壁をなくしてスタート地点を作る
        field[max_y - 1, max_x - 2] = 0; //下の壁をなくしてゴール地点を作る
        Vector3 goalPos = new Vector3((max_x - 2) * 5, 10 / 2f, (max_y - 1) * 5);
        Instantiate(goalPrefab, goalPos, Quaternion.identity);

        //壁の配置
        for (z = 0; z < max_y; z = z + 1) //迷路の縦の分だけループ
        {
            for (x = 0; x < max_x; x = x + 1) //迷路の横幅の分だけループ
            {
                if (field[z, x] == 0) //通路なら
                {
                    //何も配置しない
                }
                else if (field[z, x] == 1) //壁なら
                {
                    //壁を等間隔で並べる
                    wallgo = Instantiate(wallPrefab, new Vector3(5.0f * x, 5.0f, 5.0f * z), Quaternion.identity);//壁を設置
                }
            }
        }
        List<Vector2Int> roadList = new List<Vector2Int>();

        for (z = 0; z < max_y; z++)
        {
            for (x = 0; x < max_x; x++)
            {
                if (field[z, x] == 0)
                {
                    roadList.Add(new Vector2Int(x, z));
                }
            }
        }

        int spawnCount = Mathf.Min(maxSpawnCount, roadList.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int posIndex = Random.Range(0, roadList.Count);
            Vector2Int pos = roadList[posIndex];
            roadList.RemoveAt(posIndex);

            // 全種類からランダム
            GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            Vector3 spawnPos = new Vector3(
                pos.x * 5.0f,
                2.5f,
                pos.y * 5.0f
            );

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

}

