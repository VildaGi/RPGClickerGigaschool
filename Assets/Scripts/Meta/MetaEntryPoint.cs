using Game.Configs.SkillsConfig;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using Meta.Shop;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;

        private SaveSystem _saveSystem;
        private SceneLoader _sceneLoader;

        private const string COMMON_OBJECT_TAG = "CommonObject";
        
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            
            _saveSystem = commonObject.SaveSystem;
            _sceneLoader = commonObject.SceneLoader;
            
            var progress = (Progress) _saveSystem.GetData(SavableObjectType.Progress);   
            var wallet = (Wallet) _saveSystem.GetData(SavableObjectType.Wallet);
            _locationManager.Initialize(progress, wallet, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig);
        }

        private void StartLevel(int location, int level)
        {
            
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}