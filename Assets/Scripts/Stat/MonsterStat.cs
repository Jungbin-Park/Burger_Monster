using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    private int spawnTime;
    [SerializeField]
    private int maxSpawn;

    void Start()
    {
        maxHp = 100;
        hp = 100;
        damage = 10;
        attackTime = 1.0f;
        attackDist = 1.0f;
        traceDist = 5.0f;
        spawnTime = 5;
        maxSpawn = 5;
    }

}
