using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StopGameButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _resumeSprite;
    private bool _paused;
    public void Initialize(Sprite pauseSprite, Sprite resumeSprite, ColorBlock colorBlock)
    {
        _image.sprite = pauseSprite;
        _button.colors = colorBlock;
        _paused = false;
        
        _pauseSprite = pauseSprite;
        _resumeSprite = resumeSprite;
    }
    
    public void ChangeImage()
    {
        _image.sprite = !_paused ? _resumeSprite : _pauseSprite;
        _paused = !_paused;
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