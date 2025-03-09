using TMPro;
using UnityEngine;

public class InventoryMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Initialize()
    {
        _text.text = "Coming Soon!";
        _text.fontSize = 60;
    }
}