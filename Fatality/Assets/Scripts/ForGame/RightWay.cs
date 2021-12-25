using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightWay : MonoBehaviour
{
    int value, target;
    void Start()
    {
        target =  Random.Range(0,2); // 왼_0, 오_1
        Debug.Log("RIGHT WAY: " + target);
    }
    public void AssignValue()
    {      
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        if(btnName == "LeftButton")
            value = 0;
        else if (btnName == "RightButton")
            value = 1;
        
        if(value == target)
        {
            PanelManager.pManager.CloseButtonActive();
        }
        else
        {
            PanelManager.pManager.SceneReloadButtonActive();
        }
    }

}
