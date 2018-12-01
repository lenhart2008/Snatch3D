using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.JsonRpc.UnityClient;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[FunctionOutput]
public class GetPlayerDateDTO
{
    [Parameter("uint256", "eth", 1)]
    public BigInteger eth { get; set; }
    [Parameter("uint256", "award", 2)]
    public BigInteger award { get; set; }
    [Parameter("uint256", "treasureNum", 3)]
    public BigInteger treasureNum { get; set; }
    [Parameter("uint256", "shieldtime", 4)]
    public BigInteger shieldtime { get; set; }

    public GetPlayerDateDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        eth = BigInteger.Parse(data["eth"].ToString());
        award = BigInteger.Parse(data["award"].ToString());
        treasureNum = BigInteger.Parse(data["treasureNum"].ToString());
        shieldtime = BigInteger.Parse(data["shieldtime"].ToString());
    }
}

[FunctionOutput]
public class GetSnatchLetterDataDTO
{
    [Parameter("address", "add", 1)]
    public string address { get; set; }
    [Parameter("uint256", "count", 2)]
    public BigInteger count { get; set; }

    public GetSnatchLetterDataDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        address = data["add"].ToString();
        count = BigInteger.Parse(data["count"].ToString());      
    }
}

[FunctionOutput]
public class GetTreasurePriceDTO
{
    [Parameter("uint256", "treasurePrice", 1)]
    public BigInteger treasurePrice { get; set; }

    public GetTreasurePriceDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        treasurePrice = BigInteger.Parse(data["treasurePrice"].ToString());
    }
}

[FunctionOutput]
public class GetAwardPriceDTO
{
    [Parameter("uint256", "count", 1)]
    public BigInteger count { get; set; }

    public GetAwardPriceDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        count = BigInteger.Parse(data["count"].ToString());
    }
}



[FunctionOutput]
public class GetFinalAwardDataDTO
{
    [Parameter("uint256", "eth", 1)]
    public BigInteger eth { get; set; }
    [Parameter("uint256", "time", 2)]
    public BigInteger time { get; set; }

    public GetFinalAwardDataDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        eth = BigInteger.Parse(data["eth"].ToString());
        time = BigInteger.Parse(data["time"].ToString());
    }
}

[FunctionOutput]
public class GetRoundIndexDataDTO
{
    [Parameter("uint256", "index", 1)]
    public BigInteger roundIndex { get; set; }

    public GetRoundIndexDataDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        roundIndex = BigInteger.Parse(data["index"].ToString());       
    }
}


[FunctionOutput]
public class GetStnachTarDataListDTO
{
    [Parameter("address", "add1", 1)]
    public string add1 { get; set; }
    [Parameter("uint256", "award1", 2)]
    public BigInteger award1 { get; set; }
    
    [Parameter("address", "add2", 3)]
    public string add2 { get; set; }
    [Parameter("uint256", "award2", 4)]
    public BigInteger award2 { get; set; }


    [Parameter("address", "add3", 5)]
    public string add3 { get; set; }
    [Parameter("uint256", "award3", 6)]
    public BigInteger award3 { get; set; }

    [Parameter("uint256", "playerCount", 7)]
    public BigInteger playerCount { get; set; }

    public GetStnachTarDataListDTO(string strData)
    {
        Dictionary<object, object> data = JsonMapper.ToObject<Dictionary<object, object>>(strData);

        add1 = data["add1"].ToString();
        award1 = BigInteger.Parse(data["award1"].ToString());

        add2 = data["add2"].ToString();
        award2 = BigInteger.Parse(data["award2"].ToString());

        add3= data["add3"].ToString();
        award3 = BigInteger.Parse(data["award3"].ToString());

        playerCount = BigInteger.Parse(data["playerCount"].ToString());
    }
}

