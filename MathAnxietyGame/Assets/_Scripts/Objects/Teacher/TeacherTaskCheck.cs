/// <summary>
/// A dialogue interactable that can check if a task is complete or incomplete
/// </summary>
public class TeacherTaskCheck : DialogueInteractable
{
    public virtual void CheckTask()
    {
        HasInteraction = false;
    }

    public virtual void Completed()
    {
        // reduce math anxiety meter
    }

    public virtual void InComplete()
    {
        dialogueTrigger.PreviousDialogue();
        HasInteraction = false;
    }
}
