using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float attackRate = 2f;

    [SerializeField] float range = 1f;

    [SerializeField] LayerMask hittableLayers;

    Vector2 hitPoint;
    Vector2 playerPosition;
    

    float moveX, moveY;
    float nextAttackTime = 0f;
    float flipModificator = 1f;

    Vector2 moveDirection;
    bool leftHand = true;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        FlipRotation(moveDirection);
        playerPosition = new Vector2(transform.position.x + flipModificator, transform.position.y);

        if (Time.time >= nextAttackTime)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Attack();
				nextAttackTime = Time.time + 1f / attackRate;
                animator.SetBool("LeftHand", leftHand);
                animator.SetTrigger("Attack");
                leftHand = !leftHand;
			}
		}
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

    }

    void FlipRotation(Vector2 direction)
    {
        if(direction.x > 0)
        {
            //смотрит вправо
            flipModificator = 1f;
            sr.flipX = false;
        }

        if(direction.x < 0)
        {
            //смотрим влево
            flipModificator = -1f;
            sr.flipX = true;
        }
    }

    void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, hittableLayers);

        if(hitObjects != null)
        {
            //hitObjects.ToList().ForEach(obj => obj.GetComponent<ТутачкиКомпонентСХитЛогикой>().Hit());
            // просто выебываюсь, сэкономил три строчки
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerPosition, range);
    }
}
