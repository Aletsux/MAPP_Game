using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMute : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] buttonSprites;
    public Image targetButton;

    public void ChangeSprite() {
        if(MenuScript.getIsMuted()) {
            targetButton.sprite = buttonSprites[1];
            return;
        } 
        targetButton.sprite = buttonSprites[0]; 
    }
    
}
