using System.Collections;
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
        public Rigidbody targetRb;   // 動きを止めたいオブジェクト
        RigidbodyConstraints defaultConstraints;


        [Header("Ink")]
        public Inkcontroller3D inkController;
        public TextAsset inkJSON;
        public string knotName = "door_confirm";

        public string nextSceneName;

        void Start()
        {
            open = false;
            inkController.onInkResult += OnInkResult;

            if (targetRb != null)
            {
                defaultConstraints = targetRb.constraints;
            }
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

            LockMove();

            inkController.StartKnot(inkJSON, knotName);
        }


        void OnInkResult(string result)
        {
            UnlockMove();

            if (result == "YES")
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (result == "NO")
            {
                StartCoroutine(Closing());
            }
        }

        void LockMove()
        {
            if (targetRb == null) return;

            targetRb.constraints =
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionZ;
        }

        void UnlockMove()
        {
            if (targetRb == null) return;

            targetRb.constraints = defaultConstraints;
        }

        IEnumerator Closing()
        {
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}