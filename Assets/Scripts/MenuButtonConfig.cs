using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MenuButtonConfig", menuName = "Configs/MenuButtonConfig")]
public class MenuButtonConfig : ScriptableObject
{
    public Sprite AttackDefaultSprite;
    public Sprite AttackSelectedSprite;
    
    public Sprite SkillsDefaultSprite;
    public Sprite SkillsSelectedSprite;
    
    public Sprite InventoryDefaultSprite;
    public Sprite InventorySelectedSprite;
    
    public Sprite PauseButtonSprite;
    public Sprite ResumeButtonSprite;
    
    public ColorBlock ButtonColors;
}