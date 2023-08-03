using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject monster;

    [SerializeField]
    int maxMonsterCount = 5;

    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    float spawnRadius = 10.0f;
    [SerializeField]
    float spawnTime = 5.0f;

    // 오브젝트 풀
    public Queue<GameObject> monsterPool = new Queue<GameObject>();

    public static GameManager instance = null;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        CreateMonsterPool();

        InvokeRepeating("Spawn", 1.0f, spawnTime);
    }

    void Spawn()
    {
        GameObject _monster = GetMonsterInPool();
        //NavMeshAgent agent = _monster?.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, spawnRadius);
        randDir.y = 0;
        randPos = spawnPosition + randDir;

        _monster?.transform.SetPositionAndRotation(randPos, Quaternion.identity);
        _monster?.SetActive(true);
    }

    void CreateMonsterPool()
    {
        for(int i = 0; i < maxMonsterCount; i++)
        {
            var _monster = Instantiate<GameObject>(monster);
            _monster.name = $"Monster_{i:00}";
            _monster.SetActive(false);
            monsterPool.Enqueue(_monster); 
        }
    }

    public GameObject GetMonsterInPool()
    {
        //foreach(var _monster in monsterPool)
        //{   
        //    if(_monster.activeSelf == false)
        //    {
        //        return _monster;
        //    }
        //}
        if(monsterPool.Count > 0)
        {
            GameObject nextMonster = monsterPool.Dequeue();
            if(nextMonster.activeSelf == false)
                return nextMonster;
        }
        return null;
    }

    public void ReturnMonsterToPool(GameObject monster)
    {
        monsterPool.Enqueue(monster);
    }
}

