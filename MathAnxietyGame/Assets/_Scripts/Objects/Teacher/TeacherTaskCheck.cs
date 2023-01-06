/// <summary>
/// A dialogue interactable that can check if a task is complete or incomplete
/// </summary>
public class TeacherTaskCheck : DialogueInteractable
{
    //! How much Math Anxiety this task should reduce
    public int MathAnxietyReduction;

    public virtual void CheckTask()
    {
        HasInteraction = false;
    }

    public virtual void Completed()
    {
        // reduce math anxiety meter
        GameManager.Instance.ReduceMathAnxiety(MathAnxietyReduction);
    }

    public virtual void InComplete()
    {
        dialogueTrigger.PreviousDialogue();
        HasInteraction = false;
    }
}
