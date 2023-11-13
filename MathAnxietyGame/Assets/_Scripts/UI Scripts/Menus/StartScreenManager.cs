using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
