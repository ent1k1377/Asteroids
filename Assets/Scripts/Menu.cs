using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Main_GamePlay");
    }

    public void Proceed()
    {
        print("proceed");
    }

    public void ChangeControl()
    {
        print("Change of control");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
