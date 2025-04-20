using DG.Tweening;
using UnityEngine;

namespace SceneManagement.Animations
{
    public class CloudRightAfter : MonoBehaviour
    {
        private Sequence _sequence;

        public void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(2200, 2.6f, true))
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