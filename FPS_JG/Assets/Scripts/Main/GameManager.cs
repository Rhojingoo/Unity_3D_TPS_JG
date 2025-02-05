using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;

public class GameManager : MonoBehaviour
{
    public static GameManager Gm;
    public GameObject GameLable;
    public GameObject GameOption;
    Text GameText;
    Player_Move player;
    AudioSource BGMusic;
    public AudioClip BGMusicClip;
    public AudioClip WinMusicClip;
    public AudioClip GameOverClip;

    public enum GameState
    { 
        Open,
        Ready,
        Run,
        Pause,
        GameOver,
        FindFriend,
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
        player = GameObject.Find("Player").GetComponent<Player_Move>();
        GameText = GameLable.GetComponent<Text>();
        Openning();
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

            StopMusic();
            PlayGameoverMusic();
            State = GameState.GameOver;
        }

       if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOptionWindow();
        }
    }

    void Openning()
    {
        State = GameState.Open;
        GameText.text = "Save You'r Friend";
        GameText.color = new Color32(255, 185, 0, 255);
        StartCoroutine(OpentoReady());
    }

    IEnumerator OpentoReady()
    {
        yield return new WaitForSeconds(4f);
        GameLable.SetActive(false);

        yield return new WaitForSeconds(1f);
        GameLable.SetActive(true);
        State = GameState.Ready;
        GameText.text = "Ready~~~!";
        GameText.color = new Color32(255, 185, 0, 255);
        StartCoroutine(ReadytoStart());        
    }


    IEnumerator ReadytoStart()
    {
        yield return new WaitForSeconds(2f);
        GameText.text = "Play";
        yield return new WaitForSeconds(0.5f);
        GameLable.SetActive(false);
        State = GameState.Run;
        PlayBGMusic();
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


    public void Find_Friend()
    {
        State = GameState.FindFriend;
        GameLable.SetActive(true);
        GameText.text = "Input F";
    }

    public void Exit_Friend()
    {
        State = GameState.Run;
        GameLable.SetActive(false);
    }


    public void EndingGame()
    {
        // Time.timeScale = 1f;
        StopMusic();
        PlayWinMusic();
        State = GameState.Win;
        StartCoroutine(NextEndingScene());
        // SceneManager.LoadScene(1);
    }

    IEnumerator NextEndingScene()
    {
        yield return new WaitForSeconds(7f);
        GameLable.SetActive(true);
        GameText.text = "YouWin";
        yield return new WaitForSeconds(5.5f);
        GameLable.SetActive(false);
        SceneManager.LoadScene(3);
    }
    public void PlayBGMusic()
    {
        // 오디오 클립 지정 후 재생
        BGMusic.clip = BGMusicClip;
        BGMusic.loop = true;     // 루프 재생 여부(필요시 설정)
        BGMusic.Play();
    }

    public void PlayWinMusic()
    {
        // 오디오 클립 지정 후 재생
        BGMusic.clip = WinMusicClip;
        BGMusic.loop = true;     // 루프 재생 여부(필요시 설정)
        BGMusic.Play();
    }

    public void PlayGameoverMusic()
    {
        // 오디오 클립 지정 후 재생
        BGMusic.clip = WinMusicClip;
        BGMusic.loop = false;     // 루프 재생 여부(필요시 설정)
        BGMusic.Play();
    }

    public void StopMusic()
    {
        BGMusic.Stop();
    }


}
