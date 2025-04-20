using Game.Configs;
using Game.MenuManager;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Menu
{
    public class MetaMenuButtonManager : MonoBehaviour
    {
        [SerializeField] private MenuButton _MapButton;
        [SerializeField] private MenuButton _ShopButton;
    
        [SerializeField] private MenuButtonConfig _MenuButtonConfig;
        
        public event UnityAction OnMapClicked;
        public event UnityAction OnShopClicked;
    
        public void Initialize()
        {
            _MapButton.Initialize(_MenuButtonConfig.MapDefaultSprite, _MenuButtonConfig.ButtonColors);
            _MapButton.SubscribeOnClick(() => OnMapClicked?.Invoke());
            
            _ShopButton.Initialize(_MenuButtonConfig.ShopDefaultSprite, _MenuButtonConfig.ButtonColors);
            _ShopButton.SubscribeOnClick(() => OnShopClicked?.Invoke());
            
        }
    }
}