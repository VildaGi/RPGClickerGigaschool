using UnityEngine;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private ClickButtonConfig _buttonConfig;
    
    public void Initialize()
    {
        _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        _clickButton.SubscribeOnClick(ShowClick);
    }
    
    private void ShowClick()
    {
        Debug.Log("Click");
    }
    
}
