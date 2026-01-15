/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SojaExile
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        [Header("Ink")]
        public InkController inkController;
        public TextAsset inkJSON;
        public string knotName = "door_confirm";

        public string nextSceneName;

        void Start()
        {
            open = false;
            inkController.onInkResult += OnInkResult;
        }

        void OnDestroy()
        {
            inkController.onInkResult -= OnInkResult;
        }

        void OnMouseOver()
        {
            if (!Player) return;

            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist >= 15) return;

            if (!open && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(OpenAndAsk());
            }
        }

        IEnumerator OpenAndAsk()
        {
            openandclose.Play("Opening");
            open = true;

            yield return new WaitForSeconds(0.5f);

            // InkŠJŽn
            inkController.StartKnot(inkJSON, knotName);
        }

        void OnInkResult(string result)
        {
            if (result == "YES")
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (result == "NO")
            {
                StartCoroutine(Closing());
            }
        }

        IEnumerator Closing()
        {
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
*/