using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public GameObject attackEffectPrefab;
    [HideInInspector] public LayerMask enemyLayer;

    private float moveSpeed = 5.0f;
    private float attackDistance = 2.0f;
    private Animator animator;
    private Weapon weapon;
    private bool isAttacking = false;
    private float currentHealth = 100f;
    private float walkDistance = 5f;
    private Rigidbody rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator.SetBool("Moving", true);
        animator.SetFloat("Velocity", moveSpeed / 5.0f);
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!isAttacking)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position + Vector3.up * 2.0f, transform.forward, out hit, Mathf.Min(attackDistance, walkDistance)))
            {
                if (!animator.GetBool("Moving"))
                    animator.SetBool("Moving", true);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Moving", false);
                if (((1 << hit.transform.gameObject.layer) & enemyLayer) != 0)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Trigger");
        float attackAnimationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(attackAnimationLength);
        
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position + Vector3.up * 2.0f, transform.forward * attackDistance, Color.red);
    }

    public void SetParameters(LayerMask layer, float spacing)
    {
        enemyLayer = layer;
        walkDistance = spacing;
        if (weapon)
        {
            weapon.enemyLayer = layer;
            weapon.attackEffectPrefab = attackEffectPrefab;
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
            Destroy(gameObject);
    }
}
