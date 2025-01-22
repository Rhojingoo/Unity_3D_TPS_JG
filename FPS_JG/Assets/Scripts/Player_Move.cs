using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
     CharacterController cc;
    public float Move_Speed = 7f;
    public float Jump_Power = 10f;
    public float yVelocity = 0f;
    public bool IsJump = false;
    float gravity = -20f;
    
    int Hp = 0;
    public int MaxHp = 100;
    public Slider Hp_Slider;

    public GameObject Hit_Effect;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 Dir = new Vector3(h, 0, v);
        Dir = Dir.normalized;
        Dir = Camera.main.transform.TransformDirection(Dir);

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (IsJump)
            {
                IsJump = false;
                yVelocity = 0f;
            }
        }

        if (Input.GetButtonDown("Jump") && !IsJump)
        {
            yVelocity = Jump_Power;
            IsJump = true;
        }

        yVelocity += gravity * Time.deltaTime;
        Dir.y = yVelocity;

        cc.Move(Dir* Move_Speed* Time.deltaTime);

        //transform.position += Dir * Move_Speed * Time.deltaTime;

        Hp_Slider.value = (float)Hp / (float)MaxHp;
    }

    public void Damaaged(int Dam)
    {
        Hp -= Dam;

        if (Hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        Hit_Effect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        Hit_Effect.SetActive(false);
    }
}
