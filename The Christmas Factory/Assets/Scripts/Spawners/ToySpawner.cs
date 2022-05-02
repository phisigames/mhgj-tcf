using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] toysPrefabs;

    [SerializeField]
    [Range(0, 20)]
    private float spawnerInitialDelay = 0f;

    [SerializeField]
    [Range(0, 20)]
    private float spawnerRepeatRate = 0.5f;

    void Start()
    {
        InvokeRepeating("InstantiateToy", spawnerInitialDelay, spawnerRepeatRate);
    }

    private void InstantiateToy()
    {
        //Debug.Log("NEW TOY");
        int enemyIndex = Random.Range(0, toysPrefabs.Length);
        Instantiate(toysPrefabs[enemyIndex], transform);
    }

}
