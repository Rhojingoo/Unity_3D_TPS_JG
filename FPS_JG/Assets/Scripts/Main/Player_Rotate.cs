using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotate : MonoBehaviour
{
    [Header("마우스 회전 속도")]
    public float Rot_Spped = 200f;
    [Header("전진(W) 강제 회전 속도")]
    public float forwardRotationSpeed = 5f;

    float mx = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Gm.State != GameManager.GameState.Run && GameManager.Gm.State != GameManager.GameState.FindFriend)
        {
            return;
        }

        //float Mouse_x = Input.GetAxis("Mouse X");
        //mx += Mouse_x * Rot_Spped * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, mx, 0);


        // ─────────────────────────────────────────────────────────────────
        // 1) 입력값 확인
        //    W(Vertical>0)만 누르고, A/D/좌우(Horizontal)는 거의 0인지 체크
        float h = Input.GetAxis("Horizontal");   // -1~+1
        float v = Input.GetAxis("Vertical");     // -1~+1

        bool onlyForward = (v > 0.01f && Mathf.Abs(h) < 0.01f);

        // ─────────────────────────────────────────────────────────────────
        // 2) "정확히 W키만" 누른 상태라면 -> 카메라 전방으로 몸을 돌리기
        if (onlyForward)
        {
            // (1) 카메라 정면 가져오기
            Vector3 camForward = Camera.main.transform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            // (2) 캐릭터 회전
            Quaternion targetRot = Quaternion.LookRotation(camForward);

            // (3) 부드럽게 보간
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                forwardRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // ─────────────────────────────────────────────────────────────
            // 3) 그 외(A, D, S, 대각 등) -> 기존처럼 마우스 회전 적용
            //    (Yaw 회전)
            float Mouse_x = Input.GetAxis("Mouse X");
            mx += Mouse_x * Rot_Spped * Time.deltaTime;

            // Pitch(상하)는 별도 스크립트로 처리할 수도 있으나, 지금은 생략
            transform.eulerAngles = new Vector3(0, mx, 0);
        }
    }
}

