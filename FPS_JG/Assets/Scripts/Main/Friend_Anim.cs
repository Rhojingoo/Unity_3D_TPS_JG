using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_Anim : MonoBehaviour
{
    Animator Anim;
    public GameObject EndingCinema;
    TimelineController TC;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        TC = EndingCinema.GetComponent<TimelineController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // Layer 8이 플레이어
        if (other.gameObject.layer == 8)
        {
            print("플레이어와 만났습니다");
            GameManager.Gm.Find_Friend();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        // Layer 8이 플레이어
        if (other.gameObject.layer == 8)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Anim.SetTrigger("STANDUP");
                GameManager.Gm.Exit_Friend();
                TC.PlayCinema();
                GameManager.Gm.EndingGame();
                other.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Layer 8이 플레이어
        if (other.gameObject.layer == 8)
        {
            GameManager.Gm.Exit_Friend();
        }
    }


    public void GameWin()
    {
        GameManager.Gm.EndingGame();
    }
}
