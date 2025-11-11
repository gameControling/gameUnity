using Unity.VisualScripting;
using UnityEngine;

public class AmbushScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;
    [SerializeField] GameObject[] gates;
    [SerializeField] float quantityEnemy;
    [SerializeField] float[] positionGatesX;
    [SerializeField] float[] positionGatesY;
    [SerializeField] float[] posSpavnEnemyX;
    [SerializeField] float posSpavnEnemyY;
    bool startSpavn = false;
    float timerSpavnWaves = 30;
    float quantityWaves;
    float timerDie = 10;

    void Start()
    {
        gates[0].transform.position = new Vector3(positionGatesX[0], positionGatesY[0]);
        gates[1].transform.position = new Vector3(positionGatesX[1], positionGatesY[0]);
    }

    private void Update()
    {
        if (startSpavn) 
        {
            if (quantityWaves > 0)
            {
                if (timerSpavnWaves < 0)
                {
                    for (int i = 0; i < quantityEnemy; i++)
                    {
                        Instantiate(gates[0], new Vector3(Random.Range(posSpavnEnemyX[0], posSpavnEnemyX[1]), posSpavnEnemyY), transform.rotation);
                    }
                    quantityEnemy += 1;
                }
                else
                {
                    timerSpavnWaves -= Time.deltaTime;
                }
            }
            else
            {
                if(timerDie > 0)
                {
                    gates[0].transform.position = new Vector3(gates[0].transform.position.x, gates[0].transform.position.y + 0.2f);
                    gates[1].transform.position = new Vector3(gates[0].transform.position.x, gates[0].transform.position.y + 0.2f);
                    timerDie -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<move_player>())
        {
            gates[0].transform.position = new Vector3(positionGatesX[0], positionGatesY[1]);
            gates[1].transform.position = new Vector3(positionGatesX[1], positionGatesY[1]);
            startSpavn = true;
        }
    }
}
