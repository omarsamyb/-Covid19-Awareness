using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoint : MonoBehaviour
{
    public Transform player;
    public Image img;
    public Image img2;
    public Text distance;
    public Text distance2;
    private Transform target;
    private Transform target2;

    // Update is called once per frame
    void Update()
    {
        target = OutcomeManager.instance.currentObjective;
        if (OutcomeManager.instance.currentObjective2 != null)
            target2 = OutcomeManager.instance.currentObjective2;
        else
        {
            target2 = null;
            img2.gameObject.SetActive(false);
        }
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0){
            if (pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        img.transform.position = pos;
        distance.text = ((int)Vector3.Distance(target.position, player.position)).ToString() + "m";
        if(target2 != null)
        {
            img2.gameObject.SetActive(true);
            pos = Camera.main.WorldToScreenPoint(target2.position);
            if (Vector3.Dot((target2.position - transform.position), transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                    pos.x = maxX;
                else
                    pos.x = minX;
            }
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            img2.transform.position = pos;
            distance2.text = ((int)Vector3.Distance(target2.position, player.position)).ToString() + "m";
        }
    }
}
