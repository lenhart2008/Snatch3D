using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DataManager : MonoBehaviour {
    static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(DataManager)) as DataManager;
            }
            return _instance;
        }
    }


    [DllImport("__Internal")]
    private static extern string GetAccount();

    [DllImport("__Internal")]
    private static extern string InitJsLib();
    public static string account = "";
    public static string account1 = "0xf7d98f221f2e56c173f12c4b1cf750c5ce61da09";
    public static string account2 = "0xfcb3b2fabed73f331a45d500964b3317fd3e543e";
    public TreasurePlayerDataControl playerDataCtrl;
    public JackpotDataControl jackpotDataCtrl;
    public SnatchTarListDataControl snatchTarListDataCtrl;
    public SnatchLetterDataControl snatchLetterDataCtrl;

    public SnatchTarData curSelectPlayerData;
   
    Dictionary<Type, object> treasureDataDic;
    // Use this for initialization
    void Start () {
        InitJsLib();
    }

    public void Login()
    {
        StartCoroutine(GetExternalAccount());
    }

    public IEnumerator GetExternalAccount()
    {
        var wait = 2;
        var acc = GetAccount();
        account = acc.ToString();
        while (account == null)
        {
            yield return new WaitForSeconds(wait);
        }

        account = acc.ToString();
        Init();     
    }

    public void Init()
    {       
        InitPlayerData();
        InitJackPotData();
        InitSnatchTarListData();
        InitSnatchLetterData();
    }

    private void InitSnatchLetterData()
    {
        if (snatchLetterDataCtrl == null)
        {
            GameObject go = new GameObject("SnatchLetterDataControl");
            go.transform.SetParent(this.transform, false);
            snatchLetterDataCtrl = go.AddComponent<SnatchLetterDataControl>();
        }
        snatchLetterDataCtrl.Init();
    }

    private void InitSnatchTarListData()
    {
        if (snatchTarListDataCtrl == null)
        {
            GameObject go = new GameObject("SnatchTarListDataControl");
            go.transform.SetParent(this.transform, false);
            snatchTarListDataCtrl = go.AddComponent<SnatchTarListDataControl>();
        }
        snatchTarListDataCtrl.Init();
    }

    private void InitJackPotData()
    {
        if (jackpotDataCtrl == null)
        {
            GameObject go = new GameObject("JackpotDataControl");
            go.transform.SetParent(this.transform, false);
            jackpotDataCtrl = go.AddComponent<JackpotDataControl>();
        }
        jackpotDataCtrl.Init();
    }

    private void InitPlayerData()
    {
        if (playerDataCtrl == null)
        {
            GameObject go = new GameObject("TreasurePlayerDataControl");
            go.transform.SetParent(this.transform, false);
            playerDataCtrl = go.AddComponent<TreasurePlayerDataControl>(); 
        }
        playerDataCtrl.Init();
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
