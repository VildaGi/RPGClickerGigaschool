using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private Image _menuImage;
    
    [SerializeField] private MenuButton _attackMenuButton;
    [SerializeField] private MenuButton _skillsMenuButton;
    [SerializeField] private MenuButton _InventoryMenuButton;
    [SerializeField] private StopGameButton _StopGameButton;
    
    [SerializeField] private MenuButtonConfig _MenuButtonConfig;
    
    public event UnityAction OnPauseGameClicked;
    
    public void Initialize()
    {
        _attackMenuButton.Initialize(_MenuButtonConfig.AttackDefaultSprite, _MenuButtonConfig.ButtonColors, _MenuButtonConfig.AttackSelectedSprite);
        _attackMenuButton.SubscribeOnClick(AttackMenuClick);
        
        _skillsMenuButton.Initialize(_MenuButtonConfig.SkillsDefaultSprite, _MenuButtonConfig.ButtonColors, _MenuButtonConfig.SkillsSelectedSprite);
        _skillsMenuButton.SubscribeOnClick(SkillsMenuClick);
        
        _InventoryMenuButton.Initialize(_MenuButtonConfig.InventoryDefaultSprite, _MenuButtonConfig.ButtonColors, _MenuButtonConfig.InventorySelectedSprite);
        _InventoryMenuButton.SubscribeOnClick(InventoryMenuClick);
        
        _StopGameButton.Initialize(_MenuButtonConfig.PauseButtonSprite, _MenuButtonConfig.ResumeButtonSprite, _MenuButtonConfig.ButtonColors);
        _StopGameButton.SubscribeOnClick(() => OnPauseGameClicked?.Invoke());
        _StopGameButton.SubscribeOnClick(_StopGameButton.ChangeImage);
    }
    private void InventoryMenuClick()
    {
        _menuImage.sprite = _InventoryMenuButton._selectedImage;
    }

    private void AttackMenuClick()
    {
        _menuImage.sprite = _attackMenuButton._selectedImage;
    }
    private void SkillsMenuClick()
    {
        _menuImage.sprite = _skillsMenuButton._selectedImage;
    }
}