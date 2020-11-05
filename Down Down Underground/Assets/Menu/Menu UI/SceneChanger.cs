using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Player 1");
    }

    public void Scene2()
    {
        SceneManager.LoadScene("Player 2");
    }

    public void Scene3()
    {
        SceneManager.LoadScene("Player 3");
    }

    public void Scene4()
    {
        SceneManager.LoadScene("Player 4");
    }

    public void Scene5()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Scene6()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Scene7()
    {
        SceneManager.LoadScene("Credits");
    }
}