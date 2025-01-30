using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingNextScene : MonoBehaviour
{
    public int sceneNumber = 2;
    public Slider Loading_Bar;
    public Text Loading_Text;

    bool Loading_Done = false;
    // Start is called before the first frame update
    void Start()
    {
        print("로딩신시작");
        StartCoroutine(TransitionNextScene(sceneNumber));
    }

    // Update is called once per frame
    void Update()
    {
        //if (!Loading_Done)
        //{
        //    Loading_Done = true;
        //    print("로딩신업뎃");
        //}
    }

    IEnumerator TransitionNextScene(int num)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            Loading_Bar.value = ao.progress;
            Loading_Text.text = (ao.progress * 100f).ToString() + "%";

            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }           
    }
}
