using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerData
{
    public UInt64 id;
    public UInt64 publicKey;
    public UInt64 treasureNum;
    public UInt64 shieldTime;
    public decimal playerETH;
}

public class TreasureContract {
   

    public Dictionary<UInt64, PlayerData> dic_PlayerID2PlayerData;
    public decimal totalETH;
    public decimal curTreasurePrice;
    public static UInt64 curTreasureSN;
    public decimal authorETH;
    const decimal TreasurePriceIncrease = 0.1m;
    static UInt64 PlayerIdSN = 0;
    public static TreasureContract _instance;
    public static TreasureContract Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TreasureContract();
                _instance.Init();
            }
            return _instance;
        }
    }

    private void Init()
    {
        curTreasureSN = 0;
        curTreasurePrice = 1;
        dic_PlayerID2PlayerData = new Dictionary<UInt64, PlayerData>();
    }

    public bool CreateAccount(UInt64 playerPublicKey)
    {
        PlayerIdSN++;
        PlayerData data = new PlayerData();
        data.id = PlayerIdSN;
        data.publicKey = playerPublicKey;
        data.treasureNum = 0;
        data.playerETH = 0;
        dic_PlayerID2PlayerData.Add(data.id, data);
        return true;
    }

    public bool AddPlayerETH(UInt64 playerId, decimal price)
    {
        if (dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            dic_PlayerID2PlayerData[playerId].playerETH += price;
            return true;
        }

        return false;

    }

    public bool SubPlayerETH(UInt64 playerId, decimal price)
    {
        if (dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            if (dic_PlayerID2PlayerData[playerId].playerETH >= price)
            {
                dic_PlayerID2PlayerData[playerId].playerETH -= price;
                return true;
            }
        }

        return false;
    }

    public bool AddPlayerTreasure(UInt64 playerId, UInt64 num)
    {
        if (dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            dic_PlayerID2PlayerData[playerId].treasureNum += num;
            return true;
        }

        return false;
    }

    public bool SubPlayerTreasure(UInt64 playerId, UInt64 num)
    {
        if (dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            if (dic_PlayerID2PlayerData[playerId].treasureNum >= num)
            {          
                dic_PlayerID2PlayerData[playerId].treasureNum -= num;
                return true;
            }
        }

        return false;
    }

    public bool ShareETH(decimal value)
    {
        UInt64 totalTreasure = 0;
        
        foreach (var item in dic_PlayerID2PlayerData)
        {
            totalTreasure += item.Value.treasureNum;
        }
        decimal valuePreTreasure = value / totalTreasure;

        foreach (var item in dic_PlayerID2PlayerData)
        {
            decimal profit = valuePreTreasure * item.Value.treasureNum;
            item.Value.playerETH += profit;
            Debug.Log("PlayerID = " + item.Key + "Get " + profit + "ETH");
        }

        return true;
    }

    public bool AddTotalETH(decimal value)
    {
        totalETH += value;
        return true;
    }

    public bool SubTotalETH(decimal value)
    {
        totalETH -= value;
        return true;
    }

    public bool AddAuthorETH(decimal value)
    {
        if (value >= 0)
        {
            authorETH += value;
            return true;
        }

        return false;
    }
    
    public decimal CalTreasurePrice(UInt64 num)
    {
        decimal price = 0;
        decimal curTPrice = curTreasurePrice;
        for (UInt64 i = 0; i < num; i++)
        {
            curTPrice += (i * TreasurePriceIncrease);
            price += curTPrice;
        }
        return price;
    }

    public bool BuyTreasure(UInt64 playerId, UInt64 num, decimal price)
    {
        decimal eth2Author = price * 0.01m;
        decimal eth2Share = price * 0.1m;
        decimal eth2player = price - (eth2Author + eth2Share);

        if (AddAuthorETH(eth2Author)  && AddPlayerTreasure(playerId, num) && AddPlayerETH(playerId, eth2player) && ShareETH(eth2Share) && AddTreasureSN(num))
        {
            return true;
        }

        return false;
    }

    private bool AddTreasureSN(UInt64 num)
    {
        curTreasureSN += num;
        curTreasurePrice += num * TreasurePriceIncrease;
        return true;
    }
    public decimal GetSnatchMaxETHbyPlayerId(UInt64 playerId)
    {
        decimal ret = 0;
        if (dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            ret = dic_PlayerID2PlayerData[playerId].playerETH * 0.25m;
        }
        return ret;
    }

    public decimal GetSnatchRate(decimal PlayerETH, decimal SnatchPay)
    {
      
        decimal rate = 0;
        if (PlayerETH != 0)
        {
            rate = SnatchPay / (PlayerETH * 1.1m);
        }

        if (rate > 1)
        {
            rate = 1;
        }

        return rate;
    }


    public bool SnatchPlayerETH(UInt64 ThiefId, UInt64 tarId, decimal PlayerETH, decimal SnatchPay)
    {
        //计算成功率
        decimal rate = GetSnatchRate(PlayerETH, SnatchPay);
        bool ret = false;
        //if成功
        decimal rand = (decimal)UnityEngine.Random.Range(0, 1f);
        if (rate >= rand)
        {
            //playerId减钱
            SubPlayerETH(tarId, PlayerETH);
            //ThiefId加钱
            AddPlayerETH(ThiefId, PlayerETH);
            ret = true;
        }
        //分SnatchPrice playerId分89% autho分1% 其他人分10%
        decimal eth2Author = SnatchPay * 0.01m;
        decimal eth2Share = SnatchPay * 0.1m;
        decimal eth2player = SnatchPay - (eth2Author + eth2Share);

        if (AddAuthorETH(eth2Author) && ShareETH(eth2Share) && AddPlayerETH(tarId, eth2player))
        {
            Debug.Log("Share SnatchPlayerETH ");
        }
        return ret;
    }

}
