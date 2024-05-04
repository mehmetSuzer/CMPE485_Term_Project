using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private GameObject attackEffect;

    [HideInInspector] public LayerMask enemyLayer;
    private Animator animator;
    private Weapon weapon;

    private bool isAttacking = false;
    private float currentHealth = 100f;
    private float walkDistance;

    private RaycastHit hitInFront;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        animator.SetBool("Moving", true);
        animator.SetFloat("Velocity", moveSpeed / 5);
    }

    void Update()
    {
        UpdateMovement();
        // UpdateAttack();
    }

    private void UpdateMovement()
    {
        if (!isAttacking)
        {
            if (!Physics.Raycast(transform.position + Vector3.up * 2f, transform.forward, out hitInFront, walkDistance))
            {
                if (!animator.GetBool("Moving"))
                    animator.SetBool("Moving", true);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                // // Check if the object in front is an enemy
                // if (((1 << hitInFront.transform.gameObject.layer) & enemyLayer) == 0)
                // {
                    animator.SetBool("Moving", false);
                    UpdateAttack();
                // }

            }
        }
    }

    private void UpdateAttack()
    {
        if (!Physics.Raycast(transform.position + Vector3.up * 2f, transform.forward, out hitInFront, attackDistance))
        {
            if (!animator.GetBool("Moving"))
                animator.SetBool("Moving", true);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
        else if (((1 << hitInFront.transform.gameObject.layer) & enemyLayer) != 0)
        {
            animator.SetBool("Moving", false);
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("Moving", false);

        for (;;)
        {
            animator.SetTrigger("Trigger");
            float attackAnimationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
            yield return new WaitForSeconds(attackAnimationLength);

            if (!Physics.Raycast(transform.position + Vector3.up * 2f, transform.forward, out hitInFront,
                    attackDistance))
                break;
            if (((1 << hitInFront.transform.gameObject.layer) & enemyLayer) != 0) continue;
            isAttacking = false;
            break;
        }


        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position + Vector3.up * 2f, transform.forward * attackDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 2f, transform.forward * walkDistance, Color.green);
    }

    public void SetParameters(LayerMask layer, float spacing)
    {
        enemyLayer = layer;
        walkDistance = spacing;
        if (weapon)
        {
            weapon.enemyLayer = layer;
            weapon.attackEffect = attackEffect;
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}