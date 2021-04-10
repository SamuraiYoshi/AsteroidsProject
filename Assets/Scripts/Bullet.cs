using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, определяющий поведения объекта Bullet, т.е. снаряда, которыми стреляет игрок.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Объекта - игрок, для получения позиции Instantiate префаба снаряд.
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// Дистанция между объектом и игроком, на которой объект уничтожается
    /// </summary>
    [SerializeField] private float distancetoDespawn;
    /// <summary>
    /// Стартовое ускорение снаряда
    /// </summary>
    [SerializeField] private float bulletrVelocity;
    
    /// <summary>
    /// Метод, уничтожающий объекты при достижении заданной дистанции между игроком и объектом этого класса.
    /// </summary>
    private void DeSpawn()
    {
        if(Vector2.Distance(player.transform.position, gameObject.transform.position) >distancetoDespawn)
            Destroy(gameObject);
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletrVelocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            DeSpawn();
        }

    }
}
