using UnityEngine;
using UnityEngine.PlayerLoop;
/// <summary>
/// A dialogue interactable that can check if a task is complete or incomplete
/// </summary>
public class TeacherTaskCheck : DialogueInteractable
{
    //! How much Math Anxiety this task should reduce
    public int MathAnxietyReduction;

    //! Quest marker of this teacher
    public GameObject questMarker;
    private bool questComplete = false;

    //protected override void Start()
    //{
    //    base.Start();
    //    ShowQuestMarker();
    //}
    private void Update()
    {
        ShowQuestMarker();
    }

    public virtual void CheckTask()
    {
        HasInteraction = false;
    }

    public virtual void Completed()
    {
        // reduce math anxiety meter
        GameManager.Instance.ReduceMathAnxiety(MathAnxietyReduction);
        questComplete = true;
    }

    public virtual void InComplete()
    {
        dialogueTrigger.PreviousDialogue();
        HasInteraction = false;
    }

    /// <summary>
    /// Hides quest marker above teacher
    /// </summary>
    public void HideQuestMarker()
    {
        questMarker.SetActive(false);
    }

    /// <summary>
    /// Shows quest marker above teacher
    /// </summary>
    public void ShowQuestMarker()
    {
        if (!questComplete)
        {
            questMarker.SetActive(true);
            questMarker.GetComponentInChildren<Bilboard>().SetBilboard();
        }
    }

}
