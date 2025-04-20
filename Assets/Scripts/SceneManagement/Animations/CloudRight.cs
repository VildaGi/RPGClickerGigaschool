using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace SceneManagement.Animations
{
    public class CloudRight : MonoBehaviour
    {
        private Sequence _sequence;
        
        public event UnityAction OnSecEnded;

        public void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(885, 1.2f, true))
                .OnComplete(()=>
                {
                    _sequence.Kill();
                    OnSecEnded?.Invoke();
                    Destroy(gameObject);
                });
        }
    }
}