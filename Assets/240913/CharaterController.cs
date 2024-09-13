using UnityEngine;

public class CharaterController : MonoBehaviour
{
    // < ĳ���� ��Ʈ�ѷ� >
    // ĸ�� �ݶ��̴��� ����ϸ�, �������� ������ ������ ������ �浹 ���θ� �Ǵ��� �� �ִ�. (rigidbody X)
    // ��������� �ʰ�, ������������ �����̳� ĳ���ͷμ��� �ڿ������� ������ �� �ִ� ����� �����Ѵ�.
    // �ٸ�, ���е��� ���� ������ �ַ� ����������� �ʴ´�. (�ٴ� �ν��� �� ����. ����ĳ��Ʈ Ȱ������ �������� �� �ִ�.)
    
    /* �� �̵���Ŀ� ������ ���� ����� ����
    
    1. Transform : ��ġ������� �����̴� ���
    UI�� �̵�, ��������� ���, ������������ �������� �� �� (�ڵ���� ���� ���)�� �����ϴ�.
     
    2. Rigidbody : ����������� �����̴� ���
    �浹�� �߿��� ��� (��ź, ����ź ��ô/ȭ���� ��), �÷����� ���ӿ� �����ϴ�.
    
    3. NavMeshAgent : ���Ž��������� �����̴� ��� (=������̼�)
    RTS, AOS, ������ �÷��̾� ���� ��� � �����ϴ�.

    4. CharacterController : �ֺ� �浹ü�� ������� �����̴� ���
    1��Ī, 3��Ī, FPS, TPS������ �������� �����ϴ� ���� �����ϴ�.

     */

    [SerializeField] CharacterController chrCon;
    [SerializeField] float speed;
    [SerializeField] float Yspeed;

    // ���� ����
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z).normalized;
        chrCon.Move(dir * speed * Time.deltaTime);


        if (chrCon.isGrounded == false)
        {
            Yspeed -= Physics.gravity.y * Time.deltaTime;
            chrCon.Move(Vector3.down * Yspeed * Time.deltaTime);
        }
        else { Yspeed = 0f; }
    }
}
