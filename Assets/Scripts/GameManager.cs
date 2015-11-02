using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RestartGame(float time)
    {
        StartCoroutine(RestartTimer(time));
    }

    private IEnumerator RestartTimer(float time)
    {
        yield return new WaitForSeconds(time);

        Application.LoadLevel(0);
    }
}