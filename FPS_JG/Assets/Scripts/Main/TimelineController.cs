using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timelineDirector;  // 타임라인 디렉터
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject player;
    public GameObject friendcharactor;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        friendcharactor.SetActive(false);   
        // 타임라인이 시작 전에는 Virtual Camera를 비활성화
        cinemachineCamera.Priority = 0;  // Priority를 낮게 설정해서 기본 카메라 유지
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
        // Virtual Camera 우선순위 올리기
        cinemachineCamera.Priority = 10;

        // 타임라인 재생
        timelineDirector.Play();
    }


    void PlayOrPauseTimeline()
    {
        // 현재 상태에 따라 재생/일시정지 전환
        if (timelineDirector.state == PlayState.Playing)
        {
            timelineDirector.Pause();  // 재생 중이면 일시정지
        }
        else
        {
            timelineDirector.Play();   // 멈춰있으면 재생
        }
    }
}
