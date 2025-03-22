using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

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

        private Sequence _currentLevelSequence;
        public void Initialize(int LevelNumber, PinType pinType, UnityAction clickCallback)
        {
            SetupCurrentLevelSequence();
            _text.text = $"Ур. {LevelNumber}";

            _image.sprite = pinType switch
            {
                PinType.Closed => _closedtLevel,
                PinType.Current => _currentLevel,
                PinType.Passed => _passedLevel
            };

            if (pinType == PinType.Current)
            {
                transform.DORotate(new(0, 0, 25), 0.1f).OnComplete(() => _currentLevelSequence.Play());
                SetupCurrentLevelSequence();
            }

            _button.onClick.AddListener(() =>  clickCallback?.Invoke());
            
        }

        private void SetupCurrentLevelSequence()
        {
            if (_currentLevelSequence != null) return;
            _currentLevelSequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0, 0, -25), 0.2f))
                .Append(transform.DORotate(new Vector3(0, 0, 25), 0.2f))
                .SetLoops(-1)
                .Pause();
        }
        // при удалении объекта со сцены.
        private void OnDestroy()
        {
            _currentLevelSequence?.Kill();
        }
    }
}