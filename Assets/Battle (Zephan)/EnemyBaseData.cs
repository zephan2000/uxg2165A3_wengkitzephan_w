using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseData : MonoBehaviour
{
	public string enemyId { get; }
	public string displayName { get; }
	public int healthMax { get; }
	public int enemyattk { get; }

	public EnemyBaseData(string enemyId, string displayName, int healthMax, int enemyattk)
	{
		this.enemyId = enemyId;
		this.displayName = displayName;
		this.healthMax = healthMax;
		this.enemyattk = enemyattk;
	}
}
