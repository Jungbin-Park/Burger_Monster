using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        MOVE,
        ATTACK,
        DEAD
    }

    PlayerStat playerStat;

    public State state = State.IDLE;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    // Animator 파라미터 해시 값
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashAttack2 = Animator.StringToHash("IsAttack2");
    private readonly int hashAttackTime = Animator.StringToHash("attackTime");


    private List<GameObject> Enemies;
    private Transform targetEnemy;

    void Start()
    {
        playerTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        playerStat = gameObject.GetComponent<PlayerStat>();

        StartCoroutine(CheckPlayerState());

        StartCoroutine(PlayerAction());
    }

    IEnumerator CheckPlayerState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            //float distance = Vector3.Distance(targetEnemy.position, playerTr.position);
            float distance = (targetEnemy.position - playerTr.position).magnitude;

            if (distance <= playerStat.AttackDist)
            {
                agent.SetDestination(transform.position);
                state = State.ATTACK;
            }
            else if (distance <= playerStat.TraceDist)
            {
                state = State.MOVE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator PlayerAction()
    {
        while (true)
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
            yield return new WaitForSeconds(0.3f);
        }
    }


    private void UpdateIdle()
    {
        agent.isStopped = true;

        FindEnemy();
    }

    private void FindEnemy()
    {
        // 주변 몬스터를 감지하여 저장
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("MONSTER"));
        Transform nearestEnemy = null;
        float maxDistance = Mathf.Infinity;

        // 가장 가까운 적 타겟으로 지정
        foreach(GameObject enemy in Enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                nearestEnemy = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        targetEnemy = nearestEnemy;
    }

    private void UpdateMove()
    {
        if(targetEnemy != null)
        {
            agent.SetDestination(targetEnemy.position);
            agent.isStopped = false;
            anim.SetBool(hashTrace, true);
        }
        else
        {
            FindEnemy();
        }
    }

    private void UpdateAttack()
    {
        if (targetEnemy != null)
        {
            anim.SetFloat(hashAttackTime, playerStat.AttackTime);
            anim.SetBool(hashAttack, true);
            Vector3 dir = targetEnemy.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 50 * Time.deltaTime);
        }
        else
        {
            FindEnemy();
        }
    }

    // 애니메이션 이벤트
    void OnAttack1Event()
    {
        if(targetEnemy != null)
        {
            MonsterStat targetStat = targetEnemy.GetComponent<MonsterStat>();
            targetStat.Hp -= playerStat.Damage;
            
            if(targetStat.Hp <= 0)
            {
                state = State.IDLE;
                anim.SetBool(hashAttack, false);
            }
        }
        anim.SetFloat(hashAttackTime, playerStat.AttackTime);
        anim.SetBool(hashAttack2, true);
    }

    void OnAttack2Event()
    {
        if(targetEnemy != null)
        {
            Collider[] nearEnemy = Physics.OverlapSphere(transform.position, playerStat.Attack2Dist);

            foreach (Collider coll in nearEnemy)
            {
                if (coll.CompareTag("MONSTER"))
                {
                    MonsterStat nearEnemyStat = coll.GetComponent<MonsterStat>();
                    nearEnemyStat.Hp -= playerStat.Damage;
                }
            }
        }
        else
        {
            state = State.IDLE;
        }
        

        // 체력 회복 Skill
    }

}
