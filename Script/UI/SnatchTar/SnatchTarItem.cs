using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnatchTarItem : MonoBehaviour {
    public Text playerName;
    public Text money;
    public Text time;
    Action<SnatchTarData> OnItemSelectedCallBack;
    Action refreshSnatchTarDataAction;
    SnatchTarData _data;

    public CountDownComponent countDownCom;

    public float refreshIntervalTime = 2;
    public float curRefreshTime = 0;

    // Use this for initialization
    void Start () {
        UIToggle toggle = GetComponent<UIToggle>();
        if (toggle != null)
        {
            toggle.RegisterToggleValueChangeCallBack(OnTarObjectClick);
        }
        countDownCom.timeChangeCallback = OnShieldCountDownChange;
    }

    public void OnShieldCountDownChange(string value)
    {
        time.text = value;
    }


    public void OnTarObjectClick(bool isOn)
    {
        if (isOn)
        {
            if (OnItemSelectedCallBack != null)
            {
                OnItemSelectedCallBack(_data);
            }
        }
    }

    public void RegisterToggleValueChangeCallBack(Action<SnatchTarData> callback)
    {
        OnItemSelectedCallBack = callback;
    }
    // Update is called once per frame
    void Update () {
        if (curRefreshTime < refreshIntervalTime)
        {
            curRefreshTime += Time.deltaTime;
        }
        else
        {
            curRefreshTime = 0;
            RefreashDataUI();
        }
    }

    public void Init(SnatchTarData data)
    {
        _data = data;
        data.RegisterDataChangeCallBack(UpdateUIData);
        UpdateUIData();
    }

    public void RefreashDataUI()
    {
        if (_data != null)
        {
            _data.RefreshData();
        }
        
    }

    public void UpdateUIData()
    {
        if (_data != null)
        {
            playerName.text = _data.address.ToString();
            money.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(_data.tarEth/5);
            countDownCom.SetShowTime((int)_data.shieldTime);
        }
    }
}
