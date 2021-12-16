using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Speed")]
    [Tooltip("Speed the tile try to equal")]
    [SerializeField]
    private float baseSpeed;

    [Tooltip("Current speed of the tile")]
    [SerializeField]
    private float currentSpeed;

    [Header("Boost")]
    [Tooltip("True when boost occur")]
    [SerializeField]
    private bool boost;

    [SerializeField]
    [Range(1, 2)]
    [Tooltip("Speed the tile gain in percentage after a Boost/Speed Door")]
    private float boostSpeed;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Reduction of the speed in percentage when current speed is greater")]
    private float speedReduction;

    [Header("PowerUp")]
    [Tooltip("True if a speed door is trigger")]
    [SerializeField]
    private bool speedDoor;

    private void Update()
    {
        //If a Boost happen
        if (boost)
        {
            boost = false;
            currentSpeed *= boostSpeed;
        }

        //If a Speed Door is trigger, baseSpeed is multiplied and currentSpeed is even more multiplied
        if (speedDoor)
        {
            speedDoor = false;
            baseSpeed *= 1.1f;
            currentSpeed *= boostSpeed;
        }

        //Tile's speed try to equal the base Speed
        if (currentSpeed > baseSpeed)
        {
            currentSpeed -= currentSpeed * speedReduction * Time.deltaTime;
        }

        if(currentSpeed > 400)
        {
            currentSpeed = 400;
        }

        if(baseSpeed > 333)
        {
            baseSpeed = 333;
        }
    }

    //When a SpeedDoor is trigger
    public void SetSpeedDoor(bool value)
    {
        speedDoor = value;
    }

    public void SetBoost(bool value)
    {
        boost = value;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
