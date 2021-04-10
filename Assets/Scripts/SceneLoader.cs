using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для загрузки сцен и заверешния приложения
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Метод, загружающий главное меню
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Метод, загружающий игровую сцену
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    /// <summary>
    /// Метод, завершающий приложение
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
