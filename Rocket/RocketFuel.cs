using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFuel : MonoBehaviour
{
    [SerializeField] private float maxFuel = 500f;
    [SerializeField] private float currentFuel;
    [SerializeField] private float fuelDepletionRate = 100f;

    void Awake()
    {
        currentFuel = maxFuel;
    }


    public void SpendFuel() {
        if (currentFuel <= 0) {
            BroadcastMessage("FuelDepleted");
            return;
        }

        float newFuel = Mathf.Lerp(currentFuel, currentFuel - fuelDepletionRate, Time.deltaTime);
        currentFuel = Mathf.Max(newFuel, 0f);
    }
}
