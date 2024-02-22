using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    // Cached
    [SerializeField] private GameObject spawnableCapsule;
    [SerializeField] private GameObject disposableCapsule;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject parachute;

    public void Spawn() {
        
        disposableCapsule.SetActive(false);
        Instantiate(spawnableCapsule, spawnPos.transform.position, Quaternion.identity);
        parachute.SetActive(true);
    }
}
