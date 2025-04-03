using UnityEngine;
using UnityEngine.UI;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "MenuButtonConfig", menuName = "Configs/MenuButtonConfig")]
    public class MenuButtonConfig : ScriptableObject
    {
        public Sprite AttackDefaultSprite;
        public Sprite MapDefaultSprite;
        public Sprite ShopDefaultSprite;

        public ColorBlock ButtonColors;
    }
}