using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Класс, контролирующий поддержания заданного количества объектов на сцене
/// </summary>
public class ObejctSpawner : MonoBehaviour
{
    /// <summary>
    /// Массив префабов для Instantiate
    /// </summary>
     public GameObject[] SpawnPrefab;
    /// <summary>
    /// Массив, содержащий в себе количество объектов, которое должно быть на сцене для отдельного  префаба
    /// </summary>
     [SerializeField] private int[] AmountForSpawn;
    /// <summary>
    /// Массив, содержащий в себе количество объектов, содержится на сцене ля отдельного  префаба
    /// </summary>
     public int[] AmountOfSpawned;
    /// <summary>
    /// Объект игрок, для расчёта позиции Instantiate объектов
    /// </summary>
     [SerializeField] private GameObject Player;



    /// <summary>
    /// Метод, поддерживающий необходимой количество объектов на сцене
    /// </summary>
    public void Spawn()
    {
        for (int i = 0; i < SpawnPrefab.Length; i++)
        {
            if (AmountOfSpawned[i] < AmountForSpawn[i])
            {
                Vector2 tmp = new Vector2(
                    Random.Range(Player.transform.position.x - 8f, Player.transform.position.x + 8f),
                    Random.Range(Player.transform.position.y - 8f, Player.transform.position.y + 8f));
                Instantiate(SpawnPrefab[i], tmp, transform.rotation);
                AmountOfSpawned[i]++;
            }
        }
    }

  

    
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
            Cursor.visible = false;
        AmountOfSpawned = new int[SpawnPrefab.Length];
        for (int i = 0; i < AmountOfSpawned.Length-1; i++)
            AmountOfSpawned[i] = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
}
