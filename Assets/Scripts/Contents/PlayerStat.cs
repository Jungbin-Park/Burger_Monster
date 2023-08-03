using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int exp;
    [SerializeField]
    private float attack2Dist;

    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public float Attack2Dist { get { return attack2Dist; } set { attack2Dist = value; } }


    void Start()
    {
        maxHp = 100;
        hp = 100;
        damage = 10;
        attackTime = 1.0f;
        attackDist = 1.0f;
        attack2Dist = 2.0f;
        traceDist = 15.0f;
        level = 1;
        exp = 0;
    }
}
