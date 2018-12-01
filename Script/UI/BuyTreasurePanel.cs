using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class BuyTreasurePanel : MonoBehaviour {
    public Text priceText;
    public Button okBtn;
    public Button cancelBtn;
    public InputField inputField;
    BigInteger curPrice;
    public Transform EffectRoot;
    public GameObject CoinEffect;
    public MsgBox msgbox;
    UInt64 treasureNum;

    public float refreshIntervalTime = 5;
    public float curRefreshTime = 0;

    CS_GetTreasurePrice_Packet packet = null;
    // Use this for initialization
    void Start () {
        InitGetTreasurePricePack();
    }

    private void InitGetTreasurePricePack()
    {
        GameObject go = new GameObject("BuyTreasurePanel_CS_GetTreasurePrice_Packet");
        go.transform.SetParent(this.transform, false);
        packet = go.AddComponent<CS_GetTreasurePrice_Packet>();
        packet.callback = UpdateTreasurePrice;
    }

    public void Init()
    {
        Reset();
    }

    private void Awake()
    {

    }

    public void Reset()
    {
        treasureNum = 0;
        curPrice = 0;
        inputField.text = "Enter the number";
        priceText.text = "0";
    }

    // Update is called once per frame
    void Update () {
        //if (curRefreshTime < refreshIntervalTime)
        //{
        //    curRefreshTime += Time.deltaTime;
        //}
        //else
        //{
        //    curRefreshTime = 0;
        //    RefreshKeyPrice();
        //}

    }

    private void RefreshKeyPrice()
    {
      
    }

    public void OnOKBTNClick()
    {
        if (inputField.text.Length > 0 && curPrice > 0)
        {
            int num = int.Parse(inputField.text);
            BuyTreasure(num);
        }
        
        //UInt64 playerId = LoginPanel.Instance.curSelectPlayerId;
        //bool ret = TreasureContract.Instance.BuyTreasure(playerId, treasureNum, curPrice);
        //if (ret)
        //{
        //    PlayerDataPanel.Instance.ShowCurSelectPlayerData(playerId);
        //    CreateCoinEffect((int)treasureNum);
        //    inputField.text = "0";
        //    //PanelMng.Instance.ShowMsgBox("Success!!!");
        //}
        
        
        //SendTransation.TestTransaction(72);
    }

    public void BuyTreasure(int num)
    {
  
        SendTransation.BuyTransaction(num, curPrice.ToString());
        Reset();
        //CreateCoinEffect(num);
    }

    public void OnP1BtnClick()
    {
        PlusInputfieldValue(1);
    }

    public void OnP10BtnClick()
    {
        PlusInputfieldValue(10);
    }

    public void OnP100BtnClick()
    {
        PlusInputfieldValue(100);
    }

    public void PlusInputfieldValue(int num)
    {
        int curvalue = 0;
        if (inputField.text.Length != 0)
        {
            curvalue = int.Parse(inputField.text);
        }
        curvalue += num;
        inputField.text = curvalue.ToString();
        OnInputFieldValueChange();
    }

    private void CreateCoinEffect(int num)
    {
        GameObject effect = GameObject.Instantiate(CoinEffect);
        CoinEffect ce = effect.GetComponent<CoinEffect>();
        ce.GenerateCoin(num);
        effect.transform.SetParent(EffectRoot, false);
    }

    public void OnCancelBTNClick()
    {
        UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_JackPot);
    }

    public void OnInputFieldValueChange()
    {
        
        string text = inputField.text;
        if (text.Length > 0)
        {
            UInt64 num = UInt64.Parse(inputField.text);
            if (num > 3000)
            {
                inputField.text = 3000.ToString();
                num = 3000;
            }
            GetTreasurePrice(num);
            //decimal price = TreasureContract.Instance.CalTreasurePrice(UInt64.Parse(inputField.text));
            //priceText.text = price.ToString();
            //curPrice = price;
            //treasureNum = num;
        }
       
    }
    public void GetTreasurePrice(UInt64 num)
    {        
        packet.TreasureNum = num;
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);

    }

    public void UpdateTreasurePrice(BigInteger price)
    {
        curPrice = price;
        decimal value = (decimal)price ;
        value /= 1000000;
        priceText.text = value.ToString("f3");
        packet.TreasureNum = 0;
    }
}
