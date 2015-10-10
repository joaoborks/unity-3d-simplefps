using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Turret : MonoBehaviour
{
    private enum State
    {
        Default,
        Engaged,
        Search
    }

    // Custom Variables
    [Range(100, 250)]
    public int rotationSpeed = 125;
    [Range(0.5f, 5f)]
    public float engagedSpeed = 2.5f;
    [Range(0.1f, 1.5f)]
    public float shootCooldown = 1f;
    public GameObject turretBullet;

    // Object Variables
    private State state;
    private Transform tBase
    {
        get { return transform.GetChild(0); }
    }
    private Transform tWeapon
    {
        get { return tBase.GetChild(0); }
    }
    private Transform tOrigin
    {
        get { return tWeapon.GetChild(0); }
    }
    private float sightRadius
    {
        get { return transform.FindChild("Collider").GetComponent<SphereCollider>().radius; }
    }

    private void Start()
    {
        StartCoroutine(DefaultUpdate());
    }

    private void OnTriggerStay(Collider other)
    {
        if (state == State.Default && other.tag == "Player")
        {
            Vector3 dir = other.transform.position - tBase.position;
            float angle = Vector3.Angle(dir, tBase.forward);
            if (angle < 55)
            {
                RaycastHit hit;
                Physics.Raycast(tBase.transform.position, dir.normalized, out hit, sightRadius);
                if (hit.collider.tag == "Player")
                    StartCoroutine(EngagedUpdate(hit.transform));
            }
        }
    }

    /// <summary>
    /// Continuously rotates the turret to search for targets
    /// </summary>
    private IEnumerator DefaultUpdate()
    {
        state = State.Default;
        while (state == State.Default)
        {
            tBase.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// Rotates the turret aiming for the target
    /// </summary>
    /// <param name="target">The target to rotate to</param>
    private IEnumerator EngagedUpdate(Transform target)
    {
        state = State.Engaged;
        StartCoroutine(EngagedShooting());
        Vector3 dir;
        while (state == State.Engaged)
        {
            dir = target.position - tBase.position;
            tBase.rotation = Quaternion.Lerp(tBase.rotation, Quaternion.LookRotation(dir), Time.deltaTime * engagedSpeed);
            tWeapon.LookAt(target);
            tWeapon.localEulerAngles = new Vector3(Mathf.Clamp(tWeapon.eulerAngles.x, -30, 30), 0, 0);
            yield return null;
        }
    }

    private IEnumerator EngagedShooting()
    {
        while (state == State.Engaged)
        {
            Instantiate(turretBullet, tOrigin.position, tOrigin.rotation);
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
