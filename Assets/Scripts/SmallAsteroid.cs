using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Наследник класса Asteroid ( эти объекты создаются при уничтожении объекта класса Asteroid)
/// </summary>
public class SmallAsteroid : Asteroid
{
    void Update()
    {
        if (gameObject != null)
        {
            var tmp = GetIndexInSpawner(gameObject);
            DeSpawn(tmp);
            gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartInitialisation();
    }

    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageTaken(other,gameObject,SmallAsteroid);
    }
}
