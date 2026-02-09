using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goalobject : MonoBehaviour
{
    public float checkRadius = 0.5f;
    public LayerMask playerLayer;
    public string nextSceneName;
    public StarterAssetsInputs inputs;

    bool isChanged = false;

    void Update()
    {
        if (isChanged) return;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            checkRadius,
            playerLayer
        );

        if (hits.Length > 0)
        {
            isChanged = true;
            inputs.SetUIMode(true);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
