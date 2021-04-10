using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

/// <summary>
/// Наследник класса Asteroid. Определяет поведение вражеского корабля. (Методом MoveTowars класса Vector2 перемещаем объект этого класса в позицию игрока - player)
/// </summary>
public class EnemyShip : Asteroid
{
    // Start is called before the first frame update
    void Awake()
    {
        spawner = GameObject.FindWithTag("Pooler").GetComponent<ObejctSpawner>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null && player!=null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 0.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageTaken(other, gameObject);
    }
}
