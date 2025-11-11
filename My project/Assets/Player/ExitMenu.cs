using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] GameObject exitMenu;
    public bool menuOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Esc");
            if (!menuOpen)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    { 
        Debug.Log("OpenMenu");
        exitMenu.SetActive(true);
        menuOpen = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Debug.Log("CloseMenu");
        
        menuOpen = false;
        exitMenu.SetActive(false);
    }

    public void ExidToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
