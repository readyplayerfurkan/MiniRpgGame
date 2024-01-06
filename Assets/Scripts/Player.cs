using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float _xInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
        CheckInput();
        AnimatorControllers();
    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Movement()
    {
        _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x != 0;
        
        _anim.SetBool("isMoving", isMoving);
    }
}
