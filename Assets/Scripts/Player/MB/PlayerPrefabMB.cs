using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerPrefabMB : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefs;
    [SerializeField] private GameObject _currentItem;
    [SerializeField] private Button _button;
    [SerializeField] private Transform _parent;
    [SerializeField] private NotAlone _distributionParametersNotAlone;
    
    [Serializable]
    public struct NotAlone
    {
        public bool Initialized;
        public Vector2 MaxSize;
        public int Rows;
        public int Colomns;

    }

    private int _currentIndex = -1;
    private float[,] _field;

    private void Start()
    {
        _button.onClick.AddListener(() => ActivateNextItem());

        if (_currentItem != null)
        {
            _currentItem.GetComponent<Image>().enabled = false;
            _parent = _currentItem.transform;
        }
        else if (_parent == null)
            _parent = transform;

        if (_distributionParametersNotAlone.Initialized == true)
        {
            RectTransform rect = _parent.GetComponent<RectTransform>();

            if (_distributionParametersNotAlone.MaxSize.x <= 0 && _distributionParametersNotAlone.MaxSize.y <= 0)
            {
                _distributionParametersNotAlone.MaxSize.x = rect.rect.width;
                _distributionParametersNotAlone.MaxSize.y = rect.rect.height;

                GameObject temp = Instantiate(_prefs[0], _parent);
                RectTransform tempRect = temp.GetComponent<RectTransform>();
                _distributionParametersNotAlone.MaxSize.x -= tempRect.rect.width;
                Destroy(temp);
                _field = new float[_distributionParametersNotAlone.Rows, _distributionParametersNotAlone.Colomns];
            }
        }

    }

    private void ActivateNextItem()
    {
        if (_currentIndex == -1)
        {
            _currentItem = Instantiate(_prefs[0], _parent);
            _currentIndex = 1;
        }
        else if (_currentIndex < _prefs.Count && _currentIndex >= 0)
        {
            if (_distributionParametersNotAlone.Initialized != true)
                Destroy(_currentItem);

            _currentItem = Instantiate(_prefs[_currentIndex], _parent);
            _currentIndex++;
        }
        else if (_currentIndex >= _prefs.Count)
        {
            if (_distributionParametersNotAlone.Initialized != true)
                Destroy(_currentItem);

            _currentIndex = 0;
            _currentItem = Instantiate(_prefs[_currentIndex], _parent);
            _currentIndex = 1;
        }

        if (!_distributionParametersNotAlone.Initialized != true)
        {
            RectTransform rectTransform = _currentItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GiveNumber();
        }
    }

    private Vector2 GiveNumber()
    {
        Vector2 vec = new Vector2(UnityEngine.Random.Range(0f, _distributionParametersNotAlone.MaxSize.x), UnityEngine.Random.Range(0, _distributionParametersNotAlone.MaxSize.y));
        return vec;
    }
}