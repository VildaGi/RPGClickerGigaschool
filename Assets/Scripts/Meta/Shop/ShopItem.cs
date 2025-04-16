using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Button _button;
        [SerializeField] private Image _itemBorder;
        [SerializeField] private Image _buyButton;
        public string SkillId;

        public void Initialize(UnityAction<string> onClick, 
            string label, 
            string description, 
            int cost,
            Sprite borderSprite,
            Sprite buyButtonSprite,
            bool isEnough, 
            bool isMaxLevel)
        {
            _button.onClick.AddListener(() => onClick?.Invoke(SkillId));
            _label.text = label;
            _description.text = description;
            _itemBorder.sprite = borderSprite;
            _buyButton.sprite = buyButtonSprite;
            
            if (isMaxLevel)
            {
                _cost.gameObject.SetActive(false);
                _button.interactable = false;
                return;
            }
            
            _cost.text = cost.ToString();
            _cost.color = isEnough ? Color.green : Color.red;
            _button.interactable = isEnough;
        }
    }
}
