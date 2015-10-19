using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("UpdateSpawnpoint", transform);
            Destroy(this);
        }
    }
}
