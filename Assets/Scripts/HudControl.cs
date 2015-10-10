using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudControl : MonoBehaviour
{
	// Reference Variables
    private Transform module
    {
        get { return transform.FindChild("Module"); }
    }
    private Transform ammo
    {
        get { return module.FindChild("Ammo"); }
    }
    private Text ammoCur
    {
        get { return ammo.FindChild("Cur").GetComponent<Text>(); }
    }
    private Transform health
    {
        get { return module.FindChild("Health"); }
    }
    private RectTransform healthCur
    {
        get { return health.FindChild("Cur").GetComponent<RectTransform>(); }
    }

    // Object Variables
    private float maxLength;

    private void Awake()
    {
        ammo.FindChild("Total").GetComponent<Text>().text = Player.maxAmmo.ToString();
        maxLength = healthCur.anchorMax.x - healthCur.anchorMin.x;
        Player.HealthUpdate += UpdatePlayerHealth;
        Player.AmmoUpdate += UpdatePlayerAmmo;
    }

    private void UpdatePlayerAmmo(int curAmmo)
    {
        ammoCur.text = curAmmo.ToString();
    }

    private void UpdatePlayerHealth(int curHealth)
    {
        float percentage = (float)curHealth / Player.maxHealth;
        healthCur.anchorMax = new Vector2(maxLength * percentage, healthCur.anchorMax.y);
    }


}
