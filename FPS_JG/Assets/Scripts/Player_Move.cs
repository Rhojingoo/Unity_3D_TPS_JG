using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public GameObject Hit_Effect;
    CharacterController cc;
    Animator Anim;

    public float Move_Speed = 7f;
    public float Jump_Power = 10f;
    public float rotationSpeed = 10f;
    float gravity = -20f;

    
    int Hp = 0;
    public int MaxHp = 100;
    public Slider Hp_Slider;

    public bool IsJump = false;
    public float yVelocity = 0f;
    bool Die = false;


    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;

        cc = GetComponent<CharacterController>();
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP <= 0 && !Die)
        {
            Die = true;
            Anim.SetTrigger("DIE");
        }

        Hp_Slider.value = (float)Hp / (float)MaxHp;

        if (GameManager.Gm.State != GameManager.GameState.Run)
        {
            return;
        }


            // 1) 입력
        float h = Input.GetAxis("Horizontal"); // -1~+1
        float v = Input.GetAxis("Vertical");   // -1~+1
        Vector2 inputDir = new Vector2(h, v);

        // 2) 카메라 기준 이동 방향
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // 최종 이동 방향 (월드 좌표계)
        Vector3 moveDir = camForward * v + camRight * h;
        float inputMagnitude = inputDir.magnitude; // 0 ~ 1

        // 3) 점프 & 중력
        if (cc.isGrounded)
        {
            if (IsJump)
            {
                IsJump = false;
                yVelocity = 0f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = Jump_Power;
                IsJump = true;
            }
        }
        yVelocity += gravity * Time.deltaTime;
        moveDir.y = yVelocity;

        // 4) 이동
        cc.Move(moveDir * Move_Speed * Time.deltaTime);
        

        // ========== 핵심: 항상 카메라 정면을 바라보기 ==========
        // 캐릭터가 (y축 기준) 카메라 방향으로 회전
        if (camForward.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // ========== 2D Blend Tree 파라미터(Speed, Angle) ==========
        // (A) Speed: 입력 세기(0=정지, 1=최대)
        float speed = Mathf.Clamp01(inputMagnitude);

        // (B) Angle: "카메라 정면"과 "이동 방향" 사이의 각도 (부호 있음)
        //  - ex) 정면=0도, 오른쪽=+90도, 왼쪽=-90도, 뒤=±180도
        float angle = 0f;
        if (speed > 0.01f)
        {
            // 이동 방향(수평면)
            Vector3 planeMoveDir = new Vector3(moveDir.x, 0f, moveDir.z).normalized;
            // 카메라가 바라보는 정면
            Vector3 planeCamFwd = camForward; // 이미 y=0 처리됨

            angle = Vector3.SignedAngle(planeCamFwd, planeMoveDir, Vector3.up);
            // angle 범위: -180 ~ +180
        }

        // → Animator에 전달
        Anim.SetFloat("MoveMotion", inputDir.magnitude);
        //Anim.SetFloat("Speed", speed);
        //Anim.SetFloat("Angle", angle);

        //Hp_Slider.value = (float)Hp / (float)MaxHp;
    }
    void basemove()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //Vector3 Dir = new Vector3(h, 0, v);
        //Dir = Dir.normalized;
        //Anim.SetFloat("MoveMotion", Dir.magnitude);
        //Dir = Camera.main.transform.TransformDirection(Dir);


        //if (cc.collisionFlags == CollisionFlags.Below)
        //{
        //    if (IsJump)
        //    {
        //        IsJump = false;
        //        yVelocity = 0f;
        //    }
        //}

        //if (Input.GetButtonDown("Jump") && !IsJump)
        //{
        //    yVelocity = Jump_Power;
        //    IsJump = true;
        //}

        //yVelocity += gravity * Time.deltaTime;
        //Dir.y = yVelocity;

        //cc.Move(Dir* Move_Speed* Time.deltaTime);

        //PlayerRotate(v, h, Dir);
    }
    void PlayerRotate(float v, float h, Vector3 Dir)
    {
        //transform.position += Dir * Move_Speed * Time.deltaTime;
        // ─────────────────────────────────────────────────
        // 캐릭터 회전 (TPS: 이동 방향을 향하도록)
        //    - 아래는 "이동 방향"을 기준으로 회전
        //    - 또는 "카메라 정면"을 기준으로 회전하고 싶다면
        //      Camera.main.transform.forward 로 대체 가능
        // ─────────────────────────────────────────────────
        // y값(상하) 빼고, 평면에서만 바라볼 방향 계산

        //   "정확히 전진(W)만" 누를 때만 회전
        //    즉, v > 0 (앞) 이면서, h가 0에 가깝고(좌우가 거의 없음)

        if (v > 0.01f && Mathf.Abs(h) < 0.01f)
        {
            Vector3 lookDir = new Vector3(Dir.x, 0f, Dir.z);

            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }



    public void Damaaged(int Dam)
    {
        Hp -= Dam;

        if (Hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        Hit_Effect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        Hit_Effect.SetActive(false);
    }

    public int PlayerHP
    {
        get 
        {
            return Hp;
        }       
    }
}
