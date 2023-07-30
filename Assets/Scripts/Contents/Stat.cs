using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int attackTime;
    [SerializeField]
    protected int attackDist;
    [SerializeField]
    protected float traceDist;

    public int Hp { get { return hp; } set { hp = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public int AttackTime { get { return attackTime; } set { attackTime = value; } }
    public int AttackDist { get { return attackDist; } set { attackTime = value; } }
    public float TraceDist { get { return traceDist; } set { traceDist = value; } }


}
