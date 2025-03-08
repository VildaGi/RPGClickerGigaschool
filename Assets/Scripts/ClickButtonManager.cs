using UnityEngine;
using UnityEngine.Events;

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
}
