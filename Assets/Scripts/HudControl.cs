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
    private Transform itemModule
    {
        get { return transform.FindChild("ItemModule"); }
    }
    private Transform ammo
    {
        get { return module.FindChild("Ammo"); }
    }
    private Text ammoCur
    {
        get { return ammo.FindChild("Cur").GetComponent<Text>(); }
    }
    private Slider health
    {
        get { return module.FindChild("Health").GetComponent<Slider>(); }
    }

    private void Awake()
    {
        ammo.FindChild("Total").GetComponent<Text>().text = Player.maxAmmo.ToString();
        Player.HealthUpdate += UpdatePlayerHealth;
        Player.AmmoUpdate += UpdatePlayerAmmo;
        Player.ItemUpdate += UpdatePlayerItems;
    }

    private void UpdatePlayerItems(string itemName)
    {
        throw new System.NotImplementedException();
    }

    private void UpdatePlayerAmmo(int curAmmo)
    {
        ammoCur.text = curAmmo.ToString();
    }

    private void UpdatePlayerHealth(int curHealth)
    {
        float percentage = (float)curHealth / Player.maxHealth;
        health.value = percentage;
    }
}
