using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatNumbers : MonoBehaviour
{
    public static string FormatInt(double number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }
        else if (number < 1000000)
        {
            return (number / 1000).ToString("F3") + "k";
        }
        else if (number < 1000000000)
        {
            return (number / 1000000).ToString("F3") + "M";
        }
        else
        {
            return (number / 1000000000).ToString("F3") + "B";
        }
    }
}
