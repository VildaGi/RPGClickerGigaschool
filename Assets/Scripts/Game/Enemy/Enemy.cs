using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public event UnityAction<float> OnDamaged;
        public event UnityAction OnDead;
    
        private float _health;
        private ElementType _enemyElementType;
        private Sequence _currentSequenceDamage;

        public void Initialize(Sprite sprite, float health, ElementType elementType)
        {
            _health = health;
            _image.sprite = sprite;
            _enemyElementType = elementType;
        
            SetCurrentSequenceDamage();
        }

        public void OnDestroy()
        {
            _currentSequenceDamage.Kill();
        }

        private void SetCurrentSequenceDamage()
        {
            _currentSequenceDamage = DOTween.Sequence()
                .AppendCallback(() => transform.localScale = new Vector3(1, 1, 1))
                .Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f))
                .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
                .SetAutoKill(false)
                .Pause();
        }

        public void DoDamage(float damage, ElementType attackElementType)
        {
            damage *= GetElementalDamageFactor(attackElementType, _enemyElementType);
            if (damage >= _health)
            {
                _health = 0;
                OnDamaged?.Invoke(damage);
                OnDead?.Invoke();
            }
        
            _health -= damage;
            _currentSequenceDamage.Restart();
            OnDamaged?.Invoke(damage);
        
        }

        public float GetElementalDamageFactor(ElementType attackType, ElementType enemyType)
        {
            if (attackType == ElementType.NoneElement || enemyType == ElementType.NoneElement) return 1;
            switch (attackType)
            {
                case ElementType.Fire:
                    switch (enemyType)
                    {
                        case ElementType.Water:
                            return 2;
                        case ElementType.Air:
                            return 0.5f;
                        default:
                            return 1;
                    }
                case ElementType.Water:
                    switch (enemyType)
                    {
                        case ElementType.Rock:
                            return 2;
                        case ElementType.Fire:
                            return 0.5f;
                        default:
                            return 1;
                    }
                case ElementType.Rock:
                    switch (enemyType)
                    {
                        case ElementType.Air:
                            return 2;
                        case ElementType.Water:
                            return 0.5f;
                        default:
                            return 1;
                    }
                case ElementType.Air:
                    switch (enemyType)
                    {
                        case ElementType.Fire:
                            return 2;
                        case ElementType.Rock:
                            return 0.5f;
                        default:
                            return 1;
                    }
                default:
                    return 1;
            }
            
        }
    }
}