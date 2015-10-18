using UnityEngine;

public class Machine : MonoBehaviour
{
    // Customizeable Variables
    public GameObject[] itemPrefabs = new GameObject[4];
    public GameObject[] doors = new GameObject[2];
    public Animator planeHolder;

    // Object Variables
    private Transform[] items = new Transform[4];
    private string[] itemNames = new string[3];
    private bool[] itemPlaced = new bool[3];
    private int activated = 0;

    private void Awake()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = transform.FindChild("Item " + (i + 1));
            if (i < itemNames.Length)
                itemNames[i] = itemPrefabs[i].name;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            for (int i = 0; i < itemNames.Length; i++)
            {
                if (player.hasItems.Contains(itemNames[i]))
                    PlaceItem(i);
            }
        }
    }

    private void PlaceItem(int number)
    {
        if (!itemPlaced[number])
        {
            GameObject item = Instantiate(itemPrefabs[number], items[activated].position, items[activated].rotation) as GameObject;
            item.transform.SetParent(items[activated++]);
            itemPlaced[number] = true;
            if (number < doors.Length)
                doors[number].SendMessage("ToggleDoor", true, SendMessageOptions.DontRequireReceiver);
            planeHolder.SetInteger("actionNumber", activated);
            if (activated == 3)
                Instantiate(itemPrefabs[activated], items[activated].position, Quaternion.identity);
        }
    }
}
