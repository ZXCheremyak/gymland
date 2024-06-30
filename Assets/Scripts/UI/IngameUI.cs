using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText, powerText;
    bool menuIsActive;
    bool settingsMenuIsActive;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject settingsMenu;

    void Start()
    {
        EventManager.moneyChanged.AddListener(ChangeMoneyText);
        EventManager.powerChanged.AddListener(ChangePowerText);
        ChangeMoneyText();
        ChangePowerText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (menuIsActive)
        {
            menuIsActive = !menuIsActive;
            menu.SetActive(false);
            Time.timeScale = 1;
            if(settingsMenuIsActive)
            {
                settingsMenu.SetActive(false);
                settingsMenuIsActive = false;
            }
        }
        else
        {
            menuIsActive = !menuIsActive;
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        ToggleMenu();
    }

    public void ShowSettings()
    {
        menu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsMenuIsActive = true;
    }

    void ChangeMoneyText()
    {
        moneyText.text = "Money: " + GetShortedNumber(Parameters.money);
    }

    void ChangePowerText()
    {
        powerText.text = "Power: " + GetShortedNumber(Parameters.power);
    }

    string GetShortedNumber(int value)
    {
        string lettersAfterNumber = "";
        float moneyValueForText = value;

        while (moneyValueForText > 1000) 
        {
            moneyValueForText /= 1000;
            lettersAfterNumber += "K";
        }

        return moneyValueForText.ToString("0.##") + lettersAfterNumber;
    }
}
