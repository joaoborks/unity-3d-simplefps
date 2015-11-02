using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour
{
    // Customizeable Variables
    [Range(1f, 5f)]
    public float fadeTime = 3f;
    [Range(20, 120)]
    public int force = 60;
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
        if (other.collider.tag == "Player")
        {
            other.collider.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}