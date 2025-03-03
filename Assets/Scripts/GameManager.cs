using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    private void Awake()
    {
        _clickButtonManager.Initialize();
    }
}
