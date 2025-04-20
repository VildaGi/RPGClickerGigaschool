using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButtons
{
    public class ElementalClickButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        private Sprite _changeableSprite;

        public void Initialize(Sprite sprite, Sprite changeableSprite, ColorBlock colorBlock)
        {
            _image.sprite = sprite;
            _button.colors = colorBlock;
            _changeableSprite = changeableSprite;
        }

        public Sprite GetSwordSprite()
        {
            return _changeableSprite;
        }
        
        public void SubscribeOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void UnsubscribeOnClick(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }
    }
}