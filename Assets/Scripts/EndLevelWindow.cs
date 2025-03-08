using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndLevelWindow : MonoBehaviour
{
    [SerializeField] private LoseWindow _loseLevelWindow;
    [SerializeField] private WinWindow _winLevelWindow;

    
    private Statistics _statistics;
    
    public void Initialize(Statistics statistics)
    {
        _winLevelWindow.Initialize();
        _loseLevelWindow.Initialize();
        _statistics = statistics;
    }

    public WinWindow GetWinWindow()
    {
        return _winLevelWindow;
    }
    public LoseWindow GetLoseWindow()
    {
        return _loseLevelWindow;
    }
    public void ShowLoseWindow()
    {
        _loseLevelWindow.Show();
        _winLevelWindow.Hide();
        gameObject.SetActive(true);
    }
    public void ShowWinWindow()
    {
        _loseLevelWindow.Hide();
        _winLevelWindow.SetBestTime(_statistics.GetBestTime());
        _winLevelWindow.Show();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _loseLevelWindow.Hide();
        _winLevelWindow.Hide();
        gameObject.SetActive(false);
    }
}