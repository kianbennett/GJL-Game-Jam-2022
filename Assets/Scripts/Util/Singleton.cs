using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> 
{
    private static T _instance;
    private static bool doesNotExist;
  
    protected bool persist;

    public static T Instance 
    {
        get 
        {
            if(_instance == null && !doesNotExist) 
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null) 
                {
                    doesNotExist = true;
                    Debug.Log("[Singleton] No instance of " + typeof(T).ToString() + " found!");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake() 
    {
        if(_instance == null) 
        {
            _instance = this as T; 
            _instance.Init();
            if(persist) 
            {
                DontDestroyOnLoad(gameObject);
            }
        } 
        else if(_instance != this as T) 
        {
            Destroy(gameObject);
            return;
        }
    }

    protected virtual void Init() { }
}