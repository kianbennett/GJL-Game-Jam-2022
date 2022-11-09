using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Valve : MonoBehaviour
{
    [SerializeField] private Color OutlineColourOn, OutlineColourOff;
    [SerializeField] private Outline Outline;

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

    private PlayerControls Controls;
    private InputAction InputActionActivate;

    void OnEnable()
    {
        Controls = new PlayerControls();
        InputActionActivate = Controls.Player.Activate;
        InputActionActivate.Enable();
        InputActionActivate.performed += delegate 
        {
            if(PlayerController.Instance.HasStarted && IsActivePlayerInRange())
            {
                TurnValve();
            }
        };
    }

    void OnDisable()
    {
        if(InputActionActivate != null) InputActionActivate.Disable();
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
        Outline.enabled = IsActivePlayerInRange();
        Outline.OutlineColor = valveOn ? OutlineColourOn : OutlineColourOff;
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
    private bool IsActivePlayerInRange()
    {
        return (activePlayer == 0 && springInRange)
            || (activePlayer == 1 && steamInRange)
            || (activePlayer == 2 && shrinkInRange);
    }
}