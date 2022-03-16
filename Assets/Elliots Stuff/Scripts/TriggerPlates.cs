using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlates : MonoBehaviour
{
    public TriggerObject trigger;
    public PressurePlate plate1;
    public PressurePlate plate2;
    public PressurePlate plate3;

    private void Start()
    {
        trigger = GetComponent<TriggerObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if(plate1.active == true && plate2.active == true && plate3.active == true)
        {
            trigger.active = true;
        }
    }
}
