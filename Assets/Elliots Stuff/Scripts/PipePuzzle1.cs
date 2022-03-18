using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle1 : MonoBehaviour
{
    public TriggerObject valve1;
    public TriggerObject cornerPipe;
    public TriggerObject tPipe;
    public TriggerObject valve2;
    public TriggerObject turbine;
    private TriggerObject thisTrigger;

    public GameObject steam2;
    public GameObject steam3;
    public GameObject steam4;
    // Start is called before the first frame update
    void Start()
    {
        thisTrigger = GetComponent<TriggerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(valve1.active == true && cornerPipe.active == true && tPipe.active == true && valve2.active == true && turbine == true)
        {
            thisTrigger.active = true;
        }
        if(cornerPipe.active == false)
        {
            steam3.SetActive(false);
            steam4.SetActive(false);
        }
        if (cornerPipe.active == true)
        {
            if (tPipe.active == false)
            {
                steam3.SetActive(true);
                steam4.SetActive(false);
            }
            if (tPipe.active == true)
            {
                steam3.SetActive(false);
                steam4.SetActive(true);
            }
        }
    }
}
