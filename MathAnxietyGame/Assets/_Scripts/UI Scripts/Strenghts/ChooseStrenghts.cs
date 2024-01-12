using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseStrenghts : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> selectedStrenghts = new List<GameObject>();

    public StrengthCheckTeacher StrengthTeacher { get; set; }

    private bool firstTimeSelected = true;
    private ColorBlock nonSelectedColor;

    /// <summary>
    /// Adds or removes the selected strengt to the list of chosen strenghts.
    /// Changes the color of the button to visualize selection.
    /// </summary>
    public void SelectStrenght()
    {
        GameObject selectedStrenght = EventSystem.current.currentSelectedGameObject;

        if (firstTimeSelected)
        {
            nonSelectedColor = selectedStrenght.GetComponent<Button>().colors;
            firstTimeSelected = false;
        }

        // Only select when strenght isn't selected yet and less then 3 strenghts are selected
        if (!selectedStrenghts.Contains(selectedStrenght) && selectedStrenghts.Count < 3)
        {
            // Highlight color
            ColorBlock selectedColor = nonSelectedColor;
            selectedColor.normalColor = Color.green;
            selectedColor.selectedColor = Color.green;
            selectedStrenght.GetComponent<Button>().colors = selectedColor;

            selectedStrenghts.Add(selectedStrenght);
        }
        else
        {
            // Remove highlight color
            selectedStrenght.GetComponent<Button>().colors = nonSelectedColor;

            selectedStrenghts.Remove(selectedStrenght);
        }
    }

    public void ConfirmSelection()
    {
        if (selectedStrenghts.Count >= 3)
        {
            gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            GameManager.Instance.UIIsActive = false;

            GameManager.Instance.strenghtsManager.ReceiveStrenghtSelection(selectedStrenghts);
        }
        else Debug.Log("Choose 3 strenghts please!");
    }
}
