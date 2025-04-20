using DG.Tweening;
using UnityEngine;

namespace SceneManagement.Animations
{
    public class CloudLeft : MonoBehaviour
    {
        private Sequence _sequence;

        public void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(385, 1.2f, true))
                .OnComplete(()=>
                {
                    _sequence.Kill();
                    Destroy(gameObject);
                });
        }
    }
}