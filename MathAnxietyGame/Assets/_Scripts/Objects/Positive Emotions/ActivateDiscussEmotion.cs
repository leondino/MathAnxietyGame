using System.Collections;
using UnityEngine;

/// <summary>
/// Keeps track of 1 emotion that should be further discussed.
/// Makes the emotion fly and select it in the manager
/// </summary>
public class ActivateDiscussEmotion : MonoBehaviour
{
    [SerializeField]
    private GameObject discussEmotion;
    private Vector3 floatingPosition;
    [SerializeField]
    private float emotionRotationSpeed = 1f, floatHeight = 3f, ascendSpeed = 0.1f;
    private float myRotationSpeed = 0;
    [SerializeField]
    private bool isExperiencedEmotion;

    /// <summary>
    /// Checks if the emotion is fitted in the right room.
    /// </summary>
    private bool EmotionIsViable
    {
        get
        {
            return isExperiencedEmotion && GameManager.Instance.emotionManager.finalExperiencedEmotions.Contains(discussEmotion)
            || !isExperiencedEmotion && GameManager.Instance.emotionManager.finalNonExperiencedEmotions.Contains(discussEmotion);
        }
    }

    // Set floating position
    private void Start()
    {
        floatingPosition = transform.position;
        floatingPosition.y += floatHeight;
    }

    // Make emotion rotate
    void FixedUpdate()
    {
        if (discussEmotion != null)
        {
            discussEmotion.transform.Rotate(new Vector3(0, myRotationSpeed, 0));
        }
    }

    /// <summary>
    /// Makes sure the emotion block can rotate and ascend to the air
    /// </summary>
    private void ActivateEmotion()
    {
        GameManager.Instance.soundManager.playCorrectSound();
        discussEmotion.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(FlyToMid());
        myRotationSpeed = emotionRotationSpeed;
    }

    /// <summary>
    /// Sends discuss emotion to emotion manager and triggers the discussion UI screen
    /// </summary>
    private void DiscussEmotion()
    {
        if (isExperiencedEmotion)
        {
            GameManager.Instance.emotionManager.experiencedDiscussEmotion = discussEmotion;
            GameManager.Instance.emotionManager.StartExperiencedDiscussion();
        }
        else
        {
            GameManager.Instance.emotionManager.nonExperiencedDiscussEmotion = discussEmotion;
            GameManager.Instance.emotionManager.StartNonExperiencedDiscussion();
        }
    }

    /// <summary>
    /// Coroutine that makes the emotion fly to the middle and float in the air, to show it's selected
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlyToMid()
    {
        while ((floatingPosition - discussEmotion.transform.position).magnitude >= 0.1)
        {
            yield return new WaitForFixedUpdate();
            Vector3 transformation = floatingPosition - discussEmotion.transform.position;
            Debug.Log(transformation);
            transformation *= ascendSpeed;
            Debug.Log((floatingPosition - discussEmotion.transform.position).magnitude);
            discussEmotion.transform.position += transformation;
        }
        discussEmotion.transform.position = floatingPosition;
        Debug.Log(discussEmotion);
        DiscussEmotion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Emotion") && discussEmotion == null)
        {
            discussEmotion = other.gameObject;
            if (EmotionIsViable)
                ActivateEmotion();
            else
                discussEmotion = null;
        }
    }
}
