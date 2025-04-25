using Game.Configs.LevelConfigs;
using Game.EndLevelSystem.EndLevel;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;

namespace Game.EndLevelSystem
{
    public class EndLevelSystem
    {
        private readonly EndLevelWindow _endLevelWindow;
        private readonly SaveSystem _saveSystem;
        private readonly GameEnterParams _gameEnterParams;
        private readonly LevelsConfig _levelsConfig;

        public EndLevelSystem(EndLevelWindow endLevelWindow, SaveSystem saveSystem,
            GameEnterParams gameEnterParams, LevelsConfig levelsConfig)
        {
            _endLevelWindow = endLevelWindow;
            _saveSystem = saveSystem;
            _levelsConfig = levelsConfig;
            _gameEnterParams = gameEnterParams;
        }
        
        
        public void LevelPassed(bool isPassed, int reward)
        {
            if (isPassed)
            {
                TrySaveProgress();
                TrySaveWallet(reward);
                _endLevelWindow.ShowWinWindow();
            }
            else
            {
                _endLevelWindow.ShowLoseWindow();
            }
        }

        public void AddReward(int reward)
        {
            TrySaveWallet(reward);
            
        }

        private void TrySaveWallet(int reward)
        {
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            wallet.Coins += reward;
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }

        private void TrySaveProgress()
        {
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation ||
                _gameEnterParams.Level != progress.CurrentLevel) return;

            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            if (progress.CurrentLocation > maxLocationAndLevel.x ||
                (progress.CurrentLocation == maxLocationAndLevel.x &&
                 progress.CurrentLevel > maxLocationAndLevel.y)) return;
            
            
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
    }
}