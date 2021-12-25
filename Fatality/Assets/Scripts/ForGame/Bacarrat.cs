using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bacarrat : MonoBehaviour
{
    int result; // 자신의 값이 높을 때 High 1 , 자신의 값이 낮을 때 Low 0
    int target; // 랜덤으로 정해지는 상대방의 값
    public GameObject myPanel, yourPanel;
    public Text myText, yourText;

    int isClear;

    private void Awake()
    {
        isClear= 2;

        // 숫자 값을 나타내는 패널 assign
        myPanel = GameObject.Find("MyPanel");
        myText = myPanel.GetComponentInChildren<Text>();
        yourPanel = GameObject.Find("YourPanel");
        yourText = yourPanel.GetComponentInChildren<Text>();
    }
    void Start()
    {
        int me = Random.Range(1, 11); // 플레이어 값
        do {
            target = Random.Range(1, 11); // 상대방의 값
        } while(me == target);
        ReflectPanel(me, myText);
        result = (target > me) ? 0 : 1 ; // 플레이어 값이 높으면 0 상대 값이 높으면 1

        Debug.Log("BACARRET NPC:" + target);
    }

    public void OnclickBtn() // 버튼 클릭 시 작동하는 함수 - 결과 출력
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        
        if(btnName == "LowButton")
            if (result == 0)
            {
                PanelManager.pManager.CloseButtonActive();
                isClear = 1;
            }
            else
            {
                isClear = 0;
            }
        else if (btnName == "HighButton")
            if (result == 1)
            {
                PanelManager.pManager.CloseButtonActive();
                isClear = 1;
            }
            else
            {
                isClear = 0;
            }
        ReflectPanel(target, yourText); // 상대방 점수 공개
        PanelManager.pManager.ActiveButtonDecide(isClear);
    }
    
    public void ReflectPanel(int num, Text txt) // 패널의 숫자 값 변경하는 함수
    {
       txt.text = num.ToString();
    }
}
