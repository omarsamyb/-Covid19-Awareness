using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinalReport : MonoBehaviour
{
 public GameObject creditsMenu;
 public GameObject reportMenu;
 public string startgame;

    public void StartGame(){
        SceneManager.LoadScene(startgame);
    }
    public void Credits(){
        reportMenu.SetActive(false);
        creditsMenu.SetActive(true);
        
    }
    public void QuitGame(){
        Application.Quit();
    }

}
