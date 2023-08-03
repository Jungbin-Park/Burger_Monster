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

    // Animator �Ķ���� �ؽ� ��
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashAttackTime = Animator.StringToHash("attackTime");
    private readonly int hashDead = Animator.StringToHash("Dead");

    void OnEnable()
    {
        StartCoroutine(CheckMonsterState());

        StartCoroutine(MonsterAction());
    }

    private void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        monsterStat = gameObject.GetComponent<MonsterStat>();
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            //float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (monsterStat.Hp <= 0)
            {
                state = State.DEAD;
                yield break;
            }

            float distance = (playerTr.position - monsterTr.position).magnitude;

            if (distance <= monsterStat.AttackDist)
            {
                agent.SetDestination(transform.position);
                state = State.ATTACK;
            }
            else if (distance <= monsterStat.TraceDist)
            {
                state = State.MOVE;
            }
            else
            {
                state = State.IDLE;
            }
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
                case State.DEAD:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDead);
                    GetComponent<SphereCollider>().enabled = false;

                    yield return new WaitForSeconds(1.0f);
                    
                    monsterStat.Hp = 100;
                    isDie = false;
                    GetComponent<SphereCollider>().enabled = true;
                    this.gameObject.SetActive(false);

                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void UpdateIdle()
    {
        agent.isStopped = true;
    }

    void UpdateMove()
    {
        agent.SetDestination(playerTr.position);
        agent.isStopped = false;
        anim.SetBool(hashTrace, true);
    }

    void UpdateAttack()
    {
        if(playerTr != null)
        {
            anim.SetFloat(hashAttackTime, monsterStat.AttackTime);
            anim.SetBool(hashAttack, true);
            Vector3 dir = playerTr.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
        
    }

    // �ִϸ��̼� �̺�Ʈ
    void OnAttackEvent()
    {
        if(!isDie)
        {
            PlayerStat targetStat = playerTr.GetComponent<PlayerStat>();
            targetStat.Hp -= monsterStat.Damage;
        }
    }

    

}
