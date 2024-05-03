using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterDate", menuName ="Scriptable Object/Monster Data", order = int.MaxValue)]


public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string MonsterName;
    public string monsterName { get { return MonsterName; } }

    [SerializeField]
    private int Hp;
    public int hp { get { return Hp; } }

    [SerializeField]
    private int Damage;
    public int damage { get { return Damage; } }

    [SerializeField]
    private int Defense;
    public int defense { get { return Defense; } }

    [SerializeField]
    private float MoveSpeed;
    public float moveSpeed { get { return MoveSpeed; } }

    [SerializeField]
    private float AttackDistance;
    public float attackDistance { get { return AttackDistance; } }

    [SerializeField]
    private float AttackSpeed;
    public float attackSpeed { get { return AttackSpeed; } }

    [SerializeField]
    private float AttackMotionSpeed;
    public float attackMotionSpeed { get { return AttackMotionSpeed; } }

    [SerializeField]
    private int CoinValue;
    public int coinValue { get { return CoinValue; } }
}
