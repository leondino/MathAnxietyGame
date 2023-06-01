using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationBranch : Interactable
{
    private bool meditationStarted;
    private bool MeditationHasEnded { get { return meditationStarted & !meditationAudio.isPlaying; } }
    public AudioSource meditationAudio;
    public List<GameObject> sitPlaces = new List<GameObject>();
    public Transform followPointLocation;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (MeditationHasEnded)
        {
            FinishMeditation();
        }
    }

    public override void Interact()
    {
        base.Interact();
    }

    public void StartMeditationSequence()
    {
        StartCoroutine(SitForMeditation());
    }

    private IEnumerator SitForMeditation()
    {
        GameManager.Instance.UIIsActive = true;
        GetComponent<Collider>().enabled = false;

        PlayerControler player = GameManager.Instance.thePlayer.GetComponent<PlayerControler>();
        player.followPoint.transform.position = followPointLocation.position;
        for (int iPlayerCharacter = 0; iPlayerCharacter < player.playerCharacters.Count; iPlayerCharacter++)
        {
            player.playerCharacters[iPlayerCharacter].transform.position = sitPlaces[iPlayerCharacter].transform.position;
        }

        yield return new WaitForSeconds(1);

        PlayMeditation();
    }

    private void PlayMeditation()
    {
        meditationStarted = true;
        GameManager.Instance.backgroundMusic.Pause();
        meditationAudio.Play();
    }

    private void FinishMeditation()
    {
        GetComponent<Collider>().enabled = true;
        GameManager.Instance.UIIsActive = false;
        GameManager.Instance.MeditationCompleted = true;
        GameManager.Instance.backgroundMusic.UnPause();
    }
}
