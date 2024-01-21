using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move Info")] 
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")] 
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D _isPlayerDetected;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (_isPlayerDetected)
        {
            if (_isPlayerDetected.distance > 1)
            {
                Rb.velocity = new Vector2(moveSpeed * 2.5f * FacingDir, Rb.velocity.y);
                
                Debug.Log("I see the player");
                IsAttacking = false;
            }
            else
            {
                Debug.Log("ATTACK! " + _isPlayerDetected.collider.gameObject.name);
                IsAttacking = true;
            }
        }
        
        if (!IsGrounded || IsWallDetected)
            Flip();
        
        Movement();
    }

    private void Movement()
    {
        if(!IsAttacking)
            Rb.velocity = new Vector2(moveSpeed * FacingDir, 0);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * FacingDir,
            whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * FacingDir, transform.position.y));
    }
}
