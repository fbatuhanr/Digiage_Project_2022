using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class CavemanController : MonoBehaviour
{
    public static CavemanController Instance;

    [Tag] public string playerTag;
    private Transform _player;

    private Animator _animator;
    private bool _isStartedToRunning;
    public float runSpeed=5;

    [SerializeField] private float catchDistance = 3f;

    private void Awake()
    {
        Instance = this;
        _player = GameObject.FindWithTag(playerTag).transform;
    }

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _isStartedToRunning = false;
    }

    public void AttackAndRun()
    {
        _animator.SetTrigger("attack");
        Invoke(nameof(StartToRunning), 2f);
    }

    private void StartToRunning()
    {
        _isStartedToRunning = true;
    }

    private void FixedUpdate()
    {
        if (_isStartedToRunning)
        {
            var step =  runSpeed * Time.fixedDeltaTime; // calculate distance to move

            var targetVector = new Vector3(_player.position.x, 0, _player.position.z);

            if (CavemanAndPlayerDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetVector, step);

                var lookPos = _player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
            }
            else
            {
                _animator.SetTrigger("idle");
                _isStartedToRunning = false;
            }
        }
    }

    private bool CavemanAndPlayerDistance => Vector3.Distance(transform.position, _player.position) > catchDistance;


    public void TurnToThePlayer()
    {
        transform.DOLookAt(_player.position, .5f);
    }
}