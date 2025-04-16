using Global.SaveSystem.SavableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.WalletWindow
{
    public class WalletWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textMeshPro;
        [SerializeField] Image _coinImage;

        public void Initialize(Wallet wallet)
        {
            _textMeshPro.text = wallet.Coins.ToString();
        }
    }
}