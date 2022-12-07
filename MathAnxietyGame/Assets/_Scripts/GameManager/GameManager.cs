using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ManageStrengths strenghtsManager;
    public ManageEmotions emotionManager;

    public bool UIIsActive { get; set; }

    private static GameManager _instance;

    // Singleton of DialogueManager
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.Log("Game Manager is NULL");
            return _instance;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

}
