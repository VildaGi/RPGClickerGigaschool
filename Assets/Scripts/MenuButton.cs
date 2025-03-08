using UnityEditor.Embree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] public Sprite _selectedImage;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    
    public void Initialize(Sprite sprite, ColorBlock colorBlock, Sprite menuImage)
    {
        //Инициализируем палитры кнопки
        //Визуальное изменение кнопки при клике.
        _image.sprite = sprite;
        _button.colors = colorBlock;
        _selectedImage = menuImage;
    }
    
    public void Initialize(Sprite sprite, ColorBlock colorBlock)
    {
        //Инициализируем палитры кнопки
        //Визуальное изменение кнопки при клике.
        _image.sprite = sprite;
        _button.colors = colorBlock;
    }
    
    public void SubscribeOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClick(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
}