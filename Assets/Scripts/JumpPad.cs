using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("My Components")]
    [SerializeField]
    private CapsuleCollider myCapsuleCollider;

    [SerializeField]
    private MeshRenderer myMeshRenderer;

    private PlayerMovement playerScript;

    private Color _activeColor = new Color32(0, 136, 255, 255);
    private Color _inactiveColor = new Color32(0, 177, 255, 255);


    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //StartCoroutine("JumpBoost");
    }

    /*IEnumerator JumpBoost()
    {
        while (true) { 
            yield return new WaitForSeconds(1f);

            myCapsuleCollider.enabled = true;
            myMeshRenderer.material.SetColor("_Color", _inactiveColor);


            yield return new WaitForSeconds(0.15f);

            myCapsuleCollider.enabled = false;
            myMeshRenderer.material.SetColor("_Color", _activeColor);

        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") { 
            myCapsuleCollider.enabled = false;
            Debug.Log("JumpBoost");
            playerScript.SetJumpPad(true);
        }

    }

}
