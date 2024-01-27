using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefabsController : MonoBehaviour
{
    [SerializeField] private List<PlayerPrefabConfig> _playerPrefabConfigs;
    [SerializeField] private TimerMB _timerMB;
    [SerializeField] private WalletMB _walletMB;
    [SerializeField] private WalletMB _walletFansMB;
    [SerializeField] private Button _playerButton;
    [SerializeField] private Button _fansButton;

    private TimerController _timerController = new TimerController();
    private WalletController _walletController = new WalletController();
    private WalletController _walletFansController = new WalletController();
    
    void Start()
    {
        _timerController.Init(_timerMB);
        _walletController.Init(_walletMB);
        _walletFansController.Init(_walletFansMB);

        _timerController.SetSecTickAction(_walletController.IncreaseAmountOfPassiveIncome);
        _playerButton.onClick.AddListener(_walletController.IncreaseByAmountActiveIncome);

        _fansButton.onClick.AddListener(_walletFansController.IncreaseByAmountActiveIncome);
        _fansButton.onClick.AddListener(() => _walletController.SetPassiveIncome(_walletFansController.Value));
    }

    void Update()
    {
        _timerController.Update();
        _walletController.Update();
    }
}