using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    Animator animator;
    [SerializeField] SavePointLogic savePointLogic;

    private void Start()
    {
       animator = GetComponent<Animator>();
    }
    public void PlayGame()
    {
        animator.SetTrigger("GoToSave");
    }
    public void StartSaveOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        savePointLogic.LoadFromFile("save1.json");
    }
    public void StartSaveTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        savePointLogic.LoadFromFile("save2.json");
    }
    public void StartSaveThree()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        savePointLogic.LoadFromFile("save3.json");
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
    public void ExitStartMeny()
    {

        animator.SetTrigger("GoToStart");
    }
}
