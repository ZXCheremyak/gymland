using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText, powerText;
    bool menuIsActive;

    GameObject menu;

    void Start()
    {
        EventManager.moneyChanged.AddListener(ChangeMoneyText);
        EventManager.powerChanged.AddListener(ChangePowerText);
        ChangeMoneyText();
        ChangePowerText();
    }

    void Update()
    {

    }

    void ToggleMenu()
    {
        if (menuIsActive)
        {
            menuIsActive = !menuIsActive;
            menu.SetActive(false);
        }
        else
        {
            menuIsActive = !menuIsActive;
            menu.SetActive(true);
        }
    }

    void ExitToMainMenu()
    {

    }

    void Resume()
    {

    }

    void Settings()
    {

    }

    void ChangeMoneyText()
    {
        moneyText.text = "Money: " + Parameters.money.ToString();
    }

    void ChangePowerText()
    {
        powerText.text = "Power: " + Parameters.power.ToString();
    }
}
