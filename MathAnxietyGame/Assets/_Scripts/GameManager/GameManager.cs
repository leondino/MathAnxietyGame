using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public const int START_MATH_ANXIETY = 100;

    public ManageStrengths strenghtsManager;
    public ManageEmotions emotionManager;
    public AnxietyBar mathAnxietyBar;
    public GameObject endScreen, pauseScreen;
    public AudioSource backgroundMusic;

    public bool UIIsActive { get; set; }
    //! Saves if the meditation has been completed
    public bool MeditationCompleted { get; set; }

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
        MeditationCompleted = false;
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

    /// <summary>
    /// Closed the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnPauseGame(InputAction.CallbackContext value)
    {
        if (value.started && backgroundMusic.isPlaying)
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        pauseScreen.transform.parent.gameObject.SetActive(!pauseScreen.activeSelf);
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        UIIsActive = pauseScreen.activeSelf;
    }
}
