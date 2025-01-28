using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Event : MonoBehaviour
{
    public Player_Fire PF; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerFire()
    {
        PF.GunFire();
    }

    void PlayerBomb()
    {
        PF.BombFire();
    }
}
