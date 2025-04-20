using System.Collections.Generic;
using System.Linq;
using Game.Configs;
using Game.Configs.SkillsConfig;
using Game.WalletWindow;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        
        [SerializeField] private List<GameObject> _pages;
        
        [SerializeField] private List<ShopItem> _items;
        [SerializeField] private ShopConfig _config;
        [SerializeField] private WalletWindow _walletWindow;
        
        private Dictionary<string, ShopItem> _itemsMap;
        private int _currentPage = 0;
        private OpenedSkills _openedSkills;
        private Wallet _wallet;
        private SkillsConfig _skillsConfig;
        private SaveSystem _saveSystem;

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig)
        {
            _saveSystem = saveSystem;
            _openedSkills = (OpenedSkills)saveSystem.GetData(SavableObjectType.OpenedSkills);
            _wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);;
            _skillsConfig = skillsConfig;
            _walletWindow.Initialize((Wallet)saveSystem.GetData(SavableObjectType.Wallet));
            
            InitializeItemMap();
            ShowShopItems();
            InitializePageSwitching();
            
        }

        private void ShowShopItems()
        {
            foreach (var skillData in _skillsConfig.Skills)
            {
                var skillWithLevel = _openedSkills.GetSkillWithLevel(skillData.SkillId);
                var skillDataByLevel = skillData.GetSkillByLevel(skillWithLevel.Level);

                if (!_itemsMap.ContainsKey(skillData.SkillId)) continue;
                
                _itemsMap[skillData.SkillId].Initialize(
                    skillId => SkillUpgrade(skillId, skillDataByLevel.Cost), 
                    skillData.SkillName, 
                    skillData.SkillDiscription, 
                    skillDataByLevel.Cost,
                    _config.ShopItemSprite,
                    _config.BuyButtonSprite,
                    _wallet.Coins >= skillDataByLevel.Cost,
                    skillDataByLevel.Level,
                    skillData.IsMaxLevel(skillWithLevel.Level));    
            }
        }

        private void InitializeItemMap()
        {
            _itemsMap = new();
            foreach (var shopItem in _items)
                _itemsMap[shopItem.SkillId] = shopItem;
        }

        private void SkillUpgrade(string skillId, int cost)
        {
            var skillWithLevel = _openedSkills.GetSkillWithLevel(skillId);
            skillWithLevel.Level++;
            _wallet.Coins -= cost;
            
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            _walletWindow.UpdateCoins(_wallet.Coins);
            ShowShopItems();

        }

        public void InitializePageSwitching()
        {
            _previousButton.onClick.AddListener(() => ShowPage(_currentPage - 1));
            _previousButton.image.sprite = _config._prevButtonImage;
            _nextButton.image.sprite = _config._nextButtonImage;
            _nextButton.onClick.AddListener(() => ShowPage(_currentPage + 1));
            ShowPage(_currentPage);
        }

        private void ShowPage(int index)
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                _currentPage = (index + _pages.Count) % _pages.Count;
                _pages[i].SetActive(i == _currentPage);
            }
        }
    }
}