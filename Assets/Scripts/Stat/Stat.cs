using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int maxHp;
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float attackTime;
    [SerializeField]
    protected float attackDist;
    [SerializeField]
    protected float traceDist;

    public int MaxHp {  get { return maxHp; } set { maxHp = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public float AttackTime { get { return attackTime; } set { attackTime = value; } }
    public float AttackDist { get { return attackDist; } set { attackTime = value; } }
    public float TraceDist { get { return traceDist; } set { traceDist = value; } }


}
