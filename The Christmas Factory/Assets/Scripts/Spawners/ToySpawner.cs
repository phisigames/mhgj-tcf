using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] toysPrefabs;

    [SerializeField]
    private ConveyorManager myConveyor;

    [SerializeField] private bool canSpawn;

    [SerializeField]
    [Range(0, 20)]
    private float spawnDelay = 2f;

    [SerializeField]
    [Range(0, 5)]
    private int toyAdvantage = 1;

    [SerializeField]
    private int toyCapacity = 0;

    [SerializeField]
    private GameObject[] toyPoll;

    private void Awake()
    {
        toyCapacity = (int)GetComponent<BoxCollider2D>().size.x - toyAdvantage;
        myConveyor = GetComponent<ConveyorManager>();
        PopulatePool();
    }

    private void OnEnable()
    {
        canSpawn = true;
        StartCoroutine(SpawnToys());
    }

    private void PopulatePool()
    {
        toyPoll = new GameObject[toyCapacity];
        for (int i = 0; i < toyPoll.Length; i++)
        {
            int enemyIndex = Random.Range(0, toysPrefabs.Length);
            toyPoll[i] = Instantiate(toysPrefabs[enemyIndex], transform);
            toyPoll[i].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < toyPoll.Length; i++)
        {
            GameObject toy = toyPoll[i];
            if (!toy.activeSelf)
            {
                toy.SetActive(true);
                return;
            }
        }

    }

    private IEnumerator SpawnToys()
    {
        while (canSpawn)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
            IsConveyorFull();
        }
    }

    private void IsConveyorFull()
    {
        if (myConveyor.ConveyorCapacity == myConveyor.ConveyorMaxCapacity)
        {
            canSpawn = false;
            this.enabled = false;
        }
    }
}