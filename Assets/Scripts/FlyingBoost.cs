using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoost : MonoBehaviour
{
    private SpawnerManager spawnerManager;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(5, 100), transform.position.z);
    }

    private void Start()
    {
        spawnerManager = GetComponentInParent<SpawnerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnerManager.SetSpeedDoor(true);
    }

}
