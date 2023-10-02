using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStateEnum
	{
		Idle,
		Patrol,
		Attack
	}
	[SerializeField] private EnemyStateEnum enemyState;
	[SerializeField] private Player player;

	private SpriteRenderer sr;
	private Vector3 lastSeenPosition;
	private Rigidbody2D rb;
	private float attackDelay;
	public float dotProd;

	private void Start()
	{
		enemyState = EnemyStateEnum.Idle;
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		switch (enemyState)
		{
			case EnemyStateEnum.Idle:
				UpdateIdle();
				break;
			case EnemyStateEnum.Patrol:
				UpdatePatrol();
				break;
			case EnemyStateEnum.Attack:
				UpdateAttack();
				break;
		}
	}

	private void UpdateIdle()
	{
		if (TargetInLOS())
		{
			Debug.Log("Idle > Patrol");
			enemyState = EnemyStateEnum.Patrol;
		}
	}

	private void UpdatePatrol()
	{
		attackDelay -= Time.deltaTime;

		dotProd = Vector2.Dot(transform.right, player.transform.right);
		if (dotProd < -0.8f)
		{
			enemyState = EnemyStateEnum.Idle;
			return;
		}

		if (TargetInLOS())
		{
			lastSeenPosition = player.transform.position;
			if(Vector3.Distance(transform.position, lastSeenPosition) < 1f)
			{
				//Attack
				if(attackDelay <= 0f)
				{
					attackDelay = 1.5f;
					sr.color = Color.yellow;
					enemyState = EnemyStateEnum.Attack;
					rb.AddForce((lastSeenPosition - transform.position).normalized * 50);
				}
			}
		}
		else
		{
			if (Vector3.Distance(transform.position, lastSeenPosition) < 0.1f)
			{
				Debug.Log("Patrol > Idle");
				enemyState = EnemyStateEnum.Idle;
			}
		}

		rb.AddForce((lastSeenPosition - transform.position).normalized * 0.25f / Mathf.Max((rb.velocity.magnitude * 2f), 1));
	}

	private void UpdateAttack()
	{
		attackDelay -= Time.deltaTime;
		if(attackDelay <= 0f)
		{
			attackDelay = 1f;
			enemyState = EnemyStateEnum.Idle;
		}
	}

	private bool TargetInLOS()
	{
		Debug.DrawRay(transform.position, player.transform.position - transform.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,player.transform.position - transform.position);
		if (hits.Length > 0)
		{
			if (hits[1].transform.GetComponent<Player>())
			{
				sr.color = new Color(1f, 0, 0);
				return true;
			}
		}
		sr.color = new Color(0.5f, 0.5f, 0.3f);
		return false;
	}
}
