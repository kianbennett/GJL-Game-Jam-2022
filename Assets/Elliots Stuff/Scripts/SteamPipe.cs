using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPipe : MonoBehaviour
{
    public TriggerObject valve;
    public TriggerObject turbine;
    private ParticleSystem parSys;
    // Start is called before the first frame update
    void Start()
    {
        parSys = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(valve.active == true || turbine.active == false)
        {
            parSys.Stop();
        }
        if (valve.active == false && turbine.active == true)
        {
            parSys.Play();
        }
    }
}
