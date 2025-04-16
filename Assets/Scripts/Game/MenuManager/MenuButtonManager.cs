using Game.Configs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.MenuManager
{
    public class MenuButtonManager : MonoBehaviour
    {
    
        [SerializeField] private MenuButton _attackMenuButton;
        [SerializeField] private MenuButton _MapButton;
        [SerializeField] private MenuButton _ShopButton;
    
        [SerializeField] private MenuButtonConfig _MenuButtonConfig;
        
        public event UnityAction OnMapClicked;
        public event UnityAction OnShopClicked;
        public event UnityAction OnAttackMenuClicked;
    
        public void Initialize()
        {
            _attackMenuButton.Initialize(_MenuButtonConfig.AttackDefaultSprite, _MenuButtonConfig.ButtonColors);
            _attackMenuButton.SubscribeOnClick(() => OnAttackMenuClicked?.Invoke());
        
            _MapButton.Initialize(_MenuButtonConfig.MapDefaultSprite, _MenuButtonConfig.ButtonColors);
            _MapButton.SubscribeOnClick(() => OnMapClicked?.Invoke());
            
            _ShopButton.Initialize(_MenuButtonConfig.ShopDefaultSprite, _MenuButtonConfig.ButtonColors);
            _ShopButton.SubscribeOnClick(() => OnShopClicked?.Invoke());
            
        }
    }
}