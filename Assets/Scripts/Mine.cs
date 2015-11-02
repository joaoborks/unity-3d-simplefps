using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour 
{
    // Customizeable Variables
    public GameObject explosion;
    [Range(0.5f, 3f)]
    public float explodeTime = 1f;
    [Range(5, 50)]
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            Invoke("Explode", explodeTime);
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] cols = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider col in cols)
        {
            if (col.tag == "Player")
                col.SendMessage("TakeDamage", damage);
        }
        Destroy(gameObject);
    }
}
