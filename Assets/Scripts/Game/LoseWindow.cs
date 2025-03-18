using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private Button _loseRestartButton;
    
    public event UnityAction OnRestartClicked;
    public void Initialize()
    {
        _loseRestartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        OnRestartClicked?.Invoke();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        Random rnd = new Random();
        String[] loseTips = 
        {
            "Try upgrade up your skills!\n(Coming Soon!)", 
            "Maybe you should buy some new gear?\n(Coming Soon!)", 
            "Click faster...",
            "Take a break if needed, then come back refreshed and ready to dominate the game."
        };
        _statText.text = loseTips[rnd.Next(0, loseTips.Length)];
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}