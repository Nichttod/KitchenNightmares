using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UI_FridgeStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;

    public void UpdateDisplay(Dictionary<KitchenObjectSO, int> storedItems)
    {
        if (storedItems.Count == 0)
        {
            statusText.text = "";
            return;
        }

        string result = "\n";
        foreach (var item in storedItems)
        {
            result += $"{item.Key.name} x{item.Value}\n";
        }

        statusText.text = result.TrimEnd();
    }
}
