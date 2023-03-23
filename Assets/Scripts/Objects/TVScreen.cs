using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TVScreen : MonoBehaviour
{
    public bool IsOn = false;
    [SerializeField] private GameObject _screenobj;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private string _currentText = "";
    [SerializeField] private UnityEvent[] OnChangeTextEvents;
    public void ChangeText(string newText)
    {
        _textMesh.text = newText;
        _currentText = newText;
        foreach (UnityEvent e in OnChangeTextEvents)
            e.Invoke();
        
    }
    [ContextMenu("Toggle")]
    public void Toggle()
    {
        IsOn = !IsOn;
        if (IsOn)
            TurnOn();
        else
            TurnOff();
    }
    [ContextMenu("Turn ON")]
    public void TurnOn()
    {
        _screenobj.SetActive(true);
    }
    [ContextMenu("Turn OFF")]
    public void TurnOff()
    {
        _screenobj.SetActive(false);

    }

}
