using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogsMag : SoundsScript
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private Sprite dialogImage;
    [SerializeField] private GameObject image;
    private string[] message;
    [SerializeField] private string colorMag;
    private bool startDialog = false;
    public int numDialog = 0;
    public int maxDialog = 0;
    ControlAnimationNPC npc;
    float timer = 0.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npc = GetComponent<ControlAnimationNPC>();
        message = new string[21];
        //Красный маг
        message[0] = "Спасибо, что избавл меня от этих монстров!";
        message[1] = "Я Красный маг и мой долг найти героя, который освободит подземный город от Большого слзня.";
        message[2] = "Ты выглядишь достаточно сильным что-бы сразить его.";
        message[3] = "Направляйтся даше по лесу, там ты увидишь склон.";
        message[4] = "Спустись по нему и внизу тебя встретит мой брат Синий маг.";
        message[5] = "Он поможет тебе добраться до входа в подземный город.";
        message[6] = "Удачи!";
        //Синий маг (первая встреча)
        message[7] = "О, приветсвую герой, мой брат уже рассказал мне о тебе!";
        message[8] = "Я Синий маг и готов тебе помочь, но для начала ты должен добыть легендарный меч!";
        message[9] = "Он находится глубоко в этой пещере.";
        message[10] = "Если сможешь его получить, то я готов открыть тебе проход в пригородные тунели.";
        message[11] = "Найдешь меня возле входа в город!";
        //Синий маг (после добычи меча)
        message[12] = "Герой, я ждал тебя!";
        message[13] = "Вижу ты получил меч, значит ты достоин войти в тунели.";
        message[14] = "(колдует, открывая вход в тунели)";
        message[15] = "Можешь идти герой, ворота открыты!";
        message[16] = "В городских стоках находится мой брат Зеленый маг.";
        message[17] = "Найди его, он скажет тебе что делать дальше.";
        //Зеленый маг
        message[18] = "Привет герой, я Зеленый маг!";
        message[19] = "Перед тобой находится ключ от главных врат города!";
        message[20] = "Возьми его и срази Большого слизня!";

        if (colorMag == "red")
        {
            numDialog = 0;
            maxDialog = 6;
        }
        else if (colorMag == "blue")
        {
            numDialog = 7;
            maxDialog = 11;
        }
        else if (colorMag == "green")
        {
            numDialog = 18;
            maxDialog = 20;
        }

        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colorMag != "red")
        {
            if (collision.tag == "Player")
            {
                panel.SetActive(true);
                textDialog.text = message[numDialog];
                startDialog = true;
            }
        }
        else
        {
            if(npc.nearEnemy)
            {
                if (collision.tag == "Player")
                {
                    panel.SetActive(true);
                    textDialog.text = message[numDialog];
                    startDialog = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(this != null)
        {
            panel.SetActive(false);
            startDialog = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startDialog)
        {
            image.GetComponent<Image>().sprite = dialogImage;
            if(timer <= 0)
            {
                PlaySound(0, volume: 0.25f, p1: 1, p2: 1);
            }else
            {
                timer -= Time.deltaTime; 
            }
            if (Input.GetKeyUp(KeyCode.Q) && numDialog < maxDialog)
            {
                textDialog.text = message[numDialog += 1];
            }
            else if(numDialog == maxDialog)
            {
                panel.SetActive(false);
                startDialog = false;
            }
        }

    }
}
