using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timelineDirector;  // Ÿ�Ӷ��� ����
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject player;
    public GameObject friendcharactor;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        friendcharactor.SetActive(false);   
        // Ÿ�Ӷ����� ���� ������ Virtual Camera�� ��Ȱ��ȭ
        cinemachineCamera.Priority = 0;  // Priority�� ���� �����ؼ� �⺻ ī�޶� ����
        Camera.main.GetComponent<CinemachineBrain>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCinema()
    {
        player.SetActive(true);
        friendcharactor.SetActive(true);
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        ActivateCinemachineAndPlayTimeline();
    }


    void ActivateCinemachineAndPlayTimeline()
    {
        // Virtual Camera �켱���� �ø���
        cinemachineCamera.Priority = 10;

        // Ÿ�Ӷ��� ���
        timelineDirector.Play();
    }


    void PlayOrPauseTimeline()
    {
        // ���� ���¿� ���� ���/�Ͻ����� ��ȯ
        if (timelineDirector.state == PlayState.Playing)
        {
            timelineDirector.Pause();  // ��� ���̸� �Ͻ�����
        }
        else
        {
            timelineDirector.Play();   // ���������� ���
        }
    }
}
