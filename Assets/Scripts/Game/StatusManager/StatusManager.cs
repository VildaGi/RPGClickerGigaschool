using System.Collections.Generic;
using System.Linq;
using Game.Elements;
using UnityEngine;
using UnityEngine.Events;

namespace Game.StatusManager
{
    public class StatusManager : MonoBehaviour
    {
        private List<StatusData>  _statuses = new List<StatusData>();
        private int _ticksCounter = 0;

        public event UnityAction<(float, float, float, float)> OnDoTTrigger;
        
        private void FixedUpdate()
        {
            _ticksCounter++;
            if (_ticksCounter > 75 && ExistDoTStatus())
            {
                _ticksCounter = 0;
                OnDoTTrigger?.Invoke(GetDoTDamage());
            }
            
            var deltaTime = Time.fixedDeltaTime;
            UpdateStatuses(deltaTime);
        }

        private (float, float, float, float) GetDoTDamage()
        {
            var Airdamage = 0f;
            var Firedamage = 0f;
            var Rockdamage = 0f;
            var Waterdamage = 0f;


            foreach (var status in _statuses)
            {
                if (status.statusType == StatusType.DoT)
                {
                    switch (status.StatusElementType)
                    {
                        case ElementType.Air:
                            Airdamage += status.StatusValue;
                            break;
                        case ElementType.Water:
                            Waterdamage += status.StatusValue;
                            break;
                        case ElementType.Rock:
                            Rockdamage += status.StatusValue;
                            break;
                        case ElementType.Fire:
                            Firedamage += status.StatusValue;
                            break;
                    }
                }
            }
            return (Airdamage, Firedamage, Rockdamage, Waterdamage);
        }

        private void UpdateStatuses(float deltaTime)
        {
            if (_statuses == null) return;
            foreach (var statuse in _statuses.ToList())
            {
                statuse.Update(deltaTime);
                if (statuse.Time <= 0)
                {
                    _statuses.Remove(statuse);
                }
            }
        }

        private bool ExistDoTStatus()
        {
            foreach (var statuse in _statuses)
            {
                if (statuse.statusType == StatusType.DoT) return true;
            }
            return false;
        }
        
        public void AddStatus(StatusData status)
        {
            _statuses.Add(status);
        }
        
        public float GetStatusesMultiplier()
        {
            var multiplier = 1f;
            foreach (var status in _statuses) if (status.statusType == StatusType.Multiplier) multiplier *= status.StatusValue;
            
            return multiplier;
        }
    }
}