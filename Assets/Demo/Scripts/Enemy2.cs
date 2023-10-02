using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
	public enum EnemyStateEnum
	{
		Idle,
		Patrol,
		Attack,
		Dead,
		Reloading,
		TakeFriendlyFire
	}

	public EnemyStateEnum CurrentState = EnemyStateEnum.Idle;

	private void Update()
	{
		switch (CurrentState)
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

	private void UpdateAttack()
	{
		throw new NotImplementedException();
	}

	private void UpdateIdle()
	{
		//look for player, if seen change to patrol state
	}

	private void UpdatePatrol()
	{
		//move toward player, if lose LOS change to idle
		//If distance to player < 2 change to Attack state
	}
}
