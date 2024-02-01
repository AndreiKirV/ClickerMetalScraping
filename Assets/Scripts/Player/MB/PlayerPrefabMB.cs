using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerPrefabMB : MonoBehaviour
{
    private PlayerPrefabConfig _prefabConfig;
    private int _currentIndex = -1;
    private WalletController _wallet;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public void SetWallet(WalletController wallet)
    {
        _wallet = wallet;
    }

    public void Init(PlayerPrefabConfig cfg)
    {
        _prefabConfig = cfg;

        _prefabConfig.Button.onClick.AddListener(() =>
        {
            bool checkPrice = true;

            if (_wallet != null)
                checkPrice = _prefabConfig.Price <= _wallet.Value;

            if (checkPrice)
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
            }
        }

    }

    private void ActivateNextItem()
    {
        if (_currentIndex == -1)
        {
            InstantiatePref(0);
            _currentIndex = 1;
        }
        else if (_currentIndex < _prefabConfig.Prefs.Count && _currentIndex >= 0)
        {
            if (_prefabConfig.DistributionParametersNotAlone.Initialized != true)
                Destroy(_prefabConfig.CurrentItem);

            InstantiatePref(_currentIndex);
            _currentIndex++;
        }
        else if (_currentIndex >= _prefabConfig.Prefs.Count)
        {
            if (_prefabConfig.DistributionParametersNotAlone.Initialized != true)
                Destroy(_prefabConfig.CurrentItem);

            _currentIndex = 0;
            InstantiatePref(_currentIndex);
            _currentIndex = 1;
        }

        if (_prefabConfig.DistributionParametersNotAlone.Initialized == true)
        {
            RectTransform rectTransform = _prefabConfig.CurrentItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GiveRandomVector();

            SetIndexSort(_prefabConfig.CurrentItem);
        }
    }

    private void SetIndexSort(GameObject targetObj)
    {
        GameObject tempObj = null;

        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            GameObject currentObj = _spawnedObjects[i];

            if (tempObj != null)
            {
                if (currentObj.transform.position.y < targetObj.transform.position.y && currentObj.transform.position.y > tempObj.transform.position.y)
                {
                    tempObj = _spawnedObjects[i];
                }
            }
            else if (currentObj.transform.position.y < targetObj.transform.position.y)
            {
                tempObj = _spawnedObjects[i];
            }
        }

        if (tempObj != null)
        {
            targetObj.transform.SetSiblingIndex(tempObj.transform.GetSiblingIndex());
        }
    }

    private void InstantiatePref(int index)
    {
        _prefabConfig.CurrentItem = Instantiate(_prefabConfig.Prefs[index], _prefabConfig.Parent);

        if (_prefabConfig.DistributionParametersNotAlone.Initialized == true)
            _spawnedObjects.Add(_prefabConfig.CurrentItem);
    }

    private Vector2 GiveRandomVector()
    {
        float x = UnityEngine.Random.Range(0f, _prefabConfig.DistributionParametersNotAlone.MaxSize.x);
        float y = UnityEngine.Random.Range(0, _prefabConfig.DistributionParametersNotAlone.MaxSize.y);
        Vector2 vec = new Vector2(x, y);
        return vec;
    }
}