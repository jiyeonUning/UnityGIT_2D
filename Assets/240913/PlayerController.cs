using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    // 이동에 가해주는 힘과, 해당 힘이 가해졌을 때 최대로 가질 수 있는 가속도를 설정
    [SerializeField] float movePower; [SerializeField] float maxMoveSpeed;
    // 점프 시 가해주는 힘과, 해당 힘이 가해졌을 때 최대로 가해질 수 있는 가속력을 설정
    [SerializeField] float jumpPower; [SerializeField] float maxFallSpeed;

    [SerializeField] bool isGrounded;
    private float x;

    // 해시값을 설정하여 애니메이션을 출력해줄 수 있다.
    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");

    private int curAniHash;


    private void Update()
    {
        // 2D에서는 좌우만으로 움직이는 경우가 대반수이기 때문에, 해당 값만을 입력할 수 있게만 해두었다.
        x = Input.GetAxisRaw("Horizontal");
        // 점프 기능 사용은 스페이스로
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        GroundCheck();
        AnimatorPlay();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 충돌이 일어났을 때 반응하는 함수 (2D 전용)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌합니다!");
    }

    //===========================================================================================================

    // 2D 움직임을 구현하는 함수
    void Move()
    {
        rigid.AddForce(Vector2.right * x * movePower, ForceMode2D.Force);

        // 이동 시 최대 속도 이상으로는 가속도가 붙지 않도록, velocity 값을 조절해줄 수 있는 if문 작성
        if (rigid.velocity.x > maxMoveSpeed)
        { rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y); }
        else if (rigid.velocity.x < -maxMoveSpeed)
        { rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y); }

        // 점프 시 최대 가속력 이상으로는 떨어지지 않도록, velocity 값을 조절해줄 수 있는 if문 작성
        if (rigid.velocity.y < -maxFallSpeed)
        { rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed); }

        // flipX 의 체크 여부를 바꿔줌으로써 좌우반전을 가능하게 하여, 앞 뒤로 움직일 수 있도록 하는 if문을 작성
        if (x < 0) { render.flipX = true; }
        else if (x > 0) { render.flipX = false; }
    }

    // 점프를 구현하는 함수
    void Jump()
    {
        if (isGrounded == false) return;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    // 점프수를 제한하는 함수
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        if (hit.collider != null) { isGrounded = true; }
        else { isGrounded = false; }
    }

    void AnimatorPlay()
    {
        int checkAniHash;

        // 애니메이션 플레이 if문

        // 플레이어가 y축으로 이동하는 속도값이 있었을 경우, 점프 애니메이션 출력
        if (rigid.velocity.y > 1f) { checkAniHash = jumpHash; }
        // 플레이어가 y축에서 아래로 이동하는 속도값이 있었을 경우, 떨어지는 애니메이션 출력
        else if (rigid.velocity.y < -1f) { checkAniHash = fallHash; }

        // 플레이어의 속도가 0.01f보다 작았을 때, 가만히 서 있는 애니메이션 출력
        else if (rigid.velocity.sqrMagnitude < 0.01f) { checkAniHash = idleHash; }
        // 그 외의 경우, 달리는 애니메이션 출력
        else { checkAniHash = runHash; }

        // 각 해쉬값이 다르단 점을 이용하여, 애니메이션 출력의 조건을 만족하였을 때,
        // 해당하는 애니메이션의 해시값을 현재 해시값을 동일하게 만들어 해당하는 애니메이션을 출력할 수 있게 해주었다.
        // 이를 통해 한 프레임마다 애니메이션을 재생하지 않고, 필요할 때만 해당 애니메이션을 출력할 수 있게 되었다.
        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
}
