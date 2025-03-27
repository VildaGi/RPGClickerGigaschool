using Game.Configs;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private HealthBar _healthBarPrefab;
        
        private Enemy _currentEnemyMonoBehaviour;
        
        private Timer _timer;
        private HealthBar _healthBar;
        private LevelData _levelData;
        private int _currentEnemyIndex;

        public event UnityAction<bool> OnLevelPassed;

        public void Initialize(HealthBar healthBar, Timer timer)
        {
            _timer = timer;
            _healthBar = healthBar;
        }

        private void SpawnEnemy()
        {
            _currentEnemyIndex++;
            _timer.Stop();
            
            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            
            
            _timer.SetActive(currentEnemy.IsBoss);
            
            if (currentEnemy.IsBoss)
            {
                _timer.SetValue(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            
            InitHpBar(currentEnemy.Hp);
            
            var _currentEnemyViewData = _enemiesConfig.GetEnemy(currentEnemy.Id); // взяли инфу по врагу
            _currentEnemyMonoBehaviour.Initialize(_currentEnemyViewData.Sprite, currentEnemy.Hp);
        }

        private void InitHpBar(float health)
        {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);
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
                _currentEnemyMonoBehaviour.OnDamaged += _healthBar.DecreaseValue; // при получении урона уменьшаем хп
            }
            
            SpawnEnemy();
        }
        
        public void DamageCurrentEnemy(float damage)
        {
            _currentEnemyMonoBehaviour.DoDamage(damage);
        }
    }
}