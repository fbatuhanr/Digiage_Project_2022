using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isCinematic;

    private void Start()
    {
        Instance = this;
    }
}
