using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Event : MonoBehaviour
{
    public Enermy_FSM fsm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit()
    {
        fsm.AttackAction();
    }
}
