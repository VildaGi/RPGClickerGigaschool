using UnityEngine;
using UnityEngine.UI;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "ClickButtonConfig", menuName = "Configs/ClickButtonConfig")]
    public class ClickButtonConfig : ScriptableObject
    {
        public Sprite DefaultSprite;
        
        public Sprite FireSprite;
        public Sprite FireSwordButtonSprite;
        
        public Sprite RockSprite;
        public Sprite RockSwordButtonSprite;
        
        public Sprite WaterSprite;
        public Sprite WaterSwordButtonSprite;
        
        public Sprite AirSprite;
        public Sprite AirSwordButtonSprite;
        public ColorBlock ButtonColors;
    
    }
}