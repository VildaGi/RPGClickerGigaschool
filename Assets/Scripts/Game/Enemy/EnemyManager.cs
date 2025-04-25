using System.Collections.Generic;
using Game.Configs;
using Game.Configs.LevelConfigs;
using Game.Elements;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
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
        private int _currentEnemyIndex;

        private Timer.Timer _timer;
        private HealthBar.HealthBar _healthBar;
        private LevelData _levelData;
        private SaveSystem _saveSystem;
        
        private ElementType _attackElement;
        private float _currentPlayerDamage;


        public event UnityAction<bool, int> OnLevelPassed;
        public event UnityAction OnKillEnemy;
        public event UnityAction OnTimerUpdate;
        public event UnityAction<int> AddCoins;

        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer, SaveSystem saveSystem)
        {
            _timer = timer;
            _healthBar = healthBar;
            _statusManager.OnDoTTrigger += DamageCurrentEnemyWithDOT;
            _saveSystem = saveSystem;
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
            
            
            
            _timer.SetActive(currentEnemy.IsBoss);
            
            if (currentEnemy.IsBoss)
            {
                _timer.SetValue(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false, 0);
                _timer.OnCounterUpdate += () => OnTimerUpdate?.Invoke();
            }
            
            
            var actualHP = CalculateActualHp(currentEnemy.Hp);
            
            InitHpBar(actualHP, currentEnemy.Element);
            
            var _currentEnemyViewData = _enemiesConfig.GetEnemy(currentEnemy.Id); // взяли инфу по врагу
            _currentEnemyMonoBehaviour.Initialize(_currentEnemyViewData.Sprite, actualHP, currentEnemy.Element);
        }

        private float CalculateActualHp(float currentEnemyHP)
        {
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            int currentLevel = progress.CurrentLevel;
            int currentLocation = progress.CurrentLocation;
            int currentLoop = progress.CurrentLoop;
            
            float actualHP = currentEnemyHP + (150 * currentLocation) + (30 * currentLevel) + (500 * currentLoop);
            return actualHP;
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
        
        public void DamageCurrentEnemyWithDOT((float, float, float, float) damage)
        {
            var fireDamage = damage.Item2 * _statusManager.GetStatusesMultiplier();
            var AirDamage = damage.Item1 * _statusManager.GetStatusesMultiplier();
            var RockDamage = damage.Item3 * _statusManager.GetStatusesMultiplier();
            var WaterDamage = damage.Item4 * _statusManager.GetStatusesMultiplier();

            if (fireDamage > 1) _currentEnemyMonoBehaviour.DoDamage(fireDamage, ElementType.Fire);
            if (AirDamage > 1) _currentEnemyMonoBehaviour.DoDamage(AirDamage, ElementType.Air);
            if (RockDamage > 1) _currentEnemyMonoBehaviour.DoDamage(RockDamage, ElementType.Rock);
            if (WaterDamage > 1) _currentEnemyMonoBehaviour.DoDamage(WaterDamage, ElementType.Water);
        }
        public void DamageCurrentEnemy(float damage)
        {
            var relevantDamage = damage * _statusManager.GetStatusesMultiplier();
            _currentEnemyMonoBehaviour.DoDamage(relevantDamage, _attackElement);
        }
        public void DamageCurrentEnemy(ElementType element)
        {
            var damage = _currentPlayerDamage * _statusManager.GetStatusesMultiplier();
            _currentEnemyMonoBehaviour.DoDamage(damage, element);
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