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
    public GameObject buttonClose;
    private static ItemScript currentItem;

    private static bool active;

    public void Start()
    {
        active = true;
        Toggle(active);
        buttonClose.GetComponent<Button>().onClick.AddListener(CloseButtonClicked);
    }

    public void GetAllInformation(ItemScript item, bool buy)
    {
        if (item.Equals(currentItem) && active && !buy) 
        {
            active = false;
        }
        else
        {
            itemName.text = item.ReturnName();
            description.text = item.ReturnDescription();
            sprite.sprite = item.ReturnImage();
            price.text = item.ReturnPrice();

            active = true;
        }
        Toggle(active);
        currentItem = item;
    }

    private void Toggle(bool b)
    {
        gameObject.SetActive(b);
    }

    private void CloseButtonClicked() {
        active = false;
        Toggle(active);
    }
}