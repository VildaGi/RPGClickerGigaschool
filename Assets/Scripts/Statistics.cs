using UnityEngine;

public class Statistics : MonoBehaviour
{
    private float _bestTime;
    private float _maxTime;

    public void Initialize(float maxTime)
    {
        _maxTime = maxTime;
    }
    public void CheckBestTime(float time)
    {
        time = _maxTime - time;
        if (time < _bestTime || _bestTime == 0)
        {
            _bestTime = time;
        }
    }

    public float GetBestTime()
    {
        return _bestTime;
    }
}
