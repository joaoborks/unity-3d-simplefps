using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    // Customizeable Variables
    [Range(5000, 20000)]
    public int force = 10000;
    [Range(10, 200)]
    public int range = 50;

    // Reference Variables
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }

    // Object Variables
    Vector3 initPosition;

    private void Start()
    {
        initPosition = transform.position;
        Camera cam = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 position;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, LayerMask.NameToLayer("Hit")))
            position = hit.point;
        else
        {
            position = new Vector3(mousePos.x, mousePos.y, range);
            position = cam.ScreenToWorldPoint(position);
        }

        transform.LookAt(position);

        rb.velocity = transform.forward * Time.deltaTime * force;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, initPosition) > range)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        print("Shot hit");
        other.gameObject.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}