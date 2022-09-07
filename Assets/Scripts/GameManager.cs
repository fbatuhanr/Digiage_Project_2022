using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SoliloquizeDialog soliloquizeDialogData;

    private void Start()
    {
        Instance = this;
    }
}
