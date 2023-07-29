using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        WALK,
        ATTACK,
        DEAD
    }

    public State state = State.IDLE;
    public float traceDist = 5;
    public float attackDist = 1;
    public bool isDie = false;

    public int hp = 100;


    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTr.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
