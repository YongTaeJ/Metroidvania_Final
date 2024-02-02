using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTemp : MonoBehaviour
{
    public ObjectFlip ObjectFlip {get; private set;}

    private void Awake()
    {
        ObjectFlip = new ObjectFlip(transform);

        GetComponentInChildren<NPCPlayerFinder>().Initialize(ObjectFlip);
    }
}
