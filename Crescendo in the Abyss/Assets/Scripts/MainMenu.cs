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

    void Start()
    {
        SetupEventTrigger(startGameText, "LoadScene");
        SetupEventTrigger(quitGameText, "QuitGame");
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

    void Update()
    {

    }
}
