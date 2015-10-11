using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    // Customizeable Variables
    public GameObject[] itemPrefabs = new GameObject[3];

    // Object Variables
    private Transform[] items = new Transform[3];
    private string[] itemNames = new string[3];
    private bool[] itemPlaced = new bool[3];

    private void Awake()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = transform.FindChild("Item " + (i + 1));
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
            GameObject item = Instantiate(itemPrefabs[number], items[number].position, Quaternion.identity) as GameObject;
            item.transform.SetParent(items[number]);
            itemPlaced[number] = true;
        }
    }
}
