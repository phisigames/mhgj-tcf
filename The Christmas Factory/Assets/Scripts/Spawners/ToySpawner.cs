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
    [Range(3, 20)]
    private int toyCapacity = 5;
    public int ToyCapacity { get { return toyCapacity; } }

    [SerializeField]
    private GameObject[] toyPoll;

    private void Awake()
    {
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
            if (!toy.activeSelf && !myConveyor.IsInBreak)
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
            yield return new WaitForSeconds(spawnDelay);
            EnableObjectInPool();
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