using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour
{
    public TextMeshProUGUI quitText;

    void Start()
    {
        SetupEventTrigger(quitText, "Quit");
    }

    void SetupEventTrigger(TextMeshProUGUI text, string methodName)
    {
        EventTrigger eventTrigger = text.gameObject.GetComponent<EventTrigger>() ?? text.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Invoke(methodName, 0f); });
        eventTrigger.triggers.Add(entry);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
