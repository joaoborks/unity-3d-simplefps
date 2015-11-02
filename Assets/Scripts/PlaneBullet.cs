using UnityEngine;
using System.Collections;

public class PlaneBullet : MonoBehaviour
{
    // Customizeable Variables
    public GameObject explosion;
    [Range(3f, 10f)]
    public float fadeTime = 6f;
    [Range(150, 300)]
    public int force = 200;
    [Range(5, 25)]
    public int damage = 10;

    // Reference Variables
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }

	private void Start()
    {
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);

        Destroy(gameObject, fadeTime);
	}

    private void OnCollisionEnter(Collision other)
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