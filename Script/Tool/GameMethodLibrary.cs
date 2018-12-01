using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class GameMethodLibrary {
    public static string ConversionOfMoneyUnitsBigInteger2str(BigInteger value, int accuracy = 3)
    {
        string ret = "";
        ret = ((decimal)value / 1000000).ToString("f" + accuracy.ToString());
        return ret;
    }

    public static decimal ConversionOfMoneyUnitsBigInteger2decimal(BigInteger value)
    {
        decimal ret = 0;
        ret = ((decimal)value / 1000000);
        return ret;
    }


    public static BigInteger ConversionOfMoneyUnitsstr2BigInteger(string value)
    {

        BigInteger ret = 0;
        decimal d = decimal.Parse(value);
        d *= 1000000;
        ret = (BigInteger)d;       
        return ret;
    }

    public static BigInteger BigIntegerMultiplyFloat(BigInteger b, float f)
    {
        BigInteger ret = 0;
        float f100 = f * 100;
        ret = (b * (BigInteger)f100) / 100;
        return ret;
    }
}
