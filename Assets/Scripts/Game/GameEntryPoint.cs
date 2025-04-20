using System;
using Game.ClickButtons;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.Elements;
using Game.EndLevel;
using Game.Enemy;
using Game.MenuManager;
using Game.Skills;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Game
{
    public class GameEntryPoint : EntryPoint
    {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private MenuButtonManager _menuButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private StatusManager.StatusManager _statusManager;
        [SerializeField] private Image _levelBackground;
        [SerializeField] private EndLevelWindow _endLevelWindow;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private WalletWindow.WalletWindow _walletWindow;
        [SerializeField] private HealthBar.HealthBar _healthBar;

        
        
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private SkillsConfig _SkillsConfig;

        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        private SkillSystem _skillSystem;
        private EndLevelSystem _endLevelSystem;
        private SceneLoader _sceneLoader;
        private const string COMMON_OBJECT_TAG = "CommonObject";
    
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            
            _saveSystem = commonObject.SaveSystem;
            _sceneLoader = commonObject.SceneLoader;
            if (enterParams is not GameEnterParams gameEnterParams)
            {
                Debug.LogError("Game Enter Params are invalid");
                return;
            }
            
            _gameEnterParams = gameEnterParams;
            
            
            _clickButtonManager.Initialize();
            _menuButtonManager.Initialize();
            _enemyManager.Initialize(_healthBar, _timer);
            _endLevelWindow.Initialize();
            _walletWindow.Initialize((Wallet)_saveSystem.GetData(SavableObjectType.Wallet));

            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new SkillSystem(openedSkills, _SkillsConfig, _enemyManager, _statusManager);
            _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);
            
            
            // после инитиализации делаем нужные подписки.
            _clickButtonManager.OnClicked += () =>
            {
                _enemyManager.DamageCurrentEnemy();
                _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
            };
            
            // делаем подписки на кнопки и экшены с нажатий
            _clickButtonManager.OnFireClicked += () => _enemyManager.ChangeElementType(ElementType.Fire);
            _clickButtonManager.OnAirClicked += () => _enemyManager.ChangeElementType(ElementType.Air);
            _clickButtonManager.OnRockClicked += () => _enemyManager.ChangeElementType(ElementType.Rock);
            _clickButtonManager.OnWaterClicked += () => _enemyManager.ChangeElementType(ElementType.Water);
            _clickButtonManager.FireActive += () => _skillSystem.InvokeTrigger(SkillTrigger.FireActive);
            _clickButtonManager.AirActive += () => _skillSystem.InvokeTrigger(SkillTrigger.AirActive);
            _clickButtonManager.WaterActive += () => _skillSystem.InvokeTrigger(SkillTrigger.WaterActive);
            _clickButtonManager.RockActive += () => _skillSystem.InvokeTrigger(SkillTrigger.RockActive);
            
            _endLevelWindow.OnNextClicked += NextLevel;
            _endLevelWindow.OnRestartClicked += RestartLevel;
            
            _menuButtonManager.OnMapClicked += OpenMap;
            
            _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;
            _enemyManager.AddCoins += _endLevelSystem.AddReward;
            _enemyManager.AddCoins += (int x) => _walletWindow.UpdateCoins(((Wallet)_saveSystem.GetData(SavableObjectType.Wallet)).Coins);
            _enemyManager.OnTimerUpdate += () => _skillSystem.InvokeTrigger(SkillTrigger.OnTime);
            _enemyManager.OnKillEnemy += () => _skillSystem.InvokeTrigger(SkillTrigger.OnKill);

            StartLevel();
        }
        
        private void OpenMap()
        {
            _sceneLoader.LoadMetaScene();
        }

        private void RestartLevel()
        {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }

        private void StartLevel()
        {
            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            var location = _gameEnterParams.Location;
            var level = _gameEnterParams.Level;
            if (_gameEnterParams.Location > maxLocationAndLevel.x ||
                (_gameEnterParams.Location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
            {
                location = maxLocationAndLevel.x;
                level = maxLocationAndLevel.y;
            }
            
            var levelData = _levelsConfig.GetLevel(location, level);
            
            // выбор случайного уровня из возможных
            _levelBackground.sprite = levelData.LevelBackgrounds[new Random().Next(0, levelData.LevelBackgrounds.Count)];
            _enemyManager.StartLevel(levelData);    
            _skillSystem.InvokeTrigger(SkillTrigger.OnStart);
        }
        private void NextLevel()
        {
            _sceneLoader.LoadMetaScene(_gameEnterParams);
        }
    }
}
