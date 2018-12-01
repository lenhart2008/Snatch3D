using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePool : MonoBehaviour {
    public static TreasurePool _instance;
    public static TreasurePool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(TreasurePool)) as TreasurePool;
                _instance.Init();
            }
            return _instance;
        }
    }

    public Action<decimal> totalTreasureETHCallback;
    decimal _totalTreasureETH = 123456789;
    public decimal totalTreasure
    {
        get
        {
            return _totalTreasureETH;
        }
        set
        {
            _totalTreasureETH = value;
            totalTreasureETHCallback(_totalTreasureETH);
        }
    }




    private void Init()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
