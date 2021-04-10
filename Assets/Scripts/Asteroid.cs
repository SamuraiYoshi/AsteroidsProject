using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Главный класс уничтожающихся объектов, содержащий все необходимые методы и поля
/// </summary>
public class Asteroid : MonoBehaviour
{
    /// <summary>
    /// Звук, воспроизводящийся при разрушении объекта (Destroy)
    /// </summary>
    public AudioSource crash;
    /// <summary>
    /// Система частиц, вопсроизводящаяся при разрушении объекта
    /// </summary>
    public GameObject ParticleSystem; 
    /// <summary>
    /// Префаб объекта, создающийся при разрушении объекта AsteroidBig
    /// </summary>
    public GameObject MediumAsteroid;
    /// <summary>
    /// Префаб объекта, создающийся при разрушении объекта AsteroidMedium
    /// </summary>
    public GameObject SmallAsteroid;
    /// <summary>
    /// Вес объекта, или score points, начисляющийся при уничтожении игроком
    /// </summary>
    public int value;
    /// <summary>
    /// Дистанция между объектом и игроком, на которой объект уничтожается
    /// </summary>
    public float despawnDistance;
    /// <summary>
    /// Объект - Игрок, для расчёта дистанции
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Класс, контролирующий поддержания заданного количества объектов на сцене
    /// </summary>
    public ObejctSpawner spawner;


    /// <summary>
    /// Метод, обрабатывающий уничтожение объекта. Используется в случае Instantiate других объектов при своём уничтожении
    /// </summary>
    public void DamageTaken(Collider2D other,GameObject Main, GameObject Sub)
    {
        if (other.gameObject.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<ShipController>().Health -= 1;
                other.gameObject.GetComponent<ShipController>().checkLive();
                other.gameObject.GetComponent<ShipController>().Score += value;
            }
            else if (other.gameObject.CompareTag("Bullet"))
            {
                player.gameObject.GetComponent<ShipController>().Score += value;
                Destroy(other.gameObject);
            }
            Instantiate(ParticleSystem, transform.position, transform.rotation);
            Instantiate(Sub, transform.position, transform.rotation);
            Instantiate(Sub, transform.position, transform.rotation);
            spawner.AmountOfSpawned[GetIndexInSpawner(Main)] -= 1;
            spawner.AmountOfSpawned[GetIndexInSpawner(Sub)] += 2;
            crash.Play();
            Destroy(Main);
        }
    }
    
    /// <summary>
    /// Метод, обрабатывающий уничтожение объекта. Используется в случае не Instantiate других объектов при своём уничтожении
    /// </summary>
    public void DamageTaken(Collider2D other,GameObject Main)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(ParticleSystem, transform.position, transform.rotation);
            other.gameObject.GetComponent<ShipController>().Health -= 1;
            other.gameObject.GetComponent<ShipController>().checkLive();
            other.gameObject.GetComponent<ShipController>().Score += value;
            spawner.AmountOfSpawned[GetIndexInSpawner(Main)]-=1;
            crash.Play();
            Destroy(Main);
            
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(ParticleSystem, transform.position, transform.rotation);
            player.gameObject.GetComponent<ShipController>().Score += value;
            Destroy(other.gameObject);
            spawner.AmountOfSpawned[GetIndexInSpawner(Main)]-=1;
            Debug.Log(GetIndexInSpawner(Main));
            crash.Play();
            Destroy(Main);
        }
    }
    
    /// <summary>
    /// Метод, возвращающий индекс префаба из массива префабов для спавна, определённого в классе ObjectSpawner
    /// </summary>
    public int GetIndexInSpawner(GameObject gameObject)
    {
        int index = 0;
        for (int i = 0; i < spawner.SpawnPrefab.Length-1; i++)
        {
            if (gameObject.CompareTag(spawner.SpawnPrefab[i].tag))
            {
                index = i;
            }
        }
        return index;

    }
    /// <summary>
    /// Метод, уничтожающий объекты при достижении заданной дистанции между игроком и объектом этого класса.
    /// </summary>
    public void DeSpawn(int IndexInSpawner)
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) > despawnDistance)
        {
            spawner.AmountOfSpawned[IndexInSpawner]--;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Метод, инициализирующий поля spawner,player, а также стартовый вектор ускорения объекта
    /// </summary>
    public void StartInitialisation()
    {
        spawner = GameObject.FindWithTag("Pooler").GetComponent<ObejctSpawner>();
        player = GameObject.FindWithTag("Player");
        float StartingVelocity = Random.Range(0.1f, 0.2f);
        Vector2 AsteroidStartTransform = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        gameObject.GetComponent<Rigidbody2D>().velocity = AsteroidStartTransform * StartingVelocity;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartInitialisation();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            var tmp = GetIndexInSpawner(gameObject);
            DeSpawn(tmp);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageTaken(other, gameObject, MediumAsteroid);
    }

    
}
