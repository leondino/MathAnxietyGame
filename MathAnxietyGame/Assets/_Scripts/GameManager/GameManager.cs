using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{
    public const int START_MATH_ANXIETY = 100;

    public ManageStrengths strenghtsManager;
    public ManageEmotions emotionManager;
    public AnxietyBar mathAnxietyBar;
    public GameObject endScreen, pauseScreen;
    public AudioSource backgroundMusic;
    private bool afterLoad = true;

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

        //Resets language to current one to update endscreen language
        StartCoroutine(SetLanguage(LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale)));
    }

    private void Update()
    {
        if (afterLoad && Time.fixedTime > 5)
        {
            //Set language at start to load default for text not connected to localization tables (endScreen)
            StartCoroutine(SetLanguage(LocalizationSettings.AvailableLocales.
                Locales.IndexOf(LocalizationSettings.SelectedLocale)));
            afterLoad = false;
        }
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
        // EndScreen data gets filled in through EndDataFiller script
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
        // Doesn't pause game when Another UI screen is active
        if (UIIsActive & !pauseScreen.activeSelf)
            return;

        pauseScreen.transform.parent.gameObject.SetActive(!pauseScreen.activeSelf);
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        UIIsActive = pauseScreen.activeSelf;
    }

    /// <summary>
    /// Changes the language of the game to the next language in line.
    /// </summary>
    public void ChangeLanguage()
    {
        int availableLanguages = LocalizationSettings.AvailableLocales.Locales.Count;
        int nextLocaleID = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale) + 1;
        if (nextLocaleID >= availableLanguages)
            nextLocaleID = 0;
        StartCoroutine(SetLanguage(nextLocaleID));
    }

    IEnumerator SetLanguage(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];

        // Change endData screen static text to English (Switch case for potential future languages)
        EndDataFiller endData = endScreen.GetComponent<EndDataFiller>();

        switch (localeID)
        {
            default:
                Debug.Log("No language found");
                break;
            case 0:
                endData.language = EndDataFiller.Language.Dutch;
                break;
            case 1:
                endData.language = EndDataFiller.Language.English;
                break;
        }
    }
}
