using System;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.LoopWindow
{
    public class LoopWindow : MonoBehaviour
    {
        [SerializeField] Button _loopButton;
        
        public event UnityAction OnLoopClicked;
        public void Initialize(SaveSystem saveSystem)
        {
            _loopButton.onClick.AddListener(NextLoop);
        }

        private void NextLoop()
        {
            OnLoopClicked?.Invoke();
        }
    }
}