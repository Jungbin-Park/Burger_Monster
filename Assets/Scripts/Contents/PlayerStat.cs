using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int exp;

    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { level = exp; } }

    void Start()
    {
        hp = 100;
        damage = 10;
        attackTime = 1;
        attackDist = 1;
        level = 1;
        exp = 0;
        traceDist = 7.0f;
    }
}
