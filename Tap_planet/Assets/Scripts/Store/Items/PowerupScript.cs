using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PowerupScript : ItemScript
{
    protected Text amountText;
    protected string amountKey = "powerupAmount";
    public bool isUsingAmount;

    public override void Start()
    {
        if(isUsingAmount) {
        amountText = transform.GetChild(3).GetComponent<Text>();
        }
        
        table = "ButtonsPowerup";
        
        if (amountText != null)
            SetAmountText();
        
        base.Start();
        descriptionPrice += "Powerup " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }

    public override void OnBuyClick()
    {
        SetDescriptionTranslations();
        store.BuyPowerUp(title);
        SetBuyButtonText();
        if (isUsingAmount)
        {
            SetAmountText();
        }       
        desc.GetAllInformation(this, true);
    }

    public virtual void SetAmountText()
    {
        string amount = (GameController.GetPowerupAmount(title) == 0) ? "" : LocalizationSettings.StringDatabase.GetLocalizedString(table, amountKey) + GameController.GetPowerupAmount(title);
        amountText.text = amount;
    }
}