using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class RightNumber : MonoBehaviour
{
    public InputField inputField;
    public GameObject img;
    public Text imgText;
    int target, count;

    [SerializeField]
    Text chanceFor;

    void Awake()
    {
        img = GameObject.Find("Image");
        imgText = img.GetComponentInChildren<Text>();
        imgText.text = "아래에 입력";

        chanceFor = GameObject.Find("ChanceFor").GetComponent<Text>();
    }

    void Start()
    {
        target = UnityEngine.Random.Range(1, 101);
        count = Convert.ToInt32(Math.Log(101, 2));

        Debug.Log("RIGHT NUMBER: " + target);
        LeftChance();
    }

    public void InputEvent() // InputField에 입력 발생 시 작동
    {
        if (Convert.ToInt32(inputField.text) == target) // 정답
        {
            PanelManager.pManager.CloseButtonActive();
            return;
        }
        count--;
        LeftChance();

        if (count <= 0) // 기회 소진
        {
            PanelManager.pManager.SceneReloadButtonActive();
        }
        else // 남은 기회 존재
        {
            if (Convert.ToInt32(inputField.text) < target)
            {
                ReflectText(true);
            }
            else if (Convert.ToInt32(inputField.text) > target)
            {
                ReflectText(false);
            }
        }
    }

    public void ReflectText(bool check) // Image 안 text 값 변경
    {
        if (check == true)
            imgText.text = "Up";
        else if (check == false)
            imgText.text = "Down";
    }

    public void LeftChance()
    {
        chanceFor.text = "남은 기회는 " + count;
    }

}