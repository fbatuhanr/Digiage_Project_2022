using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoliloquizeData", menuName = "ScriptableObjects/SoliloquizeScriptableObject", order = 0)]
public class SoliloquizeDialog : ScriptableObject
{
    [Serializable] public struct Soliloquize
    {
        public string [] data;
    }
    
    [SerializeField] private List<Soliloquize> soliloquizes;
}
