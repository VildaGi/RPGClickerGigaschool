using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;

        public void Initialize(UnityAction<int> levelStartCallback)
        {
            var currentLevel = 3;

            for (var i = 0; i < _pins.Count; i++)
            {
                var level = i + 1;
                var pinType = currentLevel > level 
                    ? PinType.Passed 
                    : currentLevel == level 
                        ? PinType.Current 
                        : PinType.Closed;
                _pins[i].Initialize(level, pinType, () => levelStartCallback?.Invoke(level));
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}