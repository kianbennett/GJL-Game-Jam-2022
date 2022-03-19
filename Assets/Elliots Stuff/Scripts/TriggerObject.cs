using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private TriggerReceiver[] Receivers;
    [SerializeField] private bool ShowCutscene = true;
    [SerializeField] private float CutsceneDelay = 0.7f;
    [SerializeField] private float CutsceneDuration = 1.2f;
    [SerializeField] private Level LevelToReveal;

    private bool HasPlayedCutscene = false;

    private bool _active;
    public bool active 
    {
        get { return _active; }
        set
        { 
            if(value == _active) return;
            if(value) 
            {
                if(ShowCutscene && !HasPlayedCutscene)
                {
                    CameraController.Instance.StartCutscene(CutsceneDelay, CutsceneDuration, delegate { 
                        foreach(TriggerReceiver Receiver in Receivers) Receiver.Activate();
                        if(LevelToReveal) LevelToReveal.ShowLevel(1.0f);
                    }, Receivers.Select(o => o.transform.position).ToArray());
                    HasPlayedCutscene = true;
                }
                else
                {
                    foreach(TriggerReceiver Receiver in Receivers) Receiver.Activate();
                }
            }
            else
            {
                foreach(TriggerReceiver Receiver in Receivers) Receiver.Deactivate();
            }
            _active = value;
        }
    }
}
