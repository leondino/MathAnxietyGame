using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class EndDataFiller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI positiveEmotionData, strengthsData;
    private ManageEmotions emotionManager;
    private ManageStrengths strengthManager;
    // Language enum (for future proof new languages)
    public enum Language { Dutch, English};
    public Language language;

    // Awake is called when this object is enabled
    void Awake()
    {
        emotionManager = GameManager.Instance.emotionManager;
        strengthManager = GameManager.Instance.strenghtsManager;
        FillEndData();
    }

    /// <summary>
    /// Fills in all the data saved in the game manager into the text fields of the End Screen
    /// </summary>
    private void FillEndData()
    {
        // Add Positive Emotion data
        positiveEmotionData.text = PositiveEmotionData();

        // Add Strengths data
        strengthsData.text = StrengthsData();
    }

    private string PositiveEmotionData()
    {
        string data = "";

        switch (language)
        {
            case Language.Dutch:
                data += "Wel ervaren positieve emoties:\n";
                foreach (GameObject emotion in emotionManager.finalExperiencedEmotions)
                {
                    data += $"{emotion.GetComponentInChildren<TextMeshProUGUI>().text}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += "\n\nNiet ervaren positieve emoties:\n";
                foreach (GameObject emotion in emotionManager.finalNonExperiencedEmotions)
                {
                    data += $"{emotion.GetComponentInChildren<TextMeshProUGUI>().text}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += $"\n\nBesproken wel ervaren emotie: {emotionManager.experiencedDiscussEmotion.GetComponentInChildren<TextMeshProUGUI>().text}\n\n";
                foreach (QnA QnA in emotionManager.experiencedQnA)
                {
                    data += $"{QnA.question}\n";
                    data += $"{QnA.answer}\n\n";
                }
                data += $"Besproken niet ervaren emotie: {emotionManager.nonExperiencedDiscussEmotion.GetComponentInChildren<TextMeshProUGUI>().text}\n\n";
                foreach (QnA QnA in emotionManager.nonExperiencedQnA)
                {
                    data += $"{QnA.question}\n";
                    data += $"{QnA.answer}\n\n";
                }
                break;
            case Language.English:
                data += "Experienced positive emotions:\n";
                foreach (GameObject emotion in emotionManager.finalExperiencedEmotions)
                {
                    data += $"{emotion.GetComponentInChildren<TextMeshProUGUI>().text}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += "\n\nNon-experienced positive emotions:\n";
                foreach (GameObject emotion in emotionManager.finalNonExperiencedEmotions)
                {
                    data += $"{emotion.GetComponentInChildren<TextMeshProUGUI>().text}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += $"\n\nDiscussed experienced emotion: {emotionManager.experiencedDiscussEmotion.GetComponentInChildren<TextMeshProUGUI>().text}\n\n";
                foreach (QnA QnA in emotionManager.experiencedQnA)
                {
                    data += $"{QnA.question}\n";
                    data += $"{QnA.answer}\n\n";
                }
                data += $"Discussed non-experienced emotion: {emotionManager.nonExperiencedDiscussEmotion.GetComponentInChildren<TextMeshProUGUI>().text}\n\n";
                foreach (QnA QnA in emotionManager.nonExperiencedQnA)
                {
                    data += $"{QnA.question}\n";
                    data += $"{QnA.answer}\n\n";
                }
                break;
            default:
                Debug.Log("No language found");
                break;
        }

        return data;
    }

    private string StrengthsData()
    {
        string data = "";

        switch (language)
        {
            case Language.Dutch:
                data += "Jullie top 3 sterke kanten:\n";
                foreach (Strength strength in strengthManager.learnedStrengths)
                {
                    data += $"{strength.name.GetLocalizedString()}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += "\n\nOpdrachten op basis van jullie sterke kanten:\n";
                foreach (Strength strength in strengthManager.learnedStrengths)
                {
                    data += $"\n{strength.name.GetLocalizedString()}:" +
                        $"\n{string.Concat(strength.tasksDialogue[0].dialogueSentences)}\n";
                }
                break;
            case Language.English:
                data += "Your top 3 strengths:\n";
                foreach (Strength strength in strengthManager.learnedStrengths)
                {
                    data += $"{strength.name.GetLocalizedString()}, ";
                }
                data = data.TrimEnd(',', ' ');
                data += "\n\nAssignments based on your strengths:\n";
                foreach (Strength strength in strengthManager.learnedStrengths)
                {
                    data += $"\n{strength.name.GetLocalizedString()}:" +
                        $"\n{string.Concat(strength.tasksDialogue[0].dialogueSentences)}\n";
                }
                break;
            default:
                Debug.Log("No language found");
                break;
        }
        
        return data;
    }

}
