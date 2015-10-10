using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private void Awake ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}