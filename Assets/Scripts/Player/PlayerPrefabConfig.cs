using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerPrefabConfig
{
    public string Name;
    public List<GameObject> Prefs;
    public GameObject CurrentItem;
    public Button Button;
    public Transform Parent;
    public NotAlone DistributionParametersNotAlone;
    public GameObject _elementMB;

    [Serializable]
    public struct NotAlone
    {
        public bool Initialized;
        public Vector2 MaxSize;
        public int Rows;
        public int Colomns;

    }
}