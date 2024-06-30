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

    Rigidbody2D rb;

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        rb.velocity = moveDirection * moveSpeed;

        FlipRotation(moveDirection);
        playerPosition = new Vector2(transform.position.x + flipModificator, transform.position.y);

        if (Time.time >= nextAttackTime)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Attack();
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
    }

    void FlipRotation(Vector2 direction)
    {
        if(direction.x > 0)
        {
            //смотрит вправо
            flipModificator = 1f;
        }

        if(direction.x < 0)
        {
            //смотрим влево
            flipModificator = -1f;
        }
    }

    void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, hittableLayers);

        if(hitObjects != null)
        {
            //hitObjects.ToList().ForEach(obj => obj.GetComponent<IHitable>().Hit(Parameters.power));
            foreach(Collider2D hitObject in hitObjects)
            {
                if(hitObject.TryGetComponent<IHitable>(out IHitable hitable))
                {
                    hitable.Hit(Parameters.power);
                    Parameters.power += 1 * Parameters.powerGrowthMultiplier;
                    Debug.Log(Parameters.power);
                    Debug.Log("Hit");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerPosition, range);
    }
}
