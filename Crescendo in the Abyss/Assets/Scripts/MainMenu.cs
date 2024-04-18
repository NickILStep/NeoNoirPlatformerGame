using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public TextMeshProUGUI startGameText;
    public TextMeshProUGUI quitGameText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI backText;
    public GameObject menu;
    public GameObject instructions;

    void Start()
    {
        SetupEventTrigger(startGameText, "LoadScene");
        SetupEventTrigger(quitGameText, "QuitGame");
        SetupEventTrigger(instructionText, "InstructionPage");
        SetupEventTrigger(backText, "MenuPage");
    }

    void SetupEventTrigger(TextMeshProUGUI text, string methodName)
    {
        EventTrigger eventTrigger = text.gameObject.GetComponent<EventTrigger>() ?? text.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Invoke(methodName, 0f); });
        eventTrigger.triggers.Add(entry);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void InstructionPage()
    {
        menu.SetActive(false);
        instructions.SetActive(true);
    }

    public void MenuPage()
    {
        instructions.SetActive(false);
        menu.SetActive(true);
    }

    void Update()
    {

    }
}
