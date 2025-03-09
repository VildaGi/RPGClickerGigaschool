using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private MenuButtonManager _menuButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    [SerializeField] private Timer _timer;
    [SerializeField] private Statistics _statistics;
    [SerializeField] private InventoryMenuManager _inventoryMenuManager;
    [SerializeField] private SkillsMenuManager _skillsMenuManager;
    
    //[SerializeField] private HealthBar _healthBar;
    private void Awake()
    {
        _clickButtonManager.Initialize();
        _menuButtonManager.Initialize();
        _enemyManager.Initialize();
        _endLevelWindow.Initialize(_statistics);
        _skillsMenuManager.Initialize();
        _inventoryMenuManager.Initialize();
        
        
        // после инитиализации делаем нужные подписки.
        _clickButtonManager.onClicked += () => _enemyManager.DamageCurrentEnemy(1f); 
            // из пустого метода мы должны выполнить метод и передать 1f.
        _endLevelWindow.GetWinWindow().OnNextClicked += StartLevel;
        _endLevelWindow.GetLoseWindow().OnRestartClicked += StartLevel;
        
        _menuButtonManager.OnPauseGameClicked += PauseGame;
        _menuButtonManager.OnAttackMenuClicked += OpenAttackMenu;
        _menuButtonManager.OnSkillsMenuClicked += OpenSkillsMenu;
        _menuButtonManager.OnInventoryMenuClicked += OpenInventoryMenu;
        
        _enemyManager.OnLevelPassed += LevelPassed;

        StartLevel();
    }

    private void PauseGame()
    {
        if (_timer.IsPlaying)
        {
            _timer.Pause();
            _clickButtonManager.DisableButtons();
        }
        else
        {
            _timer.Resume();
            _clickButtonManager.EnableButtons();
        }
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

    private void OpenInventoryMenu()
    {
        _clickButtonManager.gameObject.SetActive(false);
        _inventoryMenuManager.gameObject.SetActive(true);
        _skillsMenuManager.gameObject.SetActive(false);
    }

    private void OpenAttackMenu()
    {
        _clickButtonManager.gameObject.SetActive(true);
        _inventoryMenuManager.gameObject.SetActive(false);
        _skillsMenuManager.gameObject.SetActive(false);
    }

    private void OpenSkillsMenu()
    {
        _clickButtonManager.gameObject.SetActive(false);
        _inventoryMenuManager.gameObject.SetActive(false);
        _skillsMenuManager.gameObject.SetActive(true);
    }
}
