using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;


/// <summary>
/// Класс для управления игроком(кораблём), стрельбой, состоянием игрока и его счёта.
/// </summary>
public class ShipController : MonoBehaviour
{
    /// <summary>
    /// Префаб снаряда, Instantiate которого выполняется при нажатии LMB
    /// </summary>
    [SerializeField] private GameObject BulletPref;
    /// <summary>
    /// Система частиц, воспроизводящаяся при уничтожении объекта этого класса
    /// </summary>
    [SerializeField] private GameObject ParticleSystem;
    /// <summary>
    /// Текст,элемент UI, отображающий текущий счёт игрока
    /// </summary>
    [SerializeField] private GameObject scoreText;
    /// <summary>
    /// Текст,элемент UI, отображающий кнопку для возвоащения в главное меню (загрузка сцены MainMenu)
    /// </summary>
    [SerializeField] private GameObject toMainMenuButton;
    /// <summary>
    /// Массив элементов UI(Image), отображающий текущее здоровье игрока
    /// </summary>
    [SerializeField] private GameObject[] lifeImgaes;
    /// <summary>
    /// Ускорение, передающееся на корабль игрока при нажатии клавиши W
    /// </summary>
    [SerializeField] private float VelocityStrenght;
    /// <summary>
    /// Звук, воспроизводящийся при выстреле
    /// </summary>
    [SerializeField] private AudioSource shoot;
    /// <summary>
    /// Поле, определяющее состояние игрока(Жив/Мёртв)
    /// </summary>
    private bool isAlive;
    
    
    /// <summary>
    /// Поле, определяющее здоровье игрока
    /// </summary>
    private int health;
    /// <summary>
    /// Поле, определяющее счёт игрока
    /// </summary>
    private int score;

    
    /// <summary>
    /// Метод, для изменения визуальной части здоровья игрока
    /// </summary>
    private void HealthBar()
    {
        if(health<3)
            lifeImgaes[health].SetActive(false);
    }

    /// <summary>
    /// Метод, для изменения визуальной части счёта игрока
    /// </summary>
    private void ScoreBar()
    {
        scoreText.GetComponent<Text>().text = score.ToString();
    }
    /// <summary>
    /// Свойство для получения доступа к счёта игрока
    /// </summary>
    public int Score
    {
        get => score;
        set => score = value;
    }
    /// <summary>
    /// Свойство для получения доступа к здоровью игрока
    /// </summary>
    public int Health
    {
        get => health;
        set => health = value;
    }

   
    
    /// <summary>
    /// Метод для проверки состояния isAlive, а также метод для уничтожения корябля игрока и Instantiate системы частиц.
    /// </summary>
    public void checkLive()
    {
        if (health <= 0)
        {
            isAlive = false;
            Instantiate(ParticleSystem, transform.position, transform.rotation);
            HealthBar();
            UnityEngine.Cursor.visible = true;
            toMainMenuButton.SetActive(true);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Метод для поворота корабля игрока  с помощью указателя мыши.
    /// </summary>
    private void RotateByMouse()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    /// <summary>
    /// Метод для передвижения корабля и стрельбы
    /// </summary>
    private void MoveAndShoot()
    {
        if (Input.GetKey(KeyCode.W))
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * VelocityStrenght);

        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
        {
            Instantiate(BulletPref, gameObject.transform.position, gameObject.transform.rotation);
            shoot.Play();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBar();
        HealthBar();
        RotateByMouse();
        MoveAndShoot();
        
    }

    
}
