using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    private bool valveOn;
    private Animator anim;
    private bool steamInRange;
    private bool springInRange;
    private bool shrinkInRange;
    private int activePlayer;
    private void Start()
    {
        anim = GetComponent<Animator>();
        valveOn = false;
        this.GetComponent<TriggerObject>().active = false;
    }
    public void TurnValve()
    {
        if (valveOn)
        {
            ////Turn off valve
            //valveOn = false;
            //anim.SetTrigger("valveOff");
            //this.GetComponent<TriggerObject>().active = false;
        }
        else
        {
            //Turn on lever
            valveOn = true;
            anim.SetTrigger("valveOn");
            this.GetComponent<TriggerObject>().active = true;
        }
    }
    private void Update()
    {
        activePlayer = GameObject.Find("PlayerController").GetComponent<PlayerController>().ActiveCharacter;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activePlayer == 0 && springInRange)
            {
                TurnValve();
            }
            if (activePlayer == 1 && steamInRange)
            {
                TurnValve();
            }
            if (activePlayer == 2 && shrinkInRange)
            {
                TurnValve();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (activePlayer == 0)
            {
                springInRange = true;
            }
            if (activePlayer == 1)
            {
                steamInRange = true;
            }
            if (activePlayer == 2)
            {
                shrinkInRange = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (activePlayer == 0)
            {
                springInRange = false;
            }
            if (activePlayer == 1)
            {
                steamInRange = false;
            }
            if (activePlayer == 2)
            {
                shrinkInRange = false;
            }
        }
    }
}