using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public string pickupMessage;
    public int rotationSpeed = 100;

    private Vector3 downVector;
    private Vector3 upVector;

    private void Awake()
    {
        downVector = transform.position - Vector3.up / 2;
        upVector = transform.position;
        StartCoroutine(MoveDown());
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage(pickupMessage);
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveDown()
    {
        while (transform.position.y > downVector.y + 0.05f)
        {
            transform.Translate(Vector3.down / 2 * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (transform.position.y < upVector.y - 0.05f)
        {
            transform.Translate(Vector3.up / 2 * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(MoveDown());
    }
}
