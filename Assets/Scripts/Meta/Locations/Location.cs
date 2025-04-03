using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;

        public void Initialize(ProgressState locationState, int currentLevel ,UnityAction<int> levelStartCallback)
        {
            for (var i = 0; i < _pins.Count; i++)
            {
                var level = i + 1;
                ProgressState progressState = locationState switch
                {
                    ProgressState.Closed => ProgressState.Closed,
                    ProgressState.Passed => ProgressState.Passed,
                    _ => currentLevel > level ? ProgressState.Passed :
                        currentLevel == level ? ProgressState.Current : ProgressState.Closed
                };

                if (progressState == ProgressState.Closed)
                {
                    _pins[i].Initialize(level, progressState, null);
                }
                else
                {
                    _pins[i].Initialize(level, progressState, () => levelStartCallback?.Invoke(level));
                }
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}