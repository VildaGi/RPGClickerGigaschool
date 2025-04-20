using System.Collections.Generic;
using Game.Configs;
using Game.Configs.LevelConfigs;
using Game.Elements;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private HealthBar.HealthBar _healthBarPrefab;
        [SerializeField] private Sprite _waterSprite;
        [SerializeField] private Sprite _airSprite;
        [SerializeField] private Sprite _fireSprite;
        [SerializeField] private Sprite _rockSprite;
        
        [SerializeField] private StatusManager.StatusManager _statusManager;
        
        private Enemy _currentEnemyMonoBehaviour;
        private float _currentPlayerDamage;
        
        private Timer.Timer _timer;
        private HealthBar.HealthBar _healthBar;
        private LevelData _levelData;
        private int _currentEnemyIndex;
        private ElementType _attackElement;
        
        

        public event UnityAction<bool, int> OnLevelPassed;
        public event UnityAction OnKillEnemy;
        public event UnityAction OnTimerUpdate;
        public event UnityAction<int> AddCoins;

        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer)
        {
            _timer = timer;
            _healthBar = healthBar;
            _statusManager.OnDoTTrigger += DamageCurrentEnemy;
        }

        private void SpawnEnemy()
        {
            
            _currentEnemyIndex++;
            _timer.Stop();
            
            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelPassed?.Invoke(true, _levelData.Reward);
                _currentPlayerDamage = 1;
                _timer.Stop();
                return;
            }
            
            
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            _currentPlayerDamage = 1;
            
            
            
            _timer.SetActive(currentEnemy.IsBoss);
            
            if (currentEnemy.IsBoss)
            {
                _timer.SetValue(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false, 0);
                _timer.OnCounterUpdate += () => OnTimerUpdate?.Invoke();
            }
            
            InitHpBar(currentEnemy.Hp, currentEnemy.Element);
            
            var _currentEnemyViewData = _enemiesConfig.GetEnemy(currentEnemy.Id); // взяли инфу по врагу
            _currentEnemyMonoBehaviour.Initialize(_currentEnemyViewData.Sprite, currentEnemy.Hp, currentEnemy.Element);
        }

        public void IncreasePlayerDamage(float additionalDamage)
        {
            _currentPlayerDamage += additionalDamage;
        }

        public ElementType GetElementType()
        {
            return _attackElement;
        }
        
        private void InitHpBar(float health, ElementType element)
        {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);
            if (element == ElementType.Water)
                _healthBar.SetImage(_waterSprite);
            else if (element == ElementType.Fire)
                _healthBar.SetImage(_fireSprite);
            else if (element == ElementType.Rock)
                _healthBar.SetImage(_rockSprite);
            else if (element == ElementType.Air) _healthBar.SetImage(_airSprite);
        }

        public void StartLevel(LevelData levelData)
        {
            _levelData = levelData;
            _currentEnemyIndex -= 1;
            
            if (_currentEnemyMonoBehaviour == null)
            {
                _currentEnemyMonoBehaviour = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                // создает объекты на сцене (2 - аргумент это родитель внутри которого создаться наш враг)
                _currentEnemyMonoBehaviour.OnDead += SpawnEnemy;
                _currentEnemyMonoBehaviour.OnDead += () => OnKillEnemy?.Invoke();
                _currentEnemyMonoBehaviour.OnDamaged += _healthBar.DecreaseValue; // при получении урона уменьшаем хп
            }
            
            SpawnEnemy();
        }
        
        public void DamageCurrentEnemy()
        {
            var damage = _currentPlayerDamage * _statusManager.GetStatusesMultiplier();
            _currentEnemyMonoBehaviour.DoDamage(damage, _attackElement);
        }
        
        public void DamageCurrentEnemy(float damage)
        {
            var relevantDamage = damage * _statusManager.GetStatusesMultiplier();
            _currentEnemyMonoBehaviour.DoDamage(relevantDamage, _attackElement);
        }
        
        public void DamageCurrentEnemy(float damage, ElementType element)
        {
            var relevantDamage = damage * _statusManager.GetStatusesMultiplier();
            _currentEnemyMonoBehaviour.DoDamage(relevantDamage, element);
        }

        public void AddExtraCoins(int coins)
        {
            AddCoins?.Invoke(coins);
        }

        public void ChangeElementType(ElementType elementType)
        {
            if (elementType == _attackElement)
            {
                _attackElement = ElementType.NoneElement;
            }
            else
            {
                _attackElement = elementType;
            }
        }
    }
}