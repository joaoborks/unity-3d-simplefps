using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameObject flyingPlane;
    public GameObject cutsceneCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player.hasItems.Exists(x => x.Equals("GasCan")))
            {                
                GameObject plane = Instantiate(flyingPlane);
                plane.transform.SetParent(transform.parent);
                FindObjectOfType<GameManager>().SendMessage("RestartGame", 5);
                Destroy(player.gameObject);
                cutsceneCamera.GetComponent<Camera>().enabled = true;
                cutsceneCamera.GetComponent<AudioListener>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
