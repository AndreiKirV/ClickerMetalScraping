using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefabsController : MonoBehaviour
{
    [SerializeField] private PlayerPrefabConfig _fansCfg;
    [SerializeField] private PlayerPrefabConfig _paleCfg;
    [SerializeField] private PlayerPrefabConfig _weaponCfg;
    [SerializeField] private PlayerPrefabConfig _hairCfg;
    [SerializeField] private PlayerPrefabConfig _maskCfg;
    [SerializeField] private PlayerPrefabConfig _backgroundCfg;
    [SerializeField] private Config _config;

    [Serializable]
    private class Config
    {
        public float MultiplyingFactor;
        public TimerMB TimerMB;
        public WalletMB WalletMB;
        public WalletMB WalletFansMB;
        public Button PlayerButton;
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

        SetButtonSubscriptions();

        _walletController.SetValue(999999);
    }

    private void SetButtonSubscriptions()
    {
        _config.PlayerButton.onClick.AddListener(() =>
        {
            _walletController.IncreaseByAmountActiveIncome();
        });

        _fansCfg.Button.onClick.AddListener(() =>
        {
            if (_walletController.Value >= _fansCfg.Price)
            {
                _walletFansController.IncreaseByAmountActiveIncome();
                _walletController.SetPassiveIncome(_walletFansController.Value);
                _walletController.ReduceValue(_fansCfg.Price);
                _fansCfg.Price = (int)Math.Ceiling(_fansCfg.Price * _config.MultiplyingFactor);
            }
        });

        _weaponCfg.Button.onClick.AddListener(() =>
        {
            if (_walletController.Value >= _weaponCfg.Price)
            {
                _walletController.SetActiveIncome((int)Math.Ceiling(_walletController.ActiveIncome * _config.MultiplyingFactor));
                _walletController.ReduceValue(_weaponCfg.Price);
                _weaponCfg.Price = (int)Math.Ceiling(_weaponCfg.Price * _config.MultiplyingFactor);
            }
        });
    }

    private void InitPlayerPrefabButtons()
    {
        _fansCfg.ElementMB.Init(_fansCfg);
        _paleCfg.ElementMB.Init(_paleCfg);
        _weaponCfg.ElementMB.Init(_weaponCfg);
        _hairCfg.ElementMB.Init(_hairCfg);
        _maskCfg.ElementMB.Init(_maskCfg);
        _backgroundCfg.ElementMB.Init(_backgroundCfg);

        _fansCfg.ElementMB.SetWallet(_walletController);
        _weaponCfg.ElementMB.SetWallet(_walletController);
    }

    private void Update()
    {
        _timerController.Update();
    }
}