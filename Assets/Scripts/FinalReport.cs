using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalReport : MonoBehaviour
{
    private string startgame;
    public GameObject outputGameObject;
    private bool success;
    public GameObject failed;
    public GameObject succeeded;

    private void Start()
    {
        success = true;
        startgame = "MainScene";
        TextMeshProUGUI output = outputGameObject.GetComponent<TextMeshProUGUI>(); 
        string text = "Covido Report:- \n";
        text += "Task 1 \n";
        text += checkLaptopTask();
        text += "Task 2 \n";
        text += checkEntertainment();
        text += "Task 3 \n";
        text += checkPrinting();
        text += "Extras \n";
        text += checkOpeningScene();
        text += checkSanitizing();
        text += checkSocialDistance();
        if (success)
        {
            text += "\nWell done! You succeeded in preventing COVIDO from spreading!";
            succeeded.SetActive(true);
            AudioManager.instance.Play("succSFX");
        }
        else
        {
            text += "\nYou failed, you helped spread COVIDO. \n";
            text += "Many of your colleagues got infected & the company is closing. \n";
            text += "The CEO is angry & requested a meeting with you!";
            failed.SetActive(true);
            AudioManager.instance.Play("failedSFX");
        }
        output.text = text;
    }

    string checkOpeningScene(){
        string text = "";
        if(GameManager.instance.OpeningSceneEvent == 0){
            text += "- Interaction Name: Meeting a colleague \n" +
                "-- Interaction Type: Wave - Safety Measurement Result: Success\n";
        }
        else if(GameManager.instance.OpeningSceneEvent == 1){
            success = false;
            text += "- Interaction Name: Meeting a colleague \n" +
                "-- Interaction Type: Hug - Safety Measurement Result: Failure \n";
        }
        else if(GameManager.instance.OpeningSceneEvent == 2){
            success = false;
            text += "- Interaction Name: Meeting a colleague \n" +
                "-- Interaction Type: Shake Hands - Safety Measurement Result: Failure \n";
        }
        else{
            success = false;
            text += "- Interaction Name: Meeting a colleague \n" +
                "-- Interaction Did Not Take Place \n";
        }
        return text;
    }
    string checkSanitizing(){
        string text = "";
        if(GameManager.instance.SanitizingEvent == 0){
            success = false;
            text += "- Interaction Name: Sanitization \n" +
                "-- Interaction Type: Did Not Sanitize - Safety Measurement Result: Failure \n";
        }
        else if(GameManager.instance.SanitizingEvent == 1){
            text += "- Interaction Name: Sanitization \n" +
                " -- Interaction Type: Did Sanitize - Safety Measurement Result: Success \n";
        }
        else{
            success = false;
            text += "- Interaction Name: Sanitization \n" +
                "-- Interaction Did Not Take Place \n";
        }
        return text;
    }
    string checkEntertainment(){
        string text = "";
        if (GameManager.instance.EntertainmentEvent == 0){
            success = false;
            text += "- Interaction Name: Entertainment \n" +
                " -- Interaction Type: Choosing Full Sofa - Safety Measurement Result: Failure \n";
        }
        else if(GameManager.instance.EntertainmentEvent == 1){
            text += "- Interaction Name: Entertainment \n" +
                "-- Interaction Type: Choosing Empty Sofa - Safety Measurement Result: Success \n";
        }
        else{
            success = false;
            text += "- Interaction Name: Entertainment \n" +
                "-- Interaction Did Not Take Place \n";
        }
        return text;
    }
    string checkSocialDistance(){
        string text = "";
        if (GameManager.instance.SocialDistanceCounter == 0){
            text += "- Interaction Name: Social Distancing With Others \n" +
                "-- Safety Measurement Result: Success \n";
        }
        else{
            success = false;
            text += "- Interaction Name: Social Distancing With Others \n" +
                "-- Safety Measurement Result: Failure, had a Close Contact With People For " +GameManager.instance.SocialDistanceCounter+" Times \n";
        }
        return text;
    }
    string checkPrinting(){
        string text = "";
        if (GameManager.instance.PrintingEvent){
            text += "- Interaction Name: Printing \n" +
                "-- Interaction Type: Did Print \n";
        }
        else{
            text += "- Interaction Name: Printing \n" +
                "-- Interaction Type: Did Not Print \n";
        }
        return text;
    }

    string checkLaptopTask(){
        string text = "";
        if (GameManager.instance.LaptopTask){
            text += "- Interaction Name: Using Laptop \n" +
                "-- Interaction Type: Did Use Laptop \n";
        }
        else{
            text += "- Interaction Name: Using Laptop \n" +
                "-- Interaction Type: Did Not Use Laptop \n";
        }
        return text;
    }
    public void StartGame(){
        SceneManager.LoadScene(startgame);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
