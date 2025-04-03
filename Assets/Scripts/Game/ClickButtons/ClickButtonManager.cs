using System;
using Game.Configs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButtons
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private ClickButton _clickButton;
        [SerializeField] private ElementalClickButton _fireButton;
        [SerializeField] private ElementalClickButton _airButton;
        [SerializeField] private ElementalClickButton _waterButton;
        [SerializeField] private ElementalClickButton _rockButton;
        [SerializeField] private ClickButtonConfig _buttonConfig;

        public event UnityAction OnClicked;
        public event UnityAction OnFireClicked;
        public event UnityAction OnAirClicked;
        public event UnityAction OnRockClicked;
        public event UnityAction OnWaterClicked;
        public void Initialize()
        {
            _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
            _fireButton.Initialize(_buttonConfig.FireSprite, _buttonConfig.FireSwordButtonSprite, _buttonConfig.ButtonColors);
            _waterButton.Initialize(_buttonConfig.WaterSprite, _buttonConfig.WaterSwordButtonSprite,_buttonConfig.ButtonColors);
            _rockButton.Initialize(_buttonConfig.RockSprite, _buttonConfig.RockSwordButtonSprite,_buttonConfig.ButtonColors);
            _airButton.Initialize(_buttonConfig.AirSprite, _buttonConfig.AirSwordButtonSprite,_buttonConfig.ButtonColors);
            
            
            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
            
            _fireButton.SubscribeOnClick(() => OnFireClicked?.Invoke());
            _fireButton.SubscribeOnClick(() => ChangeClickButtonSprite(_fireButton.GetSwordSprite()));
            
            _airButton.SubscribeOnClick(() => OnAirClicked?.Invoke());
            _airButton.SubscribeOnClick(() => ChangeClickButtonSprite(_airButton.GetSwordSprite()));
            
            _rockButton.SubscribeOnClick(() => OnRockClicked?.Invoke());
            _rockButton.SubscribeOnClick(() => ChangeClickButtonSprite(_rockButton.GetSwordSprite()));
            
            _waterButton.SubscribeOnClick(() => OnWaterClicked?.Invoke());
            _waterButton.SubscribeOnClick(() => ChangeClickButtonSprite(_waterButton.GetSwordSprite()));
        }

        public void DisableButtons()
        {
            _clickButton.GetComponent<Button>().interactable = false;
        }

        public void EnableButtons()
        {
            _clickButton.GetComponent<Button>().interactable = true;
        }

        public void ChangeClickButtonSprite(Sprite sprite)
        {
            if (_clickButton.GetComponent<Image>().sprite == sprite)
            {
                _clickButton.GetComponent<Image>().sprite = _buttonConfig.DefaultSprite;
            }
            else
            {
                _clickButton.GetComponent<Image>().sprite = sprite;
            }
        }
    }
}
