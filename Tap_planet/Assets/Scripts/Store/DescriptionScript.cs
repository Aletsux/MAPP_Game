using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionScript : MonoBehaviour
{
    public Image sprite;
    public TMP_Text itemName;
    public TMP_Text description;
    public TMP_Text price;


    

    public void GetAllInformation(ItemScript item)
    {
        itemName.text = item.ReturnName();
        description.text = item.ReturnDescription();
        sprite.sprite = item.ReturnImage();
        price.text = item.ReturnPrice();
    }

<<<<<<< HEAD
=======
    public void GetAllInformation(ItemScript item)
    {
        if (item.Equals(currentItem) && active)
        {
            active = false;
        }
        else
        {
            itemName.text = item.ReturnName();
            description.text = item.ReturnDescription();
            sprite.sprite = item.ReturnImage();
            price.text = item.ReturnPrice();
>>>>>>> parent of b9ee07c0 (...)


<<<<<<< HEAD


}
=======
    private void Toggle(bool b)
    {
        gameObject.SetActive(b);
    }

    private void setObjectInactive()
    {
        gameObject.SetActive(false);
    }
}
>>>>>>> parent of b9ee07c0 (...)
