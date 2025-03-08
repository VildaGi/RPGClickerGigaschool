using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private MenuButtonManager _menuButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    //[SerializeField] private HealthBar _healthBar;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    [SerializeField] private Timer _timer;
    [SerializeField] private Statistics _statistics;
    
    private void Awake()
    {
        _clickButtonManager.Initialize();
        _menuButtonManager.Initialize();
        _enemyManager.Initialize();
        _endLevelWindow.Initialize(_statistics);
        
        
        // после инитиализации делаем нужные подписки.
        _clickButtonManager.onClicked += () => _enemyManager.DamageCurrentEnemy(1f); 
            // из пустого метода мы должны выполнить метод и передать 1f.
        
        _endLevelWindow.GetWinWindow().OnNextClicked += StartLevel;
        _endLevelWindow.GetLoseWindow().OnRestartClicked += StartLevel;
        _enemyManager.OnLevelPassed += LevelPassed;

        StartLevel();
    }

    private void LevelPassed()
    {
        _timer.Stop();
        _statistics.CheckBestTime(_timer.GetCurrentTime());
        
        _endLevelWindow.ShowWinWindow();
    }

    private void StartLevel()
    {
        _endLevelWindow.Hide();
        _timer.Initialize(5f);
        _statistics.Initialize(5f);
        _enemyManager.SpawnEnemy();
        _timer.OnTimerEnd += _endLevelWindow.ShowLoseWindow;
    }
}
