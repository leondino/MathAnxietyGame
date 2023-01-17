using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int START_MATH_ANXIETY = 100;

    public ManageStrengths strenghtsManager;
    public ManageEmotions emotionManager;
    public AnxietyBar mathAnxietyBar;
    public GameObject endScreen;

    public bool UIIsActive { get; set; }

    public int mathAnxietyLevel;
    public GameObject thePlayer;

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

    // Initsialize Game Manager instance and Math Anxiety level in start
    void Start()
    {
        _instance = this;
        mathAnxietyLevel = START_MATH_ANXIETY;
        mathAnxietyBar.SetMaxMathAnxiety(START_MATH_ANXIETY);
    }

    /// <summary>
    /// Reduces Math Anxiety levels based on a given parameter
    /// </summary>
    /// <param name="mathAnxietyReduction">Amount of Math Anxiety that gets reduced</param>
    public void ReduceMathAnxiety(int mathAnxietyReduction)
    {
        mathAnxietyLevel -= mathAnxietyReduction;
        mathAnxietyBar.SetMathAnxiety(mathAnxietyLevel);
    }

    /// <summary>
    /// Ends the game to end game screen and fills in all data
    /// </summary>
    public void EndGame()
    {
        //Fill in all data for EndScreen (with EndScreen script or something)

        UIIsActive = true;
        endScreen.SetActive(true);
    }
}
