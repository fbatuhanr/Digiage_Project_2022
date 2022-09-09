using System;
using NaughtyAttributes;
using UnityEngine;

public class CavemanController : MonoBehaviour
{
    public static CavemanController Instance;

    private Animator _animator;

    private bool isStartedToRunning;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        isStartedToRunning = false;
    }

    public void AttackAndRun()
    {
        _animator.SetTrigger("attack");
        Invoke(nameof(StartToRunning), 1f);
    }

    private void StartToRunning()
    {
        isStartedToRunning = true;
    }

    private void Update()
    {
        if (isStartedToRunning)
            transform.position += Vector3.right * (3 * Time.deltaTime);
    }
}