using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    // �̵��� �����ִ� ����, �ش� ���� �������� �� �ִ�� ���� �� �ִ� ���ӵ��� ����
    [SerializeField] float movePower; [SerializeField] float maxMoveSpeed;
    // ���� �� �����ִ� ����, �ش� ���� �������� �� �ִ�� ������ �� �ִ� ���ӷ��� ����
    [SerializeField] float jumpPower; [SerializeField] float maxFallSpeed;
    private float x;

    private void Update()
    {
        // 2D������ �¿츸���� �����̴� ��찡 ��ݼ��̱� ������, �ش� ������ �Է��� �� �ְԸ� �صξ���.
        x = Input.GetAxisRaw("Horizontal");
        // ���� ��� ����� �����̽���
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
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
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
