using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class JackpotData : MonoBehaviour
{
    BigInteger _award;
    public BigInteger award
    {
        get
        {
            return _award;
        }
    }

    BigInteger _countdownTime;
    public BigInteger countdownTime
    {
        get
        {
            return _countdownTime;
        }
    }

    BigInteger _roundIndex;
    public BigInteger roundIndex
    {
        get
        {
            return _roundIndex;
        }
    }

    Action callback;
    CS_GetFinalAwardData_Packet packet = null;
    CS_GetCurRoundIndex_Packet roundIndexpacket = null;

    public void Init()
    {
        InitPacket();

    }

    private void InitPacket()
    {
        GameObject go = new GameObject("JackpotData_CS_GetFinalAwardData_Packet");
        go.transform.SetParent(this.transform, false);
        packet = go.AddComponent<CS_GetFinalAwardData_Packet>();
        packet.callback = UpdateData;

        if (roundIndexpacket == null)
        {
            GameObject go2 = new GameObject("JackpotPanel_CS_GetCurRoundIndex_Packet");
            go2.transform.SetParent(this.transform, false);
            roundIndexpacket = go2.AddComponent<CS_GetCurRoundIndex_Packet>();
            roundIndexpacket.callback = UpRoundIndex;
        }
    }

    public void RefreshData()
    {
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);
        NetWorkManager.Instance.ClientSendMessage2Contract(roundIndexpacket);
    }


    void UpdateData(GetFinalAwardDataDTO data)
    {
        _award = data.eth;
        _countdownTime = data.time;

        if (callback != null)
        {
            callback();
        }
    }


    private void UpRoundIndex(GetRoundIndexDataDTO data)
    {
       _roundIndex = data.roundIndex;
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

public class JackpotDataControl : MonoBehaviour {
    public JackpotData jackpotData;
    Action<JackpotData> dataChangeCallback;
    
    public void Init()
    {
        GameObject go = new GameObject("JackpotData");
        go.transform.SetParent(this.transform, false);
        jackpotData = go.AddComponent<JackpotData>();
        jackpotData.Init();
        jackpotData.RegisterDataChangeCallBack(OnDataChange);
        RefreshData();
    }

    public void RefreshData()
    {
        jackpotData.RefreshData();
        
    }

    public void OnDataChange()
    {
        if (dataChangeCallback != null)
        {
            dataChangeCallback(jackpotData);
        }
    }

    public void ResigerDataChangeCallBack(Action<JackpotData> callback)
    {
        dataChangeCallback += callback;
    }

    public void UnResigerDataChangeCallBack(Action<JackpotData> callback)
    {
        dataChangeCallback -= callback;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
