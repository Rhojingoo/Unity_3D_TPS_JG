using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cammer_Rotate : MonoBehaviour
{
    public float rotSpeed = 200f;

    private Vector2 prevMousePos;
    private bool initialized = false;

    float currentPitch = 0f; // 상하 회전값
    float currentYaw = 0f;   // 좌우 회전값

    //float mx = 0f;
    //float my = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // 마우스 커서를 숨기고 
        Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Locked;

        Vector3 initAngle = transform.eulerAngles;
        currentPitch = initAngle.x;
        currentYaw = initAngle.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Gm.State != GameManager.GameState.Run)
        {
            return;
        }


        //맨처음 학습시
        //{
        //Vector3 Dir = new Vector3(-Mouse_Y,Mouse_X, 0);

        //transform.eulerAngles += Dir * rotSpeed * Time.deltaTime;

        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90f, 90);
        //transform.eulerAngles = rot;
        //}

        // 기존 

        //float Mouse_X = Input.GetAxis("Mouse X");
        //float Mouse_Y = Input.GetAxis("Mouse Y");

        //mx = Mouse_X * rotSpeed * Time.deltaTime;
        //my = Mouse_Y * rotSpeed * Time.deltaTime;
        //my = Mathf.Clamp(my, -90f, 90);

        //transform.eulerAngles += new Vector3(-my, mx, 0);


        //변경할 내용
        if (!initialized)
        {
            // Unity에서 Raw Mouse Position(스크린 좌표)을 가져오는 방식
            prevMousePos = Input.mousePosition;
            initialized = true;
            return; // 이번 프레임은 회전 계산 안 함
        }

       // 현재 마우스 위치
        Vector2 curMousePos = Input.mousePosition;
        // 이번 프레임에서의 이동량(Delta)
        Vector2 delta = curMousePos - prevMousePos;

        // 다음 프레임 계산을 위해 현재 위치를 저장
        prevMousePos = curMousePos;

        // 회전 계산
        float deltaX = delta.x * rotSpeed * Time.deltaTime;
        float deltaY = delta.y * rotSpeed * Time.deltaTime;

        currentPitch -= deltaY;
        currentPitch = Mathf.Clamp(currentPitch, -90f, 90f);

        currentYaw += deltaX;

        transform.eulerAngles = new Vector3(currentPitch, currentYaw, 0f);
    }

}
