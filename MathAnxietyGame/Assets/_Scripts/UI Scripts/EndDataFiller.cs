using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDataFiller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI positiveEmotionData, strengthsData;
    private ManageEmotions emotionManager;
    private ManageStrengths strengthManager;
    public bool translateToEnglish = false;

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

        if (translateToEnglish) 
        {
            data += "Experienced positive emotions:\n";
            foreach (GameObject emotion in emotionManager.finalExperiencedEmotions)
            {
                data += $"{emotion.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += "\n\nNon-experienced positive emotions:\n";
            foreach (GameObject emotion in emotionManager.finalNonExperiencedEmotions)
            {
                data += $"{emotion.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += $"\n\nDiscussed experienced emotion: {emotionManager.experiencedDiscussEmotion.name}\n\n";
            foreach (QnA QnA in emotionManager.experiencedQnA)
            {
                data += $"{QnA.question}\n";
                data += $"{QnA.answer}\n\n";
            }
            data += $"Discussed non-experienced emotion: {emotionManager.nonExperiencedDiscussEmotion.name}\n\n";
            foreach (QnA QnA in emotionManager.nonExperiencedQnA)
            {
                data += $"{QnA.question}\n";
                data += $"{QnA.answer}\n\n";
            }
        }
        else
        {
            data += "Wel ervaren positieve emoties:\n";
            foreach (GameObject emotion in emotionManager.finalExperiencedEmotions)
            {
                data += $"{emotion.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += "\n\nNiet ervaren positieve emoties:\n";
            foreach (GameObject emotion in emotionManager.finalNonExperiencedEmotions)
            {
                data += $"{emotion.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += $"\n\nBesproken wel ervaren emotie: {emotionManager.experiencedDiscussEmotion.name}\n\n";
            foreach (QnA QnA in emotionManager.experiencedQnA)
            {
                data += $"{QnA.question}\n";
                data += $"{QnA.answer}\n\n";
            }
            data += $"Besproken niet ervaren emotie: {emotionManager.nonExperiencedDiscussEmotion.name}\n\n";
            foreach (QnA QnA in emotionManager.nonExperiencedQnA)
            {
                data += $"{QnA.question}\n";
                data += $"{QnA.answer}\n\n";
            }
        }

        return data;
    }

    private string StrengthsData()
    {
        string data = "";

        if (translateToEnglish)
        {
            data += "Your top 3 strengths:\n";
            foreach (Strength strength in strengthManager.learnedStrengths)
            {
                data += $"{strength.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += "\n\nAssignments based on your strengths:\n";
            foreach (Strength strength in strengthManager.learnedStrengths)
            {
                data += $"\n{strength.name}:\n{string.Concat(strength.tasksDialogue[0].dialogueSentences)}\n";
            }
        }
        else 
        {
            data += "Jullie top 3 sterke kanten:\n";
            foreach (Strength strength in strengthManager.learnedStrengths)
            {
                data += $"{strength.name}, ";
            }
            data = data.TrimEnd(',', ' ');
            data += "\n\nOpdrachten op basis van jullie sterke kanten:\n";
            foreach (Strength strength in strengthManager.learnedStrengths)
            {
                data += $"\n{strength.name}:\n{string.Concat(strength.tasksDialogue[0].dialogueSentences)}\n";
            }
        }

        return data;
    }

}
