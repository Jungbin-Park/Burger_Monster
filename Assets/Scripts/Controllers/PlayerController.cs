using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        MOVE,
        ATTACK1,
        ATTACK2,
        DEAD
    }

    PlayerStat playerStat;

    public State state = State.IDLE;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    void Start()
    {
        playerTr = GetComponent<Transform>();
        monsterTr = GameObject.FindWithTag("MONSTER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTr.position;

        playerStat = gameObject.GetComponent<PlayerStat>();

        StartCoroutine(CheckPlayerState());
    }

    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                break;
            case State.MOVE:
                break;
            case State.ATTACK1:
                break;
            case State.ATTACK2:
                break;

        }
    }

    IEnumerator CheckPlayerState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (distance <= playerStat.AttackDist)
                state = State.ATTACK1;
            else if (distance <= playerStat.TraceDist)
                state = State.MOVE;
            else
                state = State.IDLE;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        if(state == State.IDLE)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, playerStat.TraceDist);
        }
        
    }
    */
}
