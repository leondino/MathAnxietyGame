using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of when emotions are categorized in this field
/// </summary>
public class EmotionField : MonoBehaviour
{
    [SerializeField]
    private bool isExperiencedField;

    private List<Collider> emotionsInField = new List<Collider>();

    /// <summary>
    /// Adds a given positive emotion to the emotion manager
    /// </summary>
    /// <param name="emotion">Emotion that just got categorized</param>
    private void AddEmotionToManager(GameObject emotion)
    {
        if (isExperiencedField)
            GameManager.Instance.emotionManager.AddExperiencedEmotion(emotion);
        else
            GameManager.Instance.emotionManager.AddNonExperiencedEmotion(emotion);
    }

    /// <summary>
    /// Removes a positive emotion from the emotion manager
    /// </summary>
    /// <param name="emotion">Emotion that just got uncategorized</param>
    private void RemoveEmotionFromManager(GameObject emotion)
    {
        if (isExperiencedField)
            GameManager.Instance.emotionManager.RemoveExperiencedEmotion(emotion);
        else
            GameManager.Instance.emotionManager.RemoveNonExperiencedEmotion(emotion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Emotion"))
        {
            if (!emotionsInField.Contains(other))
            {
                emotionsInField.Add(other);
                AddEmotionToManager(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Emotion"))
        {
            if (emotionsInField.Contains(other))
            {
                Debug.Log("hello remove me");
                emotionsInField.Remove(other);
                RemoveEmotionFromManager(other.gameObject);
            }
        }
    }
}
