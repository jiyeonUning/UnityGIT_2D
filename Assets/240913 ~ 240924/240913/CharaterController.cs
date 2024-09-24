using UnityEngine;

public class CharaterController : MonoBehaviour
{
    // < 캐릭터 컨트롤러 >
    // 캡슐 콜라이더를 사용하며, 물리력을 가지고 있지는 않으나 충돌 여부를 판단할 수 있다. (rigidbody X)
    // 사실적이지 않고, 비현실적으로 움직이나 캐릭터로서는 자연스럽게 움직일 수 있는 기능을 제공한다.
    // 다만, 정밀도가 많이 떨어져 주로 사용해주지는 않는다. (바닥 인식을 잘 못함. 레이캐스트 활용으로 보완해줄 수 있다.)
    
    /* 각 이동방식에 적합한 게임 기능의 예제
    
    1. Transform : 위치기반으로 움직이는 방식
    UI의 이동, 리듬게임의 노드, 무조건적으로 움직여야 할 때 (자동사냥 등의 기능)에 적합하다.
     
    2. Rigidbody : 물리기반으로 움직이는 방식
    충돌이 중요한 기능 (포탄, 수류탄 투척/화살쏘기 등), 플랫포머 게임에 적합하다.
    
    3. NavMeshAgent : 경로탐색기반으로 움직이는 방식 (=내비게이션)
    RTS, AOS, 몬스터의 플레이어 추적 기능 등에 적합하다.

    4. CharacterController : 주변 충돌체를 기반으로 움직이는 방식
    1인칭, 3인칭, FPS, TPS에서의 움직임을 구현하는 데에 적합하다.

     */

    [SerializeField] CharacterController chrCon;
    [SerializeField] float speed;
    [SerializeField] float Yspeed;

    // 구현 예제
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
