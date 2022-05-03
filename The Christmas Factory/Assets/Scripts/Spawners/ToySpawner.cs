using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] toysPrefabs;

    [SerializeField] private bool canSpawn = true;
    public bool CanSpawn { get { return canSpawn; } set { canSpawn = value; } }

    [SerializeField]
    [Range(0, 20)]
    private float spawnDelay = 2f;

    [SerializeField]
    private int toyCapacity = 0;

    [SerializeField]
    private GameObject[] toyPoll;

    private void Awake()
    {
        toyCapacity = (int)GetComponent<BoxCollider2D>().size.x;
        PopulatePool();
    }

    private void Start()
    {
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
        while (CanSpawn)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

}