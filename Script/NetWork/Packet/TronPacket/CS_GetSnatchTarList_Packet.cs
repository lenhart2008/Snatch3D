using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Runtime.InteropServices;


public class CS_GetSnatchTarList_Packet : PacketBase {

    [DllImport("__Internal")]
    private static extern string GetRamdonStealPlayerByAddress(string index,string address, string StrCallbackObjName, string strCallbackFunc);

    public string playerAddress;
    public BigInteger index
    {
        get;
        set;
    }

    public Action<GetStnachTarDataListDTO> callback;
    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetRamdonStealPlayerByAddress(index.ToString(), playerAddress, StrCallbackObjName, strCallbackFunc);
    }

    public override void _Execute(string strData)
    {
        GetStnachTarDataListDTO data = new GetStnachTarDataListDTO(strData);
        if (callback != null)
        {
            callback(data);
        }
    }


}
