using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.ClickButtons
{
    public class CooldawnTimer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private float _maxTime;
        private float _currentTime;
        private bool _isPlaying;
        
        public event UnityAction OnTimerEnd;
        
        public bool IsPlaying => _isPlaying;
        public void SetValue(float maxTime)
        {
            _maxTime = maxTime;
            _currentTime = maxTime;
            _slider.maxValue = maxTime;
            _slider.value = maxTime;
            Show();
            Play();
        }
        
    
        private void Play()
        {
            _isPlaying = true;
        }

        private void Stop()
        {
            _isPlaying = false;
            OnTimerEnd = null;
        }
        
        private void FixedUpdate()
        {
            if (!_isPlaying) return;
        
            var deltaTime = Time.fixedDeltaTime;
            if (deltaTime > _currentTime)
            {
                OnTimerEnd?.Invoke();
                Stop();
                Hide();
                return;
            }
            
            _currentTime -= deltaTime;
            _slider.value -= deltaTime;
        }
        
        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}