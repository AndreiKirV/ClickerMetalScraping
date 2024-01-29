using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController
{
    public int Value => _value;
    public int PassiveIncome => _passiveIncome;
    public int ActiveIncome => _activeIncome;

    private WalletMB _walletMB;
    private int _value;
    private int _passiveIncome = 0;
    private int _activeIncome = 1;

    public void Init(WalletMB walletMB)
    {
        _walletMB = walletMB;
    }

    public void Update()
    {
    }

    public void IncreaseAmountOfPassiveIncome()
    {
        _value += _passiveIncome;
        MatchText();
    }

    public void IncreaseByAmountActiveIncome()
    {
        _value += _activeIncome;
        MatchText();
    }

    public void IncreasePassiveIncome(int value)
    {
        _passiveIncome += value;
    }

    public void SetPassiveIncome(int value)
    {
        _passiveIncome = value;
    }

    public void ReduceValue(int value)
    {
        _value -= value;
        MatchText();
    }

    public void SetText(string targetText)
    {
        _walletMB.TMPValue.text = targetText;
    }

    private void MatchText()
    {
        if(_walletMB != null)
        _walletMB.TMPValue.text = _value.ToString();
    }
}