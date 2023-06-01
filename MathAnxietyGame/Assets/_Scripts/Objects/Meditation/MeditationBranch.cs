using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationBranch : Interactable
{
    private bool meditationStarted;
    private bool MeditationHasEnded { get { return meditationStarted & !meditationAudio.isPlaying; } }
    public AudioSource meditationAudio;

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
        GameManager.Instance.UIIsActive = false;
        GameManager.Instance.MeditationCompleted = true;
        GameManager.Instance.backgroundMusic.UnPause();
    }
}
