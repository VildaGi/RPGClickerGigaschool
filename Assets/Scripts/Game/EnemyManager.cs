using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    [SerializeField] private HealthBar _healthBarPrefab;
    
    private EnemyData _currentEnemyData;
    private Enemy _currentEnemy;

    public event UnityAction OnLevelPassed;

    public void Initialize()
    {
        
    }

    public void SpawnEnemy()
    {
        _currentEnemyData = _enemiesConfig.Enemies[0]; // взяли инфу по врагу
        if (_currentEnemy == null)
        {
            _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
            // создает объекты на сцене (2 - аргумент это родитель внутри которого создаться наш враг)
            _currentEnemy.OnDead += () => OnLevelPassed?.Invoke();
            _currentEnemy.OnDamaged += _currentEnemy._healthBar.DecreaseValue; // при получении урона уменьшаем хп
            _currentEnemy.OnDead += _currentEnemy._healthBar.Hide;  //прячем хп-бар
        }
        
        _currentEnemy.Initialize(_currentEnemyData);
    }
    
    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.DoDamage(damage);
    }
}