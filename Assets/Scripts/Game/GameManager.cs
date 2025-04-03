using System;
using Game.ClickButtons;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.EndLevel;
using Game.Enemy;
using Game.Skills;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Game
{
    public class GameManager : EntryPoint
    {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private MenuButtonManager _menuButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private EndLevelWindow _endLevelWindow;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private Image _levelBackground;
    
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private SkillsConfig _SkillsConfig;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        
        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        private SkillSystem _skillSystem;
        private const string SCENE_LOADER_TAG = "SceneLoader";
    
        public override void Run(SceneEnterParams enterParams)
        {
            _saveSystem = FindFirstObjectByType<SaveSystem>();
            
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

            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new SkillSystem(openedSkills, _SkillsConfig, _enemyManager);
            
            // после инитиализации делаем нужные подписки.
            _clickButtonManager.OnClicked += () =>
            {
                _enemyManager.DamageCurrentEnemy(1f);
                _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
            };
            _clickButtonManager.OnFireClicked += () => _enemyManager.ChangeElementType(ElementType.Fire);
            _clickButtonManager.OnAirClicked += () => _enemyManager.ChangeElementType(ElementType.Air);
            _clickButtonManager.OnRockClicked += () => _enemyManager.ChangeElementType(ElementType.Rock);
            _clickButtonManager.OnWaterClicked += () => _enemyManager.ChangeElementType(ElementType.Water);
            
            // из пустого метода мы должны выполнить метод и передать 1f.
            _endLevelWindow.OnNextClicked += NextLevel;
            _endLevelWindow.OnRestartClicked += RestartLevel;
            
            
            
            _menuButtonManager.OnMapClicked += OpenMap;
            _enemyManager.OnLevelPassed += LevelPassed;

            StartLevel();
        }
        
        private void OpenMap()
        {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadMetaScene();
        }

        private void RestartLevel()
        {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene(_gameEnterParams);
        }
        private void LevelPassed(bool isPassed)
        {
            if (isPassed)
            {
                TrySaveProgress();
                _endLevelWindow.ShowWinWindow();
            }
            else
            {
                _endLevelWindow.ShowLoseWindow();
            }
        }

        private void TrySaveProgress()
        {
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation ||
                _gameEnterParams.Level != progress.CurrentLevel) return;
            
            var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);
            if (progress.CurrentLevel + 1 > maxLevel)
            {
                progress.CurrentLevel = 1;
                progress.CurrentLocation++;
            }else
            {
                progress.CurrentLevel++;
            }
            
            _saveSystem.SaveData(SavableObjectType.Progress);

        }

        private void StartLevel()
        {
            var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
            Debug.Log($"{_gameEnterParams.Location} {_gameEnterParams.Level}");
            
            // выбор случайного уровня из возможных
            _levelBackground.sprite = levelData.LevelBackgrounds[new Random().Next(0, levelData.LevelBackgrounds.Count)];
            _enemyManager.StartLevel(levelData);
        }
        private void NextLevel()
        {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadMetaScene(_gameEnterParams);
        }
    }
}
