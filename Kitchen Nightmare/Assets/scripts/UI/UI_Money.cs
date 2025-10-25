using UnityEngine;
using TMPro;

public class UI_Money : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        CurrencyManager.Instance.OnMoneyChanged += UpdateDisplay;
        UpdateDisplay(CurrencyManager.Instance.GetMoney());
    }

    private void UpdateDisplay(int amount)
    {
        moneyText.text =  amount + "$" ;
    }
}


