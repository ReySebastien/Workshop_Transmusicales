using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDoor : MonoBehaviour
{
    private SpawnerManager spawnerManager;

    private void Start()
    {
        spawnerManager = GetComponentInParent<SpawnerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnerManager.SetSpeedDoor(true);
    }
}
