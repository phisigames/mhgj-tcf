using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalloutManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer calloutRenderer;

    [SerializeField] List<Sprite> callouts = new List<Sprite>();

    void Start()
    {
        EnableCallout(false);
    }

    public void SetCalloutSprite(CalloutTypes type)
    {
        calloutRenderer.sprite = callouts[(int)type];
    }

    public void EnableCallout(bool status)
    {
        calloutRenderer.enabled = status;
    }
}
