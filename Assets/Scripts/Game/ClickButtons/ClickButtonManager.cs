using System;
using Game.Configs;
using Game.Elements;
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

        [SerializeField] private CooldawnTimer _AirTimer;
        [SerializeField] private CooldawnTimer _FireTimer;
        [SerializeField] private CooldawnTimer _WaterTimer;
        [SerializeField] private CooldawnTimer _RockTimer;
        
        public event UnityAction OnClicked;
        public event UnityAction OnFireClicked;
        public event UnityAction FireActive;
        public event UnityAction OnAirClicked;
        public event UnityAction AirActive;
        public event UnityAction OnRockClicked;
        public event UnityAction RockActive;
        public event UnityAction OnWaterClicked;
        public event UnityAction WaterActive;
        public void Initialize()
        {
            _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
            _fireButton.Initialize(_buttonConfig.FireSprite, _buttonConfig.FireSwordButtonSprite, _buttonConfig.ButtonColors);
            _waterButton.Initialize(_buttonConfig.WaterSprite, _buttonConfig.WaterSwordButtonSprite,_buttonConfig.ButtonColors);
            _rockButton.Initialize(_buttonConfig.RockSprite, _buttonConfig.RockSwordButtonSprite,_buttonConfig.ButtonColors);
            _airButton.Initialize(_buttonConfig.AirSprite, _buttonConfig.AirSwordButtonSprite,_buttonConfig.ButtonColors);
            
            
            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
            
            _fireButton.SubscribeOnClick(() => OnFireClicked?.Invoke());
            _fireButton.SubscribeOnClick(() => ChangeClickButtonSprite(_fireButton.GetSwordSprite(), ElementType.Fire));
            
            _airButton.SubscribeOnClick(() => OnAirClicked?.Invoke());
            _airButton.SubscribeOnClick(() => ChangeClickButtonSprite(_airButton.GetSwordSprite(), ElementType.Air));
            
            _rockButton.SubscribeOnClick(() => OnRockClicked?.Invoke());
            _rockButton.SubscribeOnClick(() => ChangeClickButtonSprite(_rockButton.GetSwordSprite(), ElementType.Rock));
            
            _waterButton.SubscribeOnClick(() => OnWaterClicked?.Invoke());
            _waterButton.SubscribeOnClick(() => ChangeClickButtonSprite(_waterButton.GetSwordSprite(), ElementType.Water));
        }

        public void DisableButtons()
        {
            _clickButton.GetComponent<Button>().interactable = false;
        }

        public void EnableButtons()
        {
            _clickButton.GetComponent<Button>().interactable = true;
        }

        public void ChangeClickButtonSprite(Sprite sprite, ElementType elementType)
        {
            if (_clickButton.GetComponent<Image>().sprite == sprite)
            {
                _clickButton.GetComponent<Image>().sprite = _buttonConfig.DefaultSprite;
                ActivateSkill(elementType);
            }
            else
            {
                _clickButton.GetComponent<Image>().sprite = sprite;
            }
        }

        private void ActivateSkill(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Fire:
                    FireActive?.Invoke();
                    _fireButton.GetComponent<Button>().interactable = false;
                    _FireTimer.SetValue(4f);
                    _FireTimer.OnTimerEnd += () => _fireButton.GetComponent<Button>().interactable = true;
                    break;
                case ElementType.Water:
                    WaterActive?.Invoke();
                    _waterButton.GetComponent<Button>().interactable = false;
                    _WaterTimer.SetValue(4f);
                    _WaterTimer.OnTimerEnd += () => _waterButton.GetComponent<Button>().interactable = true;
                    break;
                case ElementType.Air:
                    AirActive?.Invoke();
                    _airButton.GetComponent<Button>().interactable = false;
                    _AirTimer.SetValue(4f);
                    _AirTimer.OnTimerEnd += () => _airButton.GetComponent<Button>().interactable = true;
                    break;
                case ElementType.Rock:
                    RockActive?.Invoke();
                    _rockButton.GetComponent<Button>().interactable = false;
                    _RockTimer.SetValue(8f);
                    _RockTimer.OnTimerEnd += () => _rockButton.GetComponent<Button>().interactable = true;
                    break;
            }
        }
    }
}
