using DG.Tweening;
using UnityEngine;

namespace SceneManagement.Animations
{
    public class CloudLeftAfter : MonoBehaviour
    {
        private Sequence _sequence;

        public void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(-1250, 2.6f, true))
                .OnComplete(()=> 
                { 
                    _sequence.Kill(); 
                    Destroy(gameObject);
                });
        }

        public void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}