using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeditationBranch : Interactable
{
    private bool meditationStarted;
    private bool MeditationHasEnded { get { return (meditationStarted & !meditationAudio.isPlaying) || meditationAudio.mute; } }
    public AudioSource meditationAudio;
    public List<GameObject> sitPlaces = new List<GameObject>();
    public Transform followPointLocation;
    private PlayerControler player;
    private NavMeshObstacle obstacle;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (MeditationHasEnded)
        {
            FinishMeditation();
        }

        // Press '[' to skip mediation during development tests
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            SkipMeditationCheat();
        }
    }

    public override void Interact()
    {
        base.Interact();
    }

    private void SkipMeditationCheat()
    {
        meditationAudio.Stop();
    }

    public void StartMeditationSequence()
    {
        StartCoroutine(SitForMeditation());
    }

    private IEnumerator SitForMeditation()
    {
        GetComponent<Collider>().enabled = false;

        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        player = GameManager.Instance.thePlayer.GetComponent<PlayerControler>();
        player.followPoint.transform.position = followPointLocation.position;

        for (int iPlayerCharacter = 0; iPlayerCharacter < player.playerCharacters.Count; iPlayerCharacter++)
        {
            player.playerCharacters[iPlayerCharacter].GetComponent<NavMeshAgent>().updatePosition = false;
            player.playerCharacters[iPlayerCharacter].GetComponent<NavMeshAgent>().updateRotation = false;
            player.playerCharacters[iPlayerCharacter].transform.position = sitPlaces[iPlayerCharacter].transform.position;
            player.playerCharacters[iPlayerCharacter].transform.rotation = sitPlaces[iPlayerCharacter].transform.rotation;
            player.playerCharacters[iPlayerCharacter].GetComponent<Animator>().SetBool("IsMeditating", true);
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
        foreach (GameObject playerCharacter in player.playerCharacters)
        {
            playerCharacter.GetComponent<Animator>().SetBool("IsMeditating", false);
            playerCharacter.GetComponent<NavMeshAgent>().updatePosition = true;
            playerCharacter.GetComponent<NavMeshAgent>().updateRotation = true;
        }
        GetComponent<Collider>().enabled = true;
        obstacle.enabled = true;

        GameManager.Instance.MeditationCompleted = true;
        GameManager.Instance.backgroundMusic.UnPause();
    }
}