using UnityEngine;
using System.Collections;

public class HostilePlane : MonoBehaviour 
{
    // Customizeable Variables
    public AudioClip shootSound;
    public GameObject ammoPrefab;
    [Range(3f, 10f)]
    public float shootTimer = 5f;

    // Reference Variables
    private AudioSource source
    {
        get { return GetComponent<AudioSource>(); }
    }
    private Transform origin
    {
        get { return transform.FindChild("Origin"); }
    }

    // Object Variables
    private Transform target;

    private void Start()
    {
        target = Player.instance.transform;
        StartCoroutine(TimedShooting());
    }

    private void FixedUpdate()
    {
        if (target != null)
            origin.LookAt(target.position + Vector3.up * 2);
    }

    private IEnumerator TimedShooting()
    {
        while (target != null)
        {
            yield return new WaitForSeconds(shootTimer);         
            Instantiate(ammoPrefab, origin.position, origin.rotation);
            source.pitch = Random.Range(0.8f, 1.2f);
            source.PlayOneShot(shootSound);
            source.pitch = 1;
        }
    }
}
