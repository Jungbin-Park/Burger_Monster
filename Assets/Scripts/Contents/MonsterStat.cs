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
        hp = 100;
        damage = 10;
        attackTime = 1;
        attackDist = 1;
        spawnTime = 5;
        maxSpawn = 5;
        traceDist = 5.0f;
    }

}
