using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the emotions categorized in the positve emotion (Broaden-Build) task.
/// </summary>
public class ManageEmotions : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> experiencedEmotions = new List<GameObject>();
    [SerializeField]
    private List<GameObject> nonExperiencedEmotions = new List<GameObject>();

    public GameObject experiencedDiscussEmotion, nonExperiencedDiscussEmotion;

    public List<GameObject> finalNonExperiencedEmotions = new List<GameObject>();
    public List<GameObject> finalExperiencedEmotions = new List<GameObject>();

    public List<QnA> experiencedQnA = new List<QnA>();
    public List<QnA> nonExperiencedQnA = new List<QnA>();

    private bool TenEmotionsCategorized { get { return experiencedEmotions.Count + nonExperiencedEmotions.Count == 10; } }
    private bool NoFieldLeftEmpty { get { return experiencedEmotions.Count > 0 && nonExperiencedEmotions.Count > 0; } }

    [SerializeField]
    private GameObject QnAUI;
    [SerializeField]
    private Dialogue experiencedQuestions, nonExperiencedQuestions;

    public void AddExperiencedEmotion(GameObject emotion)
    {
        experiencedEmotions.Add(emotion);
    }
    public void AddNonExperiencedEmotion(GameObject emotion)
    {
        nonExperiencedEmotions.Add(emotion);
    }
    public void RemoveExperiencedEmotion(GameObject emotion)
    {
        Debug.Log("hello remove me (experienced)");
        experiencedEmotions.Remove(emotion);
    }
    public void RemoveNonExperiencedEmotion(GameObject emotion)
    {
        Debug.Log("hello remove me (non-Experienced)");
        nonExperiencedEmotions.Remove(emotion);
    }

    /// <summary>
    /// Checks if all 10 emotions are categorized and returns a true/false value
    /// 
    /// Keeps track of the non experienced emotions at the end of the categorisation period.
    /// This way it can be used later on in the task.
    /// </summary>
    /// <returns></returns>
    public bool CheckAllCategorized()
    {
        if (TenEmotionsCategorized && NoFieldLeftEmpty)
        {
            finalExperiencedEmotions.AddRange(experiencedEmotions);
            finalNonExperiencedEmotions.AddRange(nonExperiencedEmotions);
            Debug.Log("Awheag");
            return true;
        }
        Debug.Log("Not all emotions are catergorized yet");
        return false;
    }

    /// <summary>
    /// Checks if both a non-experienced and experienced emotion have been discussed
    /// </summary>
    /// <returns></returns>
    public bool CheckBothDiscussed()
    {
        return experiencedDiscussEmotion != null && nonExperiencedDiscussEmotion != null;
    }

    /// <summary>
    /// Opens the UI components for discussing the chosen experienced emotion
    /// </summary>
    public void StartExperiencedDiscussion()
    {
        EnableUI();
        QnAUI.GetComponent<QnAInputHandler>().StartQnA(experiencedQuestions, experiencedQnA);
    }

    /// <summary>
    /// Opens the UI components for discussing the chosen non experienced emotion
    /// </summary>
    public void StartNonExperiencedDiscussion()
    {
        EnableUI();
        QnAUI.GetComponent<QnAInputHandler>().StartQnA(nonExperiencedQuestions, nonExperiencedQnA);
    }

    private void EnableUI()
    {
        QnAUI.transform.parent.gameObject.SetActive(true);
        QnAUI.SetActive(true);
        GameManager.Instance.UIIsActive = true;
    }
}
