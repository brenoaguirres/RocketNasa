using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFuel : MonoBehaviour
{
    [SerializeField] private float maxFuel = 500f;
    [SerializeField] private float currentFuel;
    [SerializeField] private float fuelDepletionRate = 100f;
    [SerializeField] private float delayFuel = 50f;
    private bool extraTank = true;

    void Awake()
    {
        currentFuel = maxFuel;
    }


    public bool SpendFuel() {
        if (currentFuel <= 0) {
            return false;
        }

        float newFuel = Mathf.Lerp(currentFuel, currentFuel - fuelDepletionRate, Time.deltaTime);
        currentFuel = Mathf.Max(newFuel, 0f);
        return true;
    }

    public void Refuel() {
        if (extraTank) {
            currentFuel = delayFuel;
            extraTank = false;
        }
    }
}
