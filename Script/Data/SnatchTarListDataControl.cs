using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SnatchTarData : MonoBehaviour
{
    string _address;
    public string address
    {
        get
        {
            return _address;
        }
    }

    BigInteger _tarEth;
    public BigInteger tarEth
    {
        get
        {
            return _tarEth;
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

    Action callback;
    CS_GetPlayerData_Packet packet = null;

    public void Init(string add)
    {
        _address = add;
        InitPacket();

    }

    private void InitPacket()
    {
        GameObject go = new GameObject("CS_GetPlayerData_Packet" + address);
        go.transform.SetParent(this.transform, false);
        packet = go.AddComponent<CS_GetPlayerData_Packet>();
        packet.PlayerAddress = address;
        packet.callback = UpdateData;
    }



    public void RefreshData()
    {
        NetWorkManager.Instance.ClientSendMessage2Contract(packet);
    }


    public void UpdateData(GetPlayerDateDTO data)
    {
        _tarEth = data.eth;
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



public class SnatchTarListDataControl : MonoBehaviour {
    public List<SnatchTarData> snatchTarDatasList;
    public BigInteger index;
    public BigInteger playercount;
    Action<List<SnatchTarData>> dataChangeCallback;
    CS_GetSnatchTarList_Packet getSnatchTarListpacket = null;
    CS_GetPlayerData_Packet searchTarDatapacket = null;
    GameObject snatchTarDatasListRoot = null;
    string searchAddress;

    public void Init()
    {
        index = 0;
        playercount = 0;
        snatchTarDatasList = new List<SnatchTarData>();
        snatchTarDatasListRoot = new GameObject("snatchTarDatasListRoot");
        snatchTarDatasListRoot.transform.SetParent(this.transform, false);
        InitSearchSnatchTarDataPack();
        InitGetSnatchTarListPack();
        GetSnatchTarDataList();
    }

    private void InitSearchSnatchTarDataPack()
    {
        GameObject go = new GameObject("SnatchTarListDataControl_SearchTarData");
        go.transform.SetParent(this.transform, false);
        searchTarDatapacket = go.AddComponent<CS_GetPlayerData_Packet>();
        searchTarDatapacket.callback = UpdateSearchTarData;
    }

    private void InitGetSnatchTarListPack()
    {
        GameObject go = new GameObject("SnatchTarListDataControl_CS_GetSnatchTarList_Packet");
        go.transform.SetParent(this.transform, false);
        getSnatchTarListpacket = go.AddComponent<CS_GetSnatchTarList_Packet>();
        getSnatchTarListpacket.playerAddress = DataManager.account;
        getSnatchTarListpacket.callback = UpdateSnatchTarDataList; 
    }



    public void GetSnatchTarDataList()
    {
        ClearSnatchTarDatasList();
        RandomIndex();      
        getSnatchTarListpacket.index = index;
        NetWorkManager.Instance.ClientSendMessage2Contract(getSnatchTarListpacket);
    }

    public bool SearchTarDataByAdd(string add)
    {
        if (!AddIsInvalid(searchAddress))
        {
            ClearSnatchTarDatasList();
            searchAddress = add;
            searchTarDatapacket.PlayerAddress = add;
            NetWorkManager.Instance.ClientSendMessage2Contract(searchTarDatapacket);
        }
        else
        {
            return false;
        }
        return true;
    }

    public void UpdateSearchTarData(GetPlayerDateDTO output)
    {
        if (!AddIsInvalid(searchAddress))
        {
            GameObject go = new GameObject("SnatchTarListDataControl_SnatchTarData1");
            go.transform.SetParent(snatchTarDatasListRoot.transform, false);
            SnatchTarData data1 = go.AddComponent<SnatchTarData>();
            data1.Init(searchAddress);
            data1.UpdateData(output);
            snatchTarDatasList.Add(data1);
        }

        if (dataChangeCallback != null)
        {
            dataChangeCallback(snatchTarDatasList);
        }

        searchAddress = "";
    }

    public void UpdateSnatchTarDataList(GetStnachTarDataListDTO output)
    {       
        if (!AddIsInvalid(output.add1))
        {
            GameObject go = new GameObject("SnatchTarListDataControl_SnatchTarData1");
            go.transform.SetParent(snatchTarDatasListRoot.transform, false);
            SnatchTarData data1 = go.AddComponent<SnatchTarData>();
            data1.Init(output.add1);
            data1.RefreshData();
            snatchTarDatasList.Add(data1);
        }

        if (!AddIsInvalid(output.add2))
        {
            GameObject go = new GameObject("SnatchTarListDataControl_SnatchTarData2");
            go.transform.SetParent(snatchTarDatasListRoot.transform, false);
            SnatchTarData data2 = go.AddComponent<SnatchTarData>();
            data2.Init(output.add2);
            data2.RefreshData();
            snatchTarDatasList.Add(data2);
        }

        if (!AddIsInvalid(output.add3))
        {
            GameObject go = new GameObject("SnatchTarListDataControl_SnatchTarData3");
            go.transform.SetParent(snatchTarDatasListRoot.transform, false);
            SnatchTarData data3 = go.AddComponent<SnatchTarData>();
            data3.Init(output.add3);
            data3.RefreshData();
            snatchTarDatasList.Add(data3);
        }
           
        playercount = output.playerCount;

        if (dataChangeCallback != null)
        {
            dataChangeCallback(snatchTarDatasList);
        }
    }

    public void RegisterDataChangeCallBack(Action<List<SnatchTarData>> callback)
    {
        dataChangeCallback += callback;
    }

    public void UnRegisterDataChangeCallBack(Action<List<SnatchTarData>> callback)
    {
        dataChangeCallback -= callback;
    }

    private void ClearSnatchTarDatasList()
    {
        foreach (SnatchTarData item in snatchTarDatasList)
        {
            if (item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
        snatchTarDatasList.Clear();
    }

    public bool AddIsInvalid(string add)
    {
        return (add == "T9yD14Nj9j7xAB4dbGeiX9h8unkKHxuWwb" || add == "") ? true : false;
    }

    private void RandomIndex()
    {
        index = UnityEngine.Random.Range(0, (int)playercount);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
