using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class RestartGame : MonoBehaviour
{
    public TextMeshProUGUI restartText;

    void Start()
    {
        SetupEventTrigger(restartText, "RestartScene");
    }

    void SetupEventTrigger(TextMeshProUGUI text, string methodName)
    {
        EventTrigger eventTrigger = text.gameObject.GetComponent<EventTrigger>() ?? text.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Invoke(methodName, 0f); });
        eventTrigger.triggers.Add(entry);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
