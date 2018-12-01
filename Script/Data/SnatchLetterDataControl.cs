using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SnatchLetterData : MonoBehaviour
{
    string _address;
    public string address
    {
        get
        {
            return _address;
        }
    }

    BigInteger _count;
    public BigInteger count
    {
        get
        {
            return _count;
        }
    }

  

    Action callback;
    CS_GetSnatchLetterData_Packet packet = null;

    public void Init(string playerAddress)
    {
        InitPacket(playerAddress);

    }

    private void InitPacket(string playerAddress)
    {
        GameObject go = new GameObject("TreasurePlayerData_CS_GetSnatchLetterData_Packet");
        go.transform.SetParent(this.transform, false);
        packet = go.AddComponent<CS_GetSnatchLetterData_Packet>();
        packet.PlayerAddress = playerAddress;
        packet.callback = UpdateData;
    }

    public void RefreshData()
    {
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);
    }


    void UpdateData(GetSnatchLetterDataDTO data)
    {
        _address = data.address;
        _count = data.count;


        if (callback != null)
        {
            callback();
        }
    }



    public void RegisterDataChangeCallBack(Action cb)
    {
        callback = cb;
    }
}

public class SnatchLetterDataControl : MonoBehaviour {

    public SnatchLetterData letterData;
    Action<SnatchLetterData> dataChangeCallback;

    public void Init()
    {
        GameObject go = new GameObject("LetterData");
        go.transform.SetParent(this.transform, false);
        letterData = go.AddComponent<SnatchLetterData>();
        letterData.Init(DataManager.account);
        letterData.RegisterDataChangeCallBack(OnDataChange);
        RefreshData();
    }

    public void RefreshData()
    {
        letterData.RefreshData();
    }

    public void OnDataChange()
    {
        if (dataChangeCallback != null)
        {
            dataChangeCallback(letterData);
        }
    }

    public void ResigerDataChangeCallBack(Action<SnatchLetterData> callback)
    {
        dataChangeCallback += callback;
    }

    public void UnResigerDataChangeCallBack(Action<SnatchLetterData> callback)
    {
        dataChangeCallback -= callback;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
