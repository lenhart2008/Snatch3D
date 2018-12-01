using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TreasurePlayerData : MonoBehaviour
{
    string _address;
    public string address
    {
        get
        {
            return _address;
        }
    }

    BigInteger _award;
    public BigInteger award
    {
        get
        {
            return _award;
        }
    }

    BigInteger _treasureNum;
    public BigInteger treasureNum
    {
        get
        {
            return _treasureNum;
        }
    }

    BigInteger _shieldTime;
    public BigInteger shieldTime
    {
        get
        {
            return _shieldTime;
        }
    }

    BigInteger _playerETH;
    public BigInteger playerETH
    {
        get
        {
            return _playerETH;
        }
    }

    Action callback;
    CS_GetPlayerData_Packet packet = null;

    public void Init(string playerAddress)
    {
        _address = playerAddress;
        InitPacket();

    }

    private void InitPacket()
    {
        GameObject go = new GameObject("TreasurePlayerData_CS_GetPlayerData_Packet");
        go.transform.SetParent(this.transform, false);
        packet = go.AddComponent<CS_GetPlayerData_Packet>();
        packet.PlayerAddress = address;
        packet.callback = UpdateData;
    }

    public void RefreshData()
    {
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);
    }


    void UpdateData(GetPlayerDateDTO data)
    {
        _playerETH = data.eth;
        _award = data.award;
        _treasureNum = data.treasureNum;
        _shieldTime = data.shieldtime;

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

public class TreasurePlayerDataControl : MonoBehaviour
{
    public TreasurePlayerData playerData;
    Action<TreasurePlayerData> dataChangeCallback;
    
    public void Init()
    {
        GameObject go = new GameObject("TreasurePlayerData");
        go.transform.SetParent(this.transform, false);
        playerData = go.AddComponent<TreasurePlayerData>();
        playerData.Init(DataManager.account);
        playerData.RegisterDataChangeCallBack(OnDataChange);

        RefreshData();
    }



    public void ResigerDataChangeCallBack(Action<TreasurePlayerData> callback)
    {
        dataChangeCallback += callback;
    }

    public void UnResigerDataChangeCallBack(Action<TreasurePlayerData> callback)
    {
        dataChangeCallback -= callback;
    }

    public void RefreshData()
    {
        playerData.RefreshData();
    }

    public void OnDataChange()
    {       
        if (dataChangeCallback != null)
        {
            dataChangeCallback(playerData);
        }
    }
}

