using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionBlockDisplacer : MonoBehaviour
{
    [SerializeField]
    private int forcePower = 150;

    private void DisplaceEmotion(Rigidbody emotion, Vector3 contactPoint)
    {
        Vector3 direction = contactPoint - emotion.transform.position;
        direction *= -forcePower;
        direction.y = 0;
        Debug.Log("Displace push: " + direction);

        emotion.AddForce(direction);
    }

    private Vector3 GetMiddleContact(Collision collision)
    {
        var cornerLeft = collision.GetContact(0).point;
        var cornerRight = collision.GetContact(3).point;
        Debug.Log(cornerLeft + " aand " + cornerRight);
        Vector3 distance = cornerLeft - cornerRight;
        Vector3 middle = cornerLeft - distance / 2;
        return middle;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Emotion"))
        {
            DisplaceEmotion(collision.gameObject.GetComponent<Rigidbody>(),
                GetMiddleContact(collision));
        }
    }
}
