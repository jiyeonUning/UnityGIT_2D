using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    // �̵��� �����ִ� ����, �ش� ���� �������� �� �ִ�� ���� �� �ִ� ���ӵ��� ����
    [SerializeField] float movePower; [SerializeField] float maxMoveSpeed;
    // ���� �� �����ִ� ����, �ش� ���� �������� �� �ִ�� ������ �� �ִ� ���ӷ��� ����
    [SerializeField] float jumpPower; [SerializeField] float maxFallSpeed;

    [SerializeField] bool isGrounded;
    private float x;

    // �ؽð��� �����Ͽ� �ִϸ��̼��� ������� �� �ִ�.
    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");

    private int curAniHash;


    private void Update()
    {
        // 2D������ �¿츸���� �����̴� ��찡 ��ݼ��̱� ������, �ش� ������ �Է��� �� �ְԸ� �صξ���.
        x = Input.GetAxisRaw("Horizontal");
        // ���� ��� ����� �����̽���
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        GroundCheck();
        AnimatorPlay();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // �浹�� �Ͼ�� �� �����ϴ� �Լ� (2D ����)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹�մϴ�!");
    }

    //===========================================================================================================

    // 2D �������� �����ϴ� �Լ�
    void Move()
    {
        rigid.AddForce(Vector2.right * x * movePower, ForceMode2D.Force);

        // �̵� �� �ִ� �ӵ� �̻����δ� ���ӵ��� ���� �ʵ���, velocity ���� �������� �� �ִ� if�� �ۼ�
        if (rigid.velocity.x > maxMoveSpeed)
        { rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y); }
        else if (rigid.velocity.x < -maxMoveSpeed)
        { rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y); }

        // ���� �� �ִ� ���ӷ� �̻����δ� �������� �ʵ���, velocity ���� �������� �� �ִ� if�� �ۼ�
        if (rigid.velocity.y < -maxFallSpeed)
        { rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed); }

        // flipX �� üũ ���θ� �ٲ������ν� �¿������ �����ϰ� �Ͽ�, �� �ڷ� ������ �� �ֵ��� �ϴ� if���� �ۼ�
        if (x < 0) { render.flipX = true; }
        else if (x > 0) { render.flipX = false; }
    }

    // ������ �����ϴ� �Լ�
    void Jump()
    {
        if (isGrounded == false) return;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    // �������� �����ϴ� �Լ�
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        if (hit.collider != null) { isGrounded = true; }
        else { isGrounded = false; }
    }

    void AnimatorPlay()
    {
        int checkAniHash;

        // �ִϸ��̼� �÷��� if��

        // �÷��̾ y������ �̵��ϴ� �ӵ����� �־��� ���, ���� �ִϸ��̼� ���
        if (rigid.velocity.y > 1f) { checkAniHash = jumpHash; }
        // �÷��̾ y�࿡�� �Ʒ��� �̵��ϴ� �ӵ����� �־��� ���, �������� �ִϸ��̼� ���
        else if (rigid.velocity.y < -1f) { checkAniHash = fallHash; }

        // �÷��̾��� �ӵ��� 0.01f���� �۾��� ��, ������ �� �ִ� �ִϸ��̼� ���
        else if (rigid.velocity.sqrMagnitude < 0.01f) { checkAniHash = idleHash; }
        // �� ���� ���, �޸��� �ִϸ��̼� ���
        else { checkAniHash = runHash; }

        // �� �ؽ����� �ٸ��� ���� �̿��Ͽ�, �ִϸ��̼� ����� ������ �����Ͽ��� ��,
        // �ش��ϴ� �ִϸ��̼��� �ؽð��� ���� �ؽð��� �����ϰ� ����� �ش��ϴ� �ִϸ��̼��� ����� �� �ְ� ���־���.
        // �̸� ���� �� �����Ӹ��� �ִϸ��̼��� ������� �ʰ�, �ʿ��� ���� �ش� �ִϸ��̼��� ����� �� �ְ� �Ǿ���.
        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
}
