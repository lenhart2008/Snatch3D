using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class JackpotPanel : MonoBehaviour {
    public Text moneyText;
    public Text timeText;
    public CountDownComponent countdown;
    public Text roundIndexText;

    public float refreshIntervalTime = 2;
    public float curRefreshTime = 0;

    // Use this for initialization
    void Start () {
        Init();
       
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
            RefreshJackPotDataPanel();
        }
    }

    public void Init()
    {
        countdown.timeChangeCallback = OnTimeChange;
        if (DataManager.Instance.jackpotDataCtrl != null)
        {
            DataManager.Instance.jackpotDataCtrl.ResigerDataChangeCallBack(UpdateJackPotDataUI);
            UpdateJackPotDataUI(DataManager.Instance.jackpotDataCtrl.jackpotData);
        }



    }


    private void RefreshJackPotDataPanel()
    {
        DataManager.Instance.jackpotDataCtrl.RefreshData();
       
    }

    public void OnTimeChange(string value)
    {
        timeText.text = value;
    }

    public void UpdateJackPotDataUI(JackpotData data)
    { 
        moneyText.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(data.award);
        BigInteger index = data.roundIndex + 1;
        roundIndexText.text = index.ToString();
        countdown.SetShowTime((int)data.countdownTime);
    }
}
