using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class WithDrawPanel : MonoBehaviour {

    public InputField inputField;
    public Text priceText;
    UInt64 roundIndex;

    CS_GetWithDrawData_Packet packet = null;
    // Use this for initialization
    void Start () {
		
	}

    private void InitGetWithDrawDataPack()
    {
        if (packet == null)
        {
            GameObject go = new GameObject("WithDrawPanel_CS_GetWithDrawData_Packet");
            go.transform.SetParent(this.transform, false);
            packet = go.AddComponent<CS_GetWithDrawData_Packet>();
            packet.playerAddress = DataManager.account;
            packet.callback = UpdateAwardPrice;
        }
        Reset();
    }

    public void UpdateAwardPrice(BigInteger price)
    {
        decimal value = (decimal)price;
        value /= 1000000;
        priceText.text = value.ToString("f3");
        packet.roundIndex = 0;
    }

    public void Reset()
    {
        inputField.text = "Enter the Round Index";
        priceText.text = "0";
    }

    public void OnInputFieldValueChange()
    {
        string text = inputField.text;
        if (text.Length > 0)
        {
            UInt64 index = UInt64.Parse(inputField.text);        
            GetAwardPriceByRoundIndex(index);
        }

    }

    private void GetAwardPriceByRoundIndex(ulong index)
    {
        packet.roundIndex = index - 1;
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);
    }

    public void Init()
    {
        InitGetWithDrawDataPack();
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void OnOKBTNClick()
    {
        ulong index = ulong.Parse(inputField.text);
        index -= 1;
        Debug.Log("index = " + index.ToString());
        WithDraw(index.ToString());
    }

    public void OnCancelBTNClick()
    {
        
        UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_JackPot);
    }

    private void WithDraw(string round)
    {
        SendTransation.DrawTransaction(round);
    }
}
