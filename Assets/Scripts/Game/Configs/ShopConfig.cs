using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "Configs/ShopConfig")]
    public class ShopConfig : ScriptableObject
    {
        public Sprite ShopItemSprite;
        public Sprite BuyButtonSprite;
        public Sprite CoinSprite;
        
        public Sprite _prevButtonImage;
        public Sprite _nextButtonImage;
    }
}