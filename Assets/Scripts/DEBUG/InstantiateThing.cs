using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
public class InstantiateThing : MonoBehaviour
{
    [SerializeField] GameObject objectForInstantiate;
    [SerializeField] Transform instantiatePoint;

    [Button("Create Thing", EButtonEnableMode.Playmode)]
    public void InstantiateForDebug() {
        Instantiate(objectForInstantiate, instantiatePoint);
    }
}
