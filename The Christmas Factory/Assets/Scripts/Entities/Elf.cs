using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    //DESING DATA
    [SerializeField]
    [Range(1, 10)]
    private int resistenceToWrap = 3;
    public int ResistenceToWrap { get { return resistenceToWrap; } set { resistenceToWrap = value; } }
    //RUNTIME DATA
    [SerializeField]
    private int giftWrapping = 0;
    public int GiftWrapping { get { return giftWrapping; } set { giftWrapping = value; } }

    public bool isLimitToWrap()
    {
        return giftWrapping == resistenceToWrap;
    }
}
