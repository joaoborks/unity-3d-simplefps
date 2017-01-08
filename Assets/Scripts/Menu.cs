using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    private AudioSource source
    {
        get { return GetComponent<AudioSource>(); }
    }
    private bool startable;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (startable)
        {
            if (Input.GetButtonDown("Fire"))
                StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToControls()
    {
        transform.FindChild("Controls").gameObject.SetActive(true);
        source.Play();
        startable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
