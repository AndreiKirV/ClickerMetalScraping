using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefabsController : MonoBehaviour
{
    [SerializeField] private List<PlayerPrefabConfig> _playerPrefabConfigs;
    [SerializeField] private Config _config;

    [Serializable]
    private class Config
    {
        public TimerMB TimerMB;
        public WalletMB WalletMB;
        public WalletMB WalletFansMB;
        public Button PlayerButton;
        public ButtonItem FansButton;

        [Serializable]
        public class ButtonItem
        {
            public Button Button;
            public int Price;
        }
    }

    private TimerController _timerController = new TimerController();
    private WalletController _walletController = new WalletController();
    private WalletController _walletFansController = new WalletController();

    private void Start()
    {
        _timerController.Init(_config.TimerMB);
        _walletController.Init(_config.WalletMB);
        _walletFansController.Init(_config.WalletFansMB);

        InitPlayerPrefabButtons();

        _timerController.SetSecTickAction(_walletController.IncreaseAmountOfPassiveIncome);
        _config.PlayerButton.onClick.AddListener(_walletController.IncreaseByAmountActiveIncome);
        //TODO
        PlayerPrefabMB tempMB = _playerPrefabConfigs.Find(MB => MB.Name == "Fans").ElementMB;
            bool isPurchaseOption = _walletController.Value > _config.FansButton.Price;
            tempMB.SetCondition(ref isPurchaseOption);

        _config.FansButton.Button.onClick.AddListener(() =>
        {
            

            if (_walletController.Value >= _config.FansButton.Price)
            {
                _walletFansController.IncreaseByAmountActiveIncome();
                _walletController.SetPassiveIncome(_walletFansController.Value);
                _walletController.ReduceValue(_config.FansButton.Price);
            }
        });
        //TODO
    }

    private void InitPlayerPrefabButtons()
    {
        foreach (var item in _playerPrefabConfigs)
            item.ElementMB.Init(item);
    }

    private void Update()
    {
        _timerController.Update();
    }
}