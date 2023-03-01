using TMPro;
using UnityEngine;

namespace Codes.Char
{
    public class CharBalance : MonoBehaviour
    {
        [SerializeField] float moneyAmount;
        [SerializeField] TextMeshProUGUI moneyText;
        private void Start()
        {
            SetMoneyAmount(moneyAmount);
        }
        public void EarnMoney(int amount)
        {
            moneyAmount += amount;
            moneyText.SetText(moneyAmount + "$");
        }
        public float GetMoneyAmount()
        {
            return moneyAmount;
        }
        public void SetMoneyAmount(float amount)
        {
            moneyAmount = amount;
            moneyText.SetText(moneyAmount + "$");
        }
    }
}
