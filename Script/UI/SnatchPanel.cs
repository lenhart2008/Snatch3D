using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class SnatchPanel : MonoBehaviour {
    public Text snatchEthText;
    public Text snatchPayText;
    public Text snatchRateText;
    public Scrollbar SnatchETHSB;
    public Scrollbar SnatchPaySB;

    BigInteger snatchMaxETH = 0;
    BigInteger snatchMaxPay = 0;
    BigInteger curSnatchETH = 0;
    BigInteger curSnatchPay = 0;


    // Use this for initialization
    void Start () {

	}

    public void Init()
    {
        ResetPanel();
        GetTarSnatchData();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDisable()
    {
        ResetPanel();
        DataManager.Instance.curSelectPlayerData = null;
    }

    private void GetTarSnatchDataByContract(ulong tarId)
    {
   
    }

    public void UpdateSnatchDataByContract(UInt64 tarID, UInt64 treasureNum, UInt64 shieldTime)
    {

    }

    private void GetTarSnatchData()
    {
        BigInteger tarEth = DataManager.Instance.curSelectPlayerData.tarEth/5;
        BigInteger playerEth = DataManager.Instance.playerDataCtrl.playerData.playerETH;
        snatchMaxETH = playerEth > tarEth ? tarEth : playerEth;
        snatchMaxPay = snatchMaxETH;
        //ResetScrollBar();
        UpdateSnatchScrollBar();
    }

    private void ResetScrollBar()
    {
        SnatchETHSB.value = 0;
        SnatchPaySB.value = 0;
}

    public void OnSnatchETHChange(float value)
    {
        if (Math.Abs(value) <= 1)
        {
            float value100 = value * 100;
            BigInteger bigvalue = (BigInteger)value / 100;
            curSnatchETH = GameMethodLibrary.BigIntegerMultiplyFloat(snatchMaxETH, value);
            curSnatchPay = GameMethodLibrary.BigIntegerMultiplyFloat(curSnatchETH, SnatchPaySB.value);
            UpdateSnatchScrollBar();
        }       
    }

    public void OnSnatchPayChange(float value)
    {
        if (Math.Abs(value) <= 1)
        {
            curSnatchPay = GameMethodLibrary.BigIntegerMultiplyFloat(curSnatchETH, value);
            UpdateSnatchScrollBar();
        }
    }

    private void UpdateSnatchScrollBar()
    {
        snatchEthText.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(curSnatchETH, 3);
        snatchPayText.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(curSnatchPay, 3);
        snatchRateText.text = GetSnatchRate(curSnatchETH, curSnatchPay).ToString() + "%";
    }


    public BigInteger GetSnatchRate(BigInteger snatchETH, BigInteger payEth)
    {
        BigInteger ret = 0;
        if (snatchETH != 0)
        {
            ret = (payEth * 100) / snatchETH;
        }
        return ret;
    }


    public void OnOKBTNClick()
    {
        //UInt64 playerId = LoginPanel.Instance.curSelectPlayerId;
        //UInt64 snatchTarId = curSelectSnatchTarId;

        //bool ret = TreasureContract.Instance.SnatchPlayerETH(playerId, curSelectSnatchTarId, curSnatchETH, curSnatchPay);

        //if (ret)
        //{
        //    PanelManager.Instance.ShowMsgBox("Success!!!");
        //}
        //else
        //{
        //    PanelManager.Instance.ShowMsgBox("Fail...");
        //}
        //ResetScrollBar();
        //PlayerDataPanel.Instance.ShowCurSelectPlayerData(playerId);

        SnatchTarETH(DataManager.Instance.curSelectPlayerData.address, curSnatchETH.ToString(), curSnatchPay.ToString());
        ResetScrollBar();
        UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_SelectSnatchTar);

    }

    public void SnatchTarETH(string tar, string snatchValue, string payValue)
    {
        SendTransation.SnatchTransaction(tar, snatchValue, payValue);
    }

    public void OnCancelBTNClick()
    {
        
        UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_SelectSnatchTar);
    }

    public void ResetPanel()
    {
        snatchMaxETH = 0;
        snatchMaxPay = 0;
        curSnatchETH = 0;
        curSnatchPay = 0;
        ResetScrollBar();
    }
}
