using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    public InputField id;
    public InputField password;
    public Text notify;

    // Start is called before the first frame update
    void Start()
    {
        notify.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, password.text))
        {
            return;
        }

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "아이디 생성완료";
        }
        else
        {
            notify.text = "이미 존재합니다";
        }
    }


    public void CheckUserData()
    {
        if (!CheckInput(id.text, password.text))
        {
            return;
        }

        string User_PW= PlayerPrefs.GetString(id.text);

        if (User_PW == password.text)
        {
            SceneManager.LoadScene(1);            
        }
        else 
        {
            notify.text = "아이디 또는 패스워드가 일치하지 않습니다";
        }
    }


    bool CheckInput(string Userid, string Userpw)
    {
        if (Userid == "" || Userpw == "")
        {
            notify.text = "아이디 또는 패스워드를 입력해주세요";
            return false;
        }
        
        return true;
    }
}
