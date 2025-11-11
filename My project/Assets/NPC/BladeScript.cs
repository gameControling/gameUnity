using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private GameObject blueMag;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite dialogImage;
    [SerializeField] private GameObject image;
    private string[] message;
    float timerOnDown;
    float timeDown = 1;
    float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        message = new string[1];
        message[0] = "Вы заполучили легендарный меч!";
    }

    // Update is called once per frame
    void Update()
    {
        if ((timerOnDown += Time.deltaTime) <= timeDown)
        {
            rb.transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            speed = -speed;
            timerOnDown = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panel.SetActive(true);
            blueMag.transform.position = new Vector3(218f, -52.2f);
            blueMag.GetComponent<DialogsMag>().maxDialog = 17;
            blueMag.GetComponent<DialogsMag>().numDialog = 12;
            textDialog.text = message[0];
            image.GetComponent<Image>().sprite = dialogImage;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(this != null)
        {
            panel.SetActive(false);
            Destroy(gameObject);
        }
        
    }
}
