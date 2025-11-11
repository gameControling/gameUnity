using System;
using UnityEngine;
using Unity.Mathematics;
using System.IO;

public class SavePointLogic : MonoBehaviour
{
    struct SaveClass
    {
        public float HP;
        public float positionX;
        public float positionY;
        public int dmg;
        public float speed;
        public float deshPower;
        public float attacRange;
    }
    Animator animator;
    GameObject player;
    Rigidbody2D rb;
    static SaveClass saveClass;
    static string filePath = "save1.json";
    static bool fileOpen = false;
    public bool activeUpdate = true;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeUpdate)
        {
            if (math.abs(player.transform.position.x - rb.position.x) < 1f &&
                    math.abs(player.transform.position.y - rb.position.y) < 5f)
            {
                Activate();
                saveClass = new SaveClass
                {
                    HP = player.GetComponent<move_player>().health,
                    positionX = rb.position.x,
                    positionY = rb.position.y,
                    dmg = player.GetComponent<move_player>().damage,
                    speed = player.GetComponent<move_player>().maxSpeed,
                    deshPower = player.GetComponent<move_player>().deshPover,
                    attacRange = player.GetComponent<move_player>().attackRange
                };
                string json = JsonUtility.ToJson(saveClass, true);

                try
                {
                    File.WriteAllText(filePath, json);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }

    public void LoadFromFile(string FilePath)
    {
        filePath = FilePath;
        Debug.Log(filePath);
        fileOpen = true;
        if (!File.Exists(filePath))
        {
            Debug.Log("Файла сохранения не существует");
            saveClass.speed = 20f;
            saveClass.dmg = 20;
            saveClass.positionX = 34.2f;
            saveClass.positionY = 2.4f;
            saveClass.HP = 100;
            saveClass.attacRange = 5;
            saveClass.deshPower = 75;
        }
        else
        {
            string json = File.ReadAllText(filePath);
            saveClass = JsonUtility.FromJson<SaveClass>(json);
        }
    }

    public float getSpeed() { return saveClass.speed; }

    public float getHp() { return saveClass.HP; }

    public float getPositionX() { return saveClass.positionX; }

    public float getPositionY() { return saveClass.positionY; }

    public int getDmg() { return saveClass.dmg; }

    public float getDeshPower() { return saveClass.deshPower; }

    public float getAttackRange() { return saveClass.attacRange; }

    public bool getOpenFile() { return fileOpen; }

    public void Activate()
    {
        animator.SetTrigger("Activate");
    }
}
