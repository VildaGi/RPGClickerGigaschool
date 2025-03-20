using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private Sprite _currentLevel;
        [SerializeField] private Sprite _passedLevel;
        [SerializeField] private Sprite _closedtLevel;

        public void Initialize(int LevelNumber, PinType pinType, UnityAction clickCallback)
        {
            _text.text = $"Ур. {LevelNumber}";

            _image.sprite = pinType switch
            {
                PinType.Closed => _closedtLevel,
                PinType.Current => _currentLevel,
                PinType.Passed => _passedLevel
            };

            _button.onClick.AddListener(() =>  clickCallback?.Invoke());
            
        }
    }
}