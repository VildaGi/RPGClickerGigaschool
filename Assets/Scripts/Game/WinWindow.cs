using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private Button _winNextButton;
    
    public event UnityAction OnNextClicked;
    public void Initialize()
    {
        _winNextButton.onClick.AddListener(Next);
    }
    private void Next()
    {
        OnNextClicked?.Invoke();
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetBestTime(float time)
    {
        _statText.text = $"Best Time: \n{time.ToString("00.00")}";
    }
    
}