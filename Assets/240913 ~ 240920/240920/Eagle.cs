using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Eagle : MonoBehaviour
{
    // 상태 패턴을 통한 몬스터의 행동을 표시
    public enum State { Idle, Trace, Return, Attack, Dead, Size }
    [SerializeField] State curState = State.Idle;

    [SerializeField] GameObject player;
    private float traceRange;
    private float attackRange;
    private bool isDead;

    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 startPos;

    //====================================================================================================================
    //====================================================================================================================
    #region 상대패턴 예제 1

    private void exStart()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void exUpdate()
    {
        switch (curState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Trace:
                Trace();
                break;
            case State.Return:
                Return();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    void Idle()
    {
        // 가만히 있는 몬스터의 행동 구현
        // 1. 가만히 있기

        // 2. 다른 상태로의 전환
        if (Vector2.Distance(transform.position, player.transform.position) < traceRange) { curState = State.Trace; }
    }

    void Trace()
    {
        // 플레이어를 쫒는 행동 구현
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // 다른 상태로의 전환
        if (Vector2.Distance(transform.position, player.transform.position) > traceRange) { curState = State.Return; }
        else if (Vector2.Distance(transform.position, player.transform.position) > attackRange) { curState = State.Attack; }
    }

    void Return()
    {
        // 플레이어를 쫒는 것을 그만하고 원래 자리로 돌아가는 행동 구현
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

        // 다른 상태로의 전환
        if (Vector2.Distance(transform.position, startPos) < 0.01f) { curState = State.Idle; }
    }

    void Attack()
    {
        // 플레이어를 공격하는 행동 구현
        Debug.Log("공격");

        // 다른 상태로의 전환
        if (Vector2.Distance(transform.position, player.transform.position) > attackRange) { curState = State.Trace; }
    }

    void Dead()
    {
        // 몬스터가 죽었을 때의 행동을 구현
        Debug.Log("죽음");
    }
    #endregion

    //====================================================================================================================
    //====================================================================================================================
    //                                                     상대패턴 예제 2

    private BaseState[] states = new BaseState[(int) State.Size];
    [SerializeField] IdleState idleState;
    [SerializeField] TraceState traceState;
    [SerializeField] ReturnState returnState;
    [SerializeField] AttackState attackState;
    [SerializeField] DeadState deadState;

    private void Awake()
    {
        states[(int)State.Idle] = idleState;
        states[(int)State.Trace] = traceState;
        states[(int)State.Return] = returnState;
        states[(int)State.Attack] = attackState;
        states[(int)State.Dead] = deadState;
    }

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    //=====================================================================================================================================

    public void ChangeState(State nextState)
    {
        states[(int)curState].Exit();
        curState = nextState;
        states[(int)curState].Enter();
    }

    //=====================================================================================================================================

    [System.Serializable]
    private class IdleState : BaseState
    {
        [SerializeField] Eagle eagle;
        [SerializeField] float traceRange;

        public override void Update()
        {
            // Idle 행동만 구현
            // 가만히 있기

            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < traceRange) { eagle.ChangeState(State.Trace); }
        }
    }

    [System.Serializable]
    private class TraceState : BaseState
    {
        [SerializeField] Eagle eagle;
        [SerializeField] float traceRange;
        [SerializeField] float attackRange;
        [SerializeField] float moveSpeed;


        public override void Update()
        {
            // Trace 행동만 구현
            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.player.transform.position, moveSpeed * Time.deltaTime);

            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > traceRange) { eagle.ChangeState(State.Return); }
            else if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < attackRange) { eagle.ChangeState(State.Attack); }
        }
    }

    [System.Serializable]
    private class ReturnState : BaseState
    {
        [SerializeField] Eagle eagle;
        [SerializeField] float moveSpeed;

        public override void Update()
        {
            // Return 행동만 구현
            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.startPos, moveSpeed * Time.deltaTime);

            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.startPos) < 0.01f) { eagle.ChangeState(State.Idle); }
        }
    }

    [System.Serializable]
    private class AttackState : BaseState
    {
        [SerializeField] Eagle eagle;
        [SerializeField] float attackRange;

        public override void Update()
        {
            // Attack 행동만 구현
            Debug.Log("공격");

            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > attackRange) { eagle.ChangeState(State.Trace); }
        }
    }

    [System.Serializable]
    private class DeadState : BaseState
    {
        [SerializeField] Eagle eagle;
    }

    //=====================================================================================================================================
    //=====================================================================================================================================
}
