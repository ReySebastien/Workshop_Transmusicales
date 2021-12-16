using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Controls");
    }

   IEnumerator Controls()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Hub");
    }
}
