using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isMoving;
    
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
        _xInput = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);

        _isMoving = _rb.velocity.x != 0;
        
        _anim.SetBool("isMoving", _isMoving);
    }
}
