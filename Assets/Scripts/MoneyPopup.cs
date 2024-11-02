using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyPopup : MonoBehaviour
{
    public UnityEvent<int> OnMoneyChanged { get; private set; } = new();
    private void Start()
    {
        OnMoneyChanged.AddListener(Appear);
    }
    private void Appear(int _delta)
    {
        string _newString;
        if(_delta>=0)
        {
            _newString = $"+{_delta}$";
        }
        else
        {
            _newString = $"{_delta}$";
        }
        GetComponent<TextMeshProUGUI>().text = _newString;
        GetComponent<Animator>().SetTrigger("Popup");
    }
}
