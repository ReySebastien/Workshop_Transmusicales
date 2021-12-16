using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    private SpawnerManager spawnerManager;
    private TileSpawner tileSpawner;

    private void Start()
    {
        spawnerManager = gameObject.GetComponentInParent<SpawnerManager>();
        tileSpawner = gameObject.GetComponentInParent<TileSpawner>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -1) * Time.deltaTime * spawnerManager.GetCurrentSpeed();
    }

    private void FixedUpdate()
    {
        if (transform.position.z <= -30)
        {
            tileSpawner.SpawnRandomTile();
            tileSpawner.RemoveInList();
            Destroy(gameObject);
        }
    }
}
