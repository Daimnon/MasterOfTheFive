using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    // "title menu"
    public void MoveToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // "main menu"
    public void MoveToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void MoveToSetting()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(?);
    }

    public void MoveToGameMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void MoveToCreateRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void MoveToJoinRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void MoveToRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void MoveToDuel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void MoveToDuel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }
}
