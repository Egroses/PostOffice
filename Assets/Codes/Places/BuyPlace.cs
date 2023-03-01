using System.Collections;
using System.Collections.Generic;
using Codes.Char;
using UnityEngine;
using TMPro;
public class BuyPlace : MonoBehaviour
{
    [SerializeField] CharBalance charBalance;
    [SerializeField] TextMeshPro placeValueText;
    [SerializeField] float placeValue;
    [SerializeField] List<GameObject> buyableObject;
    [SerializeField] float moneyDecreaseValue=0.1f;
    float moneyDecrease;
    bool triggerOn = false;
    bool coroutineIsON;

    private void Start()
    {
        placeValueText.SetText(placeValue + "$");
        moneyDecrease = moneyDecreaseValue;
    }
 
    IEnumerator placeBuying()
    {
        while (charBalance.GetMoneyAmount()>0)
        {
            if (placeValue == 0 || !triggerOn)
            {
                break;
            }
            if (charBalance.GetMoneyAmount() > 0)
            {
                float i = charBalance.GetMoneyAmount();
                float lessAmount = 1;
                while (i > 0)
                {
                    if (triggerOn && placeValue > 0)
                    {
                        if (lessAmount >= placeValue)
                        {
                            lessAmount = placeValue;
                        }
                        if (i - lessAmount < 0)
                        {
                            lessAmount = 0;
                        }
                        placeValue -= lessAmount;
                        i -= lessAmount;

                        lessAmount += 1f;
                        if (placeValue <= 0)
                        {
                            ObjectOn();
                        }
                        
                        charBalance.SetMoneyAmount(i);
                        placeValueText.SetText(placeValue + "$");
                        yield return new WaitForSeconds(moneyDecrease);
                        moneyDecrease -= moneyDecrease / 3;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                moneyDecrease = moneyDecreaseValue;
                coroutineIsON = false;
                break;
            }
        }
        coroutineIsON = false;
        
        
    }
    void ObjectOn()
    { 
        foreach (var obj in buyableObject)
        {
            obj.SetActive(true);
        }
        transform.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("stickman"))
        {
            coroutineIsON = true;
            moneyDecrease = moneyDecreaseValue;
            StartCoroutine(placeBuying());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("stickman") && Joystick.Instance.Horizontal + Joystick.Instance.Vertical == 0 && !coroutineIsON)
        {
            coroutineIsON = true;
            triggerOn = true;
            StartCoroutine(placeBuying());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("stickman"))
        {
            triggerOn = false;
        }
    }
}
