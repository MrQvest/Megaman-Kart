using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VariablesMenu", menuName = "ScriptableObjects/VariableObjects", order = 1)]
public class VariablesHolder : ScriptableObject
{
    public GameObject selectedItemP1;
    public bool itemUsedP1;
    public bool itemUsedP2;
    public GameObject selectedItemP2;
    public GameObject itemHolderP1;
    public GameObject itemHolderP2;
}
