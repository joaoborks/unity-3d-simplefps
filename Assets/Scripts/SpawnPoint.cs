using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public GameObject activateOnSpawn;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("UpdateSpawnpoint", transform);
            if (activateOnSpawn != null)
                activateOnSpawn.SetActive(true);
            Destroy(this);
        }
    }
}
