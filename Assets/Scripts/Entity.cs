using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D Rb;
    protected Animator Anim;
    
    protected int FacingDir = 1;
    protected bool IsFacingRight = true;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    
    protected bool IsGrounded;
    protected bool IsWallDetected;
    protected bool IsAttacking;
    
    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();

        if (wallCheck == null)
            wallCheck = transform;
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }
    
    protected virtual void CollisionChecks()
    {
        IsGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        IsWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * FacingDir, whatIsGround);
    }
    
    protected virtual void Flip()
    {
        FacingDir = FacingDir * -1;
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * FacingDir, wallCheck.position.y));
    }
}
