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
    [SerializeField] protected LayerMask whatIsGround;
    protected bool IsGrounded;
    
    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }
    
    protected virtual void CollisionChecks()
    {
        IsGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    
    protected virtual void Flip()
    {
        FacingDir = FacingDir * -1;
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0, 180, 0);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
