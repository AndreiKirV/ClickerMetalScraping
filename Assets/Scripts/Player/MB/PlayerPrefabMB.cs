using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerPrefabMB : MonoBehaviour
{
    private PlayerPrefabConfig _prefabConfig;
    private int _currentIndex = -1;
    private float[,] _field;
    private WalletController _wallet;

    public void SetWallet(WalletController wallet)
    {
        _wallet = wallet;
    }

    public void Init(PlayerPrefabConfig cfg)
    {
        _prefabConfig = cfg;

        _prefabConfig.Button.onClick.AddListener(() => {
            bool checkPrice = true;

            if(_wallet != null)
            checkPrice = _prefabConfig.Price <= _wallet.Value;

            if(checkPrice)
            ActivateNextItem();
            });

        if (_prefabConfig.CurrentItem != null)
        {
            _prefabConfig.CurrentItem.GetComponent<Image>().enabled = false;
            _prefabConfig.Parent = _prefabConfig.CurrentItem.transform;
        }
        else if (_prefabConfig.Parent == null)
            _prefabConfig.Parent = transform;

        if (_prefabConfig.DistributionParametersNotAlone.Initialized == true)
        {
            RectTransform rect = _prefabConfig.Parent.GetComponent<RectTransform>();

            if (_prefabConfig.DistributionParametersNotAlone.MaxSize.x <= 0 && _prefabConfig.DistributionParametersNotAlone.MaxSize.y <= 0)
            {
                _prefabConfig.DistributionParametersNotAlone.MaxSize.x = rect.rect.width;
                _prefabConfig.DistributionParametersNotAlone.MaxSize.y = rect.rect.height;

                GameObject temp = Instantiate(_prefabConfig.Prefs[0], _prefabConfig.Parent);
                RectTransform tempRect = temp.GetComponent<RectTransform>();
                _prefabConfig.DistributionParametersNotAlone.MaxSize.x -= tempRect.rect.width;
                Destroy(temp);
                _field = new float[_prefabConfig.DistributionParametersNotAlone.Rows, _prefabConfig.DistributionParametersNotAlone.Colomns];
            }
        }

    }

    private void ActivateNextItem()
    {
        if (_currentIndex == -1)
        {
            _prefabConfig.CurrentItem = Instantiate(_prefabConfig.Prefs[0], _prefabConfig.Parent);
            _currentIndex = 1;
        }
        else if (_currentIndex < _prefabConfig.Prefs.Count && _currentIndex >= 0)
        {
            if (_prefabConfig.DistributionParametersNotAlone.Initialized != true)
                Destroy(_prefabConfig.CurrentItem);

            _prefabConfig.CurrentItem = Instantiate(_prefabConfig.Prefs[_currentIndex], _prefabConfig.Parent);
            _currentIndex++;
        }
        else if (_currentIndex >= _prefabConfig.Prefs.Count)
        {
            if (_prefabConfig.DistributionParametersNotAlone.Initialized != true)
                Destroy(_prefabConfig.CurrentItem);

            _currentIndex = 0;
            _prefabConfig.CurrentItem = Instantiate(_prefabConfig.Prefs[_currentIndex], _prefabConfig.Parent);
            _currentIndex = 1;
        }

        if (!_prefabConfig.DistributionParametersNotAlone.Initialized != true)
        {
            RectTransform rectTransform = _prefabConfig.CurrentItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GiveNumber();
        }
    }

    private Vector2 GiveNumber()
    {
        Vector2 vec = new Vector2(UnityEngine.Random.Range(0f, _prefabConfig.DistributionParametersNotAlone.MaxSize.x), UnityEngine.Random.Range(0, _prefabConfig.DistributionParametersNotAlone.MaxSize.y));
        return vec;
    }
}