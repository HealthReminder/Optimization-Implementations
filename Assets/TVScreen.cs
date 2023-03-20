using UnityEngine;
using TMPro;

public class TVScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screenobj;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private bool _isOn;
    [SerializeField] private string _currentText = "";
    public void ChangeText(string newText)
    {
        _textMesh.text = newText;
        _currentText = newText;
    }
    [ContextMenu("Toggle")]
    public void Toggle()
    {
        _isOn = !_isOn;
        if (_isOn)
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
