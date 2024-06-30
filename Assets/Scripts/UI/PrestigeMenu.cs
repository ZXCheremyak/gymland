using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrestigeMenu : MonoBehaviour
{
    [SerializeField] GameObject prestigeMenu, requirementText;
    int nextPrestigeRequirement = 2500;

    void Start()
    {
        EventManager.powerChanged.AddListener(ChangeRequirementText);
        ChangeRequirementText();
    }

    public void OpenPrestigeMenu()
    {
        prestigeMenu.SetActive(true);
    }

    public void ClosePrestigeMenu()
    {
        prestigeMenu?.SetActive(false);
    }

    public void AcceptPrestige()
    {
        if (Parameters.power < nextPrestigeRequirement) return;
        Parameters.power = 0;
        Parameters.prestigeMultiplier += 0.1f;
        nextPrestigeRequirement *= 8;
        EventManager.powerChanged.Invoke();
    }


    void ChangeRequirementText() 
    {
        requirementText.GetComponent<TextMeshProUGUI>().text = Parameters.power.ToString() + " / " +nextPrestigeRequirement.ToString();
    }
}
