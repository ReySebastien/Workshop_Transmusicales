using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSlide : MonoBehaviour
{
    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(5, 100), transform.position.z);
    }
}
