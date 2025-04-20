using System.Collections.Generic;
using Game;
using Game.WalletWindow;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private WalletWindow _walletWindow;
        
        [SerializeField] private List<Location> _locations;
        private int _currentLocation;

        public void Initialize(Progress progress, Wallet wallet, UnityAction<int, int> startLevelCallback)
        {
            _currentLocation = progress.CurrentLocation;
            InitLocation(progress, startLevelCallback);
            _walletWindow.Initialize(wallet);
            InitializeMoveLocationButtons();
        }

        public void SetActive(int coins)
        {
            gameObject.SetActive(true);
            _walletWindow.UpdateCoins(coins);
        }
        private void InitializeMoveLocationButtons()
        {
            _previousButton.onClick.AddListener(ShowPreviousLocation);
            _nextButton.onClick.AddListener(ShowNextLocation);
            
            if (_currentLocation == _locations.Count)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 1)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }

        private void ShowNextLocation()
        {
            _locations[_currentLocation - 1].gameObject.SetActive(false);
            _currentLocation++;
            _locations[_currentLocation - 1].gameObject.SetActive(true);

            if (_currentLocation == _locations.Count)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 2)
            {
                _previousButton.gameObject.SetActive(true);
            }
        }
        
        private void ShowPreviousLocation()
        {
            _locations[_currentLocation - 1].gameObject.SetActive(false);
            _currentLocation--;
            _locations[_currentLocation - 1].gameObject.SetActive(true);

            if (_currentLocation == _locations.Count - 1)
            {
                _nextButton.gameObject.SetActive(true);
            }
            
            if (_currentLocation == 1)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }
        private void InitLocation(Progress progress, UnityAction<int, int> startLevelCallback)
        {
            for (var i = 0; i < _locations.Count; i++)
            {
                var locationNumber = i + 1;


                ProgressState isLocationPassed = progress.CurrentLocation > locationNumber
                    ? ProgressState.Passed
                    : progress.CurrentLocation == locationNumber
                        ? ProgressState.Current
                        : ProgressState.Closed;
                
                var currentLevel = progress.CurrentLevel;
                
                _locations[i].Initialize(isLocationPassed, currentLevel,level => startLevelCallback?.Invoke(locationNumber, level)); //Идем снизу вверх по передачи интов.
                _locations[i].SetActive(progress.CurrentLocation == locationNumber);
            }
        }
    }
}