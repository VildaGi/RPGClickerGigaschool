using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private ClickButtonConfig _buttonConfig;

    public event UnityAction onClicked;
    public void Initialize()
    {
        _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        _clickButton.SubscribeOnClick(() => onClicked?.Invoke());
    }

    public void DisableButtons()
    {
        _clickButton.GetComponent<Button>().interactable = false;
    }

    public void EnableButtons()
    {
        _clickButton.GetComponent<Button>().interactable = true;
    }
}
