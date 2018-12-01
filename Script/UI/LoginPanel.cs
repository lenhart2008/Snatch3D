using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {

    public static LoginPanel _instance;
    public static LoginPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LoginPanel)) as LoginPanel;
            }
            return _instance;
        }
    }

    public Button CreateAccountBtn;
    public Dropdown accountDropDowm;

    public Action<UInt64> onCurSelectPlayerIdChangeCallback;  
    UInt64 _curSelectPlayerId = 123456789;
    public UInt64 curSelectPlayerId
    {
        get
        {
            return _curSelectPlayerId;
        }
        set
        {
            _curSelectPlayerId = value;
            onCurSelectPlayerIdChangeCallback(_curSelectPlayerId);
        }
    }

    public void ResigerOnCurSelectPlayerIdChangeCallback(Action<UInt64> callback)
    {
        onCurSelectPlayerIdChangeCallback = callback;
    }

    public void RefreshUI()
    {
        accountDropDowm.ClearOptions();
        List<Dropdown.OptionData> dataList = new List<Dropdown.OptionData>();
        foreach (var item in TreasureContract.Instance.dic_PlayerID2PlayerData)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = item.Key.ToString();
            dataList.Add(data);
        }
        accountDropDowm.AddOptions(dataList);
    }

    public void OnCreateAccountBtnClick()
    {
        UInt64 randKey = (UInt64)UnityEngine.Random.Range(0, int.MaxValue);
        TreasureContract.Instance.CreateAccount(randKey);
        RefreshUI();
    }

    public void OnDropDownSelect(int id)
    {
        UInt64 playerId = UInt64.Parse(accountDropDowm.options[id].text);
        curSelectPlayerId = playerId;
    }

    // Use this for initialization
    void Start () {
        RefreshUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
