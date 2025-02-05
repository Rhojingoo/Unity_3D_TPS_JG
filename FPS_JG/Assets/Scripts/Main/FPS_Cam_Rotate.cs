using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cammer_Rotate : MonoBehaviour
{
    public float rotSpeed = 200f;

    private Vector2 prevMousePos;
    private bool initialized = false;

    float currentPitch = 0f; // ���� ȸ����
    float currentYaw = 0f;   // �¿� ȸ����

    //float mx = 0f;
    //float my = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ���콺 Ŀ���� ����� 
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


        //��ó�� �н���
        //{
        //Vector3 Dir = new Vector3(-Mouse_Y,Mouse_X, 0);

        //transform.eulerAngles += Dir * rotSpeed * Time.deltaTime;

        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90f, 90);
        //transform.eulerAngles = rot;
        //}

        // ���� 

        //float Mouse_X = Input.GetAxis("Mouse X");
        //float Mouse_Y = Input.GetAxis("Mouse Y");

        //mx = Mouse_X * rotSpeed * Time.deltaTime;
        //my = Mouse_Y * rotSpeed * Time.deltaTime;
        //my = Mathf.Clamp(my, -90f, 90);

        //transform.eulerAngles += new Vector3(-my, mx, 0);


        //������ ����
        if (!initialized)
        {
            // Unity���� Raw Mouse Position(��ũ�� ��ǥ)�� �������� ���
            prevMousePos = Input.mousePosition;
            initialized = true;
            return; // �̹� �������� ȸ�� ��� �� ��
        }

       // ���� ���콺 ��ġ
        Vector2 curMousePos = Input.mousePosition;
        // �̹� �����ӿ����� �̵���(Delta)
        Vector2 delta = curMousePos - prevMousePos;

        // ���� ������ ����� ���� ���� ��ġ�� ����
        prevMousePos = curMousePos;

        // ȸ�� ���
        float deltaX = delta.x * rotSpeed * Time.deltaTime;
        float deltaY = delta.y * rotSpeed * Time.deltaTime;

        currentPitch -= deltaY;
        currentPitch = Mathf.Clamp(currentPitch, -90f, 90f);

        currentYaw += deltaX;

        transform.eulerAngles = new Vector3(currentPitch, currentYaw, 0f);
    }

}
