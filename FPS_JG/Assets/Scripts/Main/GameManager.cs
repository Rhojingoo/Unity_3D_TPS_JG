using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Gm;
    public GameObject GameLable;
    public GameObject GameOption;
    Text GameText;
    Player_Move player;
    AudioSource BGMusic;

    public enum GameState
    { 
        Ready,
        Run,
        Pause,
        GameOver,
        Win
    }

    public GameState State;

    private void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }

        if (BGMusic == null)
        {
            BGMusic = gameObject.GetComponent<AudioSource>();
        }
        StopMusic();
    }

    // Start is called before the first frame update
    void Start()
    {
        StopMusic();
        State = GameState.Ready;
        GameText = GameLable.GetComponent<Text>();
        GameText.text = "Ready~~~!";
        GameText.color = new Color32(255, 185, 0, 255);
        player = GameObject.Find("Player").GetComponent<Player_Move>(); 

        StartCoroutine(ReadytoStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.PlayerHP <= 0)
        {
            GameLable.SetActive(true);
            GameText.text = "Game Over";
            GameText.color = new Color32(255, 0, 0, 255);

            Transform Buttons = GameText.transform.GetChild(0);
            Buttons.gameObject.SetActive(true);

            State = GameState.GameOver;
        }
    }

    IEnumerator ReadytoStart()
    {
        yield return new WaitForSeconds(2f);
        GameText.text = "Play";
        yield return new WaitForSeconds(0.5f);
        GameLable.SetActive(false);
        State = GameState.Run;
        PlayMusic();
    }

    public void SortPlayerAttack()
    {
        StartCoroutine(ImpossibleAttack());    
    }

    IEnumerator ImpossibleAttack()
    {
        GameLable.SetActive(true);
        GameText.text = "CannotAttackYourself";
        GameText.color = new Color32(255, 0, 0, 255);

        yield return new WaitForSeconds(0.5f);
        GameLable.SetActive(false);
    }

    public void OpenOptionWindow()
    {
        GameOption.SetActive(true);
        Time.timeScale = 0f;
        State = GameState.Pause;
    }

    public void CloseOptionWindow()
    {
        GameOption.SetActive(false);
        Time.timeScale = 1f;
        State = GameState.Run;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    { 
        Application.Quit();
    }


    public void EndingGame()
    {
       // Time.timeScale = 1f;
        State = GameState.Win;
        StartCoroutine(NextEndingScene());
        // SceneManager.LoadScene(1);
    }

    IEnumerator NextEndingScene()
    {
        yield return new WaitForSeconds(2f);
        GameLable.SetActive(true);
        GameText.text = "YouWin";
        yield return new WaitForSeconds(5.5f);
        GameLable.SetActive(false);        
    }
    public void PlayMusic()
    {
        // 오디오 클립 지정 후 재생
        BGMusic.loop = true;     // 루프 재생 여부(필요시 설정)
        BGMusic.Play();
    }
    public void StopMusic()
    {
        BGMusic.Stop();
    }
}
