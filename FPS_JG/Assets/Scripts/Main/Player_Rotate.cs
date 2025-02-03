using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotate : MonoBehaviour
{
    [Header("���콺 ȸ�� �ӵ�")]
    public float Rot_Spped = 200f;
    [Header("����(W) ���� ȸ�� �ӵ�")]
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


        // ����������������������������������������������������������������������������������������������������������������������������������
        // 1) �Է°� Ȯ��
        //    W(Vertical>0)�� ������, A/D/�¿�(Horizontal)�� ���� 0���� üũ
        float h = Input.GetAxis("Horizontal");   // -1~+1
        float v = Input.GetAxis("Vertical");     // -1~+1

        bool onlyForward = (v > 0.01f && Mathf.Abs(h) < 0.01f);

        // ����������������������������������������������������������������������������������������������������������������������������������
        // 2) "��Ȯ�� WŰ��" ���� ���¶�� -> ī�޶� �������� ���� ������
        if (onlyForward)
        {
            // (1) ī�޶� ���� ��������
            Vector3 camForward = Camera.main.transform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            // (2) ĳ���� ȸ��
            Quaternion targetRot = Quaternion.LookRotation(camForward);

            // (3) �ε巴�� ����
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                forwardRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // ��������������������������������������������������������������������������������������������������������������������������
            // 3) �� ��(A, D, S, �밢 ��) -> ����ó�� ���콺 ȸ�� ����
            //    (Yaw ȸ��)
            float Mouse_x = Input.GetAxis("Mouse X");
            mx += Mouse_x * Rot_Spped * Time.deltaTime;

            // Pitch(����)�� ���� ��ũ��Ʈ�� ó���� ���� ������, ������ ����
            transform.eulerAngles = new Vector3(0, mx, 0);
        }
    }
}

