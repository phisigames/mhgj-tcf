using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointCollisions : MonoBehaviour
{

    [SerializeField]
    [Range(1, 30)]
    private int emotionalDamage = 1;
    public int EmotionalDamage { get { return emotionalDamage; } set { emotionalDamage = value; } }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            StressManager.IncreaseStress(emotionalDamage);
            other.gameObject.SetActive(false);
        }
    }
}
