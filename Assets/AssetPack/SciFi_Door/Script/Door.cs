using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public float closeTime = 5f;

    private AudioSource source
    {
        get { return GetComponent<AudioSource>(); }
    }
    private Animator doorAnim
    {
        get { return transform.parent.FindChild("door").GetComponent<Animator>(); }
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Player>().hasKey)               
                ToggleDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Player>().hasKey)
                ToggleDoor(false);
        }
    }

    private void ToggleDoor(bool value)
    {
        doorAnim.SetBool("open", value);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }
}
