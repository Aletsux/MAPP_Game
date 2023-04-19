using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DescriptionScript : MonoBehaviour
{
    public Image sprite;
    public Text name;

    public void GetAllInformation(ItemScript item)
    {
        name.text = item.ReturnName();
    }

    //public string GetName(GameObject item)
    //{
    //    //calls items method
    //    return item.ReturnName();
    //}
}



// items button sends itself 