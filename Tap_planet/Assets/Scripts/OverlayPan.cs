using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
public class OverlayPan : MonoBehaviour
{
    //Elements for slide-in animation
    //Alternative: use animtor / timeline

    private bool visible;

    private void Awake()
    {
        //start inactive
        gameObject.SetActive(false);
        visible = false;
    }

    public void onClick() {
        if(this.visible) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
        toggleVisible();
    }

    public void toggleVisible() {
        visible = !visible;
    }

}
