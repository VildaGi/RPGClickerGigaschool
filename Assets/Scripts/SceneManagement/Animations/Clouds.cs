using DG.Tweening;
using UnityEngine;

namespace SceneManagement.Animations
{
    public class Clouds : MonoBehaviour
    {
        private Sequence _sequence;

        public void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(585, 1.4f, true));
        }

        public void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}