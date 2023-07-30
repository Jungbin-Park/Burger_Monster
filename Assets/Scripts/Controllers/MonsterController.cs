using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        MOVE,
        ATTACK,
        DEAD
    }

    MonsterStat monsterStat;

    public State state = State.IDLE;

    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        monsterStat = gameObject.GetComponent<MonsterStat>();

        StartCoroutine(CheckMonsterState());

        StartCoroutine(MonsterAction());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.5f);
            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (distance <= monsterStat.AttackDist)
                state = State.ATTACK;
            else if (distance <= monsterStat.TraceDist)
                state = State.MOVE;
            else
                state = State.IDLE;
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    UpdateIdle();
                    break;
                case State.MOVE:
                    UpdateMove();
                    break;
                case State.ATTACK:
                    UpdateAttack();
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void UpdateIdle()
    {
        agent.isStopped = true;
        anim.SetBool(hashTrace, false);
    }

    void UpdateMove()
    {
        agent.SetDestination(playerTr.position);
        agent.isStopped = false;
        anim.SetBool(hashTrace, true);
        anim.SetBool(hashAttack, false);
    }

    void UpdateAttack()
    {
        anim.SetBool(hashAttack, true);
    }


    /*
    private void OnDrawGizmos()
    {
        if (state == State.IDLE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, monsterStat.TraceDist);
        }
    }
    */
}
