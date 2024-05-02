using System;
using System.Collections;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDistance = 2f;
    [HideInInspector] public LayerMask enemyLayer;
    private Animator animator;

    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        animator.SetBool("Moving", true);
        animator.SetFloat("Velocity", moveSpeed / 5);
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Move forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Check for enemy soldiers in front
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 2, 0), transform.forward, out hit, attackDistance,
                    enemyLayer))
            {
                print(hit);
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        animator.SetBool("Moving", false);
        // Trigger attack animation
        for(;;)
        {
            animator.SetTrigger("Trigger");
            float attackAnimationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
            yield return new WaitForSeconds(attackAnimationLength);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 2, 0), transform.forward * attackDistance, Color.red);
    }
}