using NaughtyAttributes;
using UnityEngine;

public class CavemanController : MonoBehaviour
{
    
    public Animator animator;

    [AnimatorParam("animator")]
    public int attackAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger(attackAnim);
        }
    }
}
