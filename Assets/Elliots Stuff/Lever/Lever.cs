using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] private Color OutlineColourOn, OutlineColourOff;
    [SerializeField] private Outline Outline;

    private bool leverOn;
    private Animator anim;
    private bool steamInRange;
    private bool springInRange;
    private bool shrinkInRange;
    private int activePlayer;

    private PlayerControls Controls;
    private InputAction InputActionActivate;

    private void Start()
    {
        anim = GetComponent<Animator>();
        leverOn = false;
        // this.GetComponent<TriggerObject>().active = false;
    }

    void OnEnable()
    {
        Controls = new PlayerControls();
        InputActionActivate = Controls.Player.Activate;
        InputActionActivate.Enable();
        InputActionActivate.performed += delegate 
        {
            if(PlayerController.Instance.HasStarted && IsActivePlayerInRange())
            {
                PullLever();
            }
        };
    }

    void OnDisable()
    {
        if(InputActionActivate != null) InputActionActivate.Disable();
    }

    public void PullLever()
    {
        if(leverOn)
        {
            //Turn off lever
            leverOn = false;
            anim.SetTrigger("leverOff");
            this.GetComponent<TriggerObject>().active = false;
        }
        else
        {
            //Turn on lever
            leverOn = true;
            anim.SetTrigger("leverOn");
            this.GetComponent<TriggerObject>().active = true;
        }
        AudioManager.Instance.SfxLever.PlayAsSFX(Random.Range(0.6f, 0.8f));
    }
    private void Update()
    {
        activePlayer = GameObject.Find("PlayerController").GetComponent<PlayerController>().ActiveCharacter;
        Outline.enabled = IsActivePlayerInRange();
        Outline.OutlineColor = leverOn ? OutlineColourOn : OutlineColourOff;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(activePlayer == 0)
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
