using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public GameObject tutorialScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Toggles tutorial screen on or off.
    /// </summary>
    public void ToggleTutorial()
    {
        tutorialScreen.SetActive(!tutorialScreen.activeSelf);
        tutorialScreen.transform.parent.gameObject.SetActive(tutorialScreen.activeSelf);
    }

    /// <summary>
    /// Starts game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Closes game application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
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
    }
}
