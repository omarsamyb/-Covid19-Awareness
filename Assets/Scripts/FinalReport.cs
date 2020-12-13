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
 public Text openingScene;
 public Text sanitizingScene;
 public Text entertainmentScene;
 public Text socialDistance;
 public Text printingTask;
 public Text laptopTask;



    void Update()
    {  
        checkOpeningScene();
        checkSanitizing();
        checkEntertainment();
        checkPrinting();
        checkLaptopTask();
        checkSocialDistance();
    }
    void checkOpeningScene(){
        if(GameManager.instance.OpeningSceneEvent == 0){
            openingScene.text = "- Interaction Name: Meeting a colleague --- Interaction Type: Wave --- Safety Measurement Result: Success";
        }
        else if(GameManager.instance.OpeningSceneEvent == 1){
            openingScene.text = "- Interaction Name: Meeting a colleague --- Interaction Type: Hug --- Safety Measurement Result: Failure";
        }
        else if(GameManager.instance.OpeningSceneEvent == 2){
            openingScene.text = "- Interaction Name: Meeting a colleague --- Interaction Type: Shake Hands --- Safety Measurement Result: Failure";
        }
        else{
            openingScene.text = "- Interaction Name: Meeting a colleague --- Interaction Did Not Take Place";
        }
    }
    void checkSanitizing(){   
        if(GameManager.instance.SanitizingEvent == 0){
            sanitizingScene.text = "- Interaction Name: Sanitization --- Interaction Type: Did Not Sanitize --- Safety Measurement Result: Failure";
        }
        else if(GameManager.instance.SanitizingEvent == 1){
            sanitizingScene.text = "- Interaction Name: Sanitization --- Interaction Type: Did Sanitize --- Safety Measurement Result: Success";
        }
        else{
            sanitizingScene.text = "- Interaction Name: Sanitization --- Interaction Did Not Take Place";
        }
    }
    void checkEntertainment(){   
        if(GameManager.instance.EntertainmentEvent == 0){
            entertainmentScene.text = "- Interaction Name: Entertainment --- Interaction Type: Choosing Full Sofa --- Safety Measurement Result: Failure";
        }
        else if(GameManager.instance.EntertainmentEvent == 1){
            entertainmentScene.text = "- Interaction Name: Entertainment --- Interaction Type: Choosing Empty Sofa --- Safety Measurement Result: Success";
        }
        else{
            entertainmentScene.text = "- Interaction Name: Entertainment --- Interaction Did Not Take Place";
        }
    }
    void checkSocialDistance(){
        if(GameManager.instance.SocialDistanceCounter == 0){
            socialDistance.text = "- Interaction Name: Social Distancing With Others --- Safety Measurement Result: Success";
        }
        else{
            socialDistance.text = "- Interaction Name: Social Distancing With Others --- Safety Measurement Result: Failure, Had A Close Contact With People For "+GameManager.instance.SocialDistanceCounter+" Times";
        }
    }
    void checkPrinting(){   
        if(GameManager.instance.PrintingEvent){
            printingTask.text = "- Interaction Name: Printing --- Interaction Type: Did Print";
        }
        else{
            printingTask.text = "- Interaction Name: Printing --- Interaction Type: Did Not Print";
        }
    }   

    void checkLaptopTask(){
        if(GameManager.instance.LaptopTask){
            laptopTask.text = "- Interaction Name: Using Laptop --- Interaction Type: Did Use Laptop";
        }
        else{
            laptopTask.text = "- Interaction Name: Using Laptop --- Interaction Type: Did Not Use Laptop";
        }
    }
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
