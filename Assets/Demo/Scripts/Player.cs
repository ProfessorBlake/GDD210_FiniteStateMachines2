using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;

	public Enemy enemy;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

	private void FixedUpdate()
	{
		rb.AddForce( (moveInput * 3f) / Mathf.Max((rb.velocity.magnitude * 2f), 1));
	}
}
