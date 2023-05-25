using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PowerupScript : ItemScript
{
    private Text amountText;
    private string amountKey = "powerupAmount";
    public bool isUsingAmount;

    public override void Start()
    {
        if(isUsingAmount) {
        amountText = transform.GetChild(3).GetComponent<Text>();
        }
        
        table = "ButtonsPowerup";
        
        if (amountText != null)
            SetLevelText();
        
        base.Start();
        descriptionPrice += "Powerup " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }

    protected override void OnBuyClick()
    {
        SetDescriptionTranslations();
        store.BuyPowerUp(title);
        SetBuyButtonText();
        if (isUsingAmount)
        {
            SetLevelText();
        }       
        desc.GetAllInformation(this, true);
    }

    public void SetLevelText()
    {
        amountText.text = LocalizationSettings.StringDatabase.GetLocalizedString(table, amountKey) + GameController.GetPowerupAmount(title);
    }
}