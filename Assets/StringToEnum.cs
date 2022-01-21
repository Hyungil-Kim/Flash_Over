using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StringToEnum
{
    public static T SToE<T>(string st)
    {
        return (T)Enum.Parse(typeof(T), st);
    }
}
