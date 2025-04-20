using Game.Configs.SkillsConfig;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using Meta.Shop;
using SceneManagement;
using UnityEngine;
using Meta.Menu;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private MetaMenuButtonManager _metaMenuButtonManagerOnMap;
        [SerializeField] private MetaMenuButtonManager _metaMenuButtonManagerOnShop;
        
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
            _metaMenuButtonManagerOnMap.Initialize();
            _metaMenuButtonManagerOnShop.Initialize();


            _metaMenuButtonManagerOnMap.OnMapClicked += OpenMapWindow;
            _metaMenuButtonManagerOnShop.OnMapClicked += OpenMapWindow;
            _metaMenuButtonManagerOnMap.OnShopClicked += OpenShopWindow;
            _metaMenuButtonManagerOnShop.OnShopClicked += OpenShopWindow;
        }

        private void StartLevel(int location, int level)
        {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }

        private void OpenShopWindow()
        {
            _shopWindow.gameObject.SetActive(true);
            _locationManager.gameObject.SetActive(false);
        }

        private void OpenMapWindow()
        {
            _shopWindow.gameObject.SetActive(false);
            _locationManager.SetActive(((Wallet) _saveSystem.GetData(SavableObjectType.Wallet)).Coins);
        }
    }
}