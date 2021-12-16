using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Header("Tile")]
    [Tooltip("Number of tiles that spawn at the start of the scene")]
    [SerializeField]
    private int numberOfTile;
    
    [SerializeField]
    [Tooltip("List of base Tile that will spawn randomly")]
    private GameObject[] tiles;

    [SerializeField]
    [Tooltip("List of prefabs that will spawn randomly")]
    private GameObject[] specialTiles;

    [SerializeField]
    [Tooltip("List of prefabs that will spawn randomly when flying")]
    private GameObject[] specialFlyingTiles;

    [SerializeField]
    [Tooltip("List of current spawned Tile")]
    private List<GameObject> tileList;

    [Tooltip("Number of Tile spawned before a special one is spawn")]
    private int spawnedTile;

    [Tooltip("Number of Tile before a special one spawn")]
    private int specialTile;

    [SerializeField]
    [Tooltip("Numbers of normal tile min that can spawn before a special one")]
    private int specialTileMin;

    [SerializeField]
    [Tooltip("Numbers of normal tile max that can spawn before a special one")]
    private int specialTileMax;

    private int randomStartingTile;

    private PlayerMovement playerScript;

    //Create a number of tile at the start of the scene
    void Start()
    {
        randomStartingTile = Random.Range(15, 25);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        specialTile = Random.Range(specialTileMin, specialTileMax);
        for (int i = 0; i < numberOfTile; i++)
        {
            SpawnTile(i);
        }
    }

    //Spawn tiles at the beginning 
    public void SpawnTile(int number)
    {
        GameObject tile;

        if (number > 10 && number == randomStartingTile)
        {
            if (playerScript.GetCurrentMovementType() == "Fly")
            {
                tile = Instantiate(specialFlyingTiles[Random.Range(0, specialFlyingTiles.Length)], new Vector3(transform.position.x, transform.position.y, number * 10), Quaternion.identity, gameObject.transform);

            }

            else
            {
                tile = Instantiate(specialTiles[Random.Range(0, specialTiles.Length)], new Vector3(transform.position.x, transform.position.y, number * 10), Quaternion.identity, gameObject.transform);

            }
            randomStartingTile += Random.Range(15, 25);
        }

        else
        {
            tile = Instantiate(tiles[Random.Range(0, tiles.Length)], new Vector3(transform.position.x, transform.position.y, number * 10), Quaternion.identity, gameObject.transform);
        }

        tileList.Add(tile);
    }

    //Spawn a random tile from the list
    public void SpawnRandomTile()
    {
        if(spawnedTile == specialTile)
        {
            GameObject tile;
            if (playerScript.GetCurrentMovementType() == "Fly")
            {
                tile = Instantiate(specialFlyingTiles[Random.Range(0, specialFlyingTiles.Length)], tileList[tileList.Count - 1].GetComponent<Transform>().position + new Vector3(0, 0, 10), Quaternion.identity, gameObject.transform);
            }

            else
            {
                tile = Instantiate(specialTiles[Random.Range(0, specialTiles.Length)], tileList[tileList.Count - 1].GetComponent<Transform>().position + new Vector3(0, 0, 10), Quaternion.identity, gameObject.transform);
            }

            tileList.Add(tile);
            specialTile = Random.Range(specialTileMin, specialTileMax);
            spawnedTile = 0;
        }

        else
        {
            //Pick a random Tile, Get the position of the last Tile in the list add an offset of 10 in the z axis, 
            GameObject tile = Instantiate(tiles[Random.Range(0, tiles.Length)], tileList[tileList.Count - 1].GetComponent<Transform>().position + new Vector3(0, 0, 10), Quaternion.identity, gameObject.transform);
            tileList.Add(tile);
            spawnedTile++;
        }
        
    }

    //Remove the first gameObject in the list
    public void RemoveInList()
    {
        tileList.RemoveAt(0);
    }

    
}
