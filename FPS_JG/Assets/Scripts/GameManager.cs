using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Gm;
    public GameObject GameLable;
    Text GameText;

    public enum GameState
    { 
        Ready,
        Run,
        GameOver
    }

    public GameState State;

    private void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        State = GameState.Ready;
        GameText = GameLable.GetComponent<Text>();
        GameText.text = "Ready~~~!";
        GameText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(ReadytoStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReadytoStart()
    {
        yield return new WaitForSeconds(2f);
        GameText.text = "Play";
        yield return new WaitForSeconds(0.5f);
        GameLable.SetActive(false);
        State = GameState.Run;
    }
}
