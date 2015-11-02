using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class HudControl : MonoBehaviour
{
    // Assignment Variables
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot muted;

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
    private GameObject pauseMenu
    {
        get { return transform.FindChild("Pause").gameObject; }
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
        Player.PauseEvent += PauseMenu;
    }

    private void PauseMenu(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            muted.TransitionTo(0);
        }
        else
        {
            Time.timeScale = 1;
            normal.TransitionTo(0);
        }
        pauseMenu.SetActive(pause);
        
    }

    private void UpdatePlayerItems(string itemName)
    {
        itemModule.FindChild(itemName).GetComponent<Toggle>().isOn = true;
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
