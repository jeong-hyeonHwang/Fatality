using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* 필요 요소들
 * <Canvas 위의 요소들>
 * RSP의 Condition을 띄울 Text
 * NPC의 RSP 값을 띄울 Text
 * Player의 RSP 값을 띄울 Text
 * 플레이어가 선택할 버튼들(Rock, Scissor, Paper)
 */
public class RSP : MonoBehaviour
{
    int conditionInt;
    string condition; //WIN: 0, LOSE: 1
    int npc;
    int player;
    string[] rspList = {"묵", "찌", "빠"};

    Text conditionText; // Condition에 대한 설명
    Text npcRSP; // NPC가 낸 RSP 표시
    Text playerRSP; // Player가 선택한 RSP 표시

    private void Awake()
    {
        conditionText = GameObject.Find("ConditionText").GetComponent<Text>();
        npcRSP = GameObject.Find("NPC_RSP").GetComponent<Text>();
        playerRSP = GameObject.Find("Player_RSP").GetComponent<Text>();

        npc = Random.Range(0, 3); // Rock: 0, Scissor: 1, Paper: 2

        Debug.Log("RSP: " + npc);

        string conditionStr = ConditionChoose();
        conditionText.text = "당신은 '" + conditionStr + "' 합니다!";

    }

    public void YouChoose()
    {
        player = EventSystem.current.currentSelectedGameObject.GetComponent<RSP_Number>().number;
        npcRSP.text = rspList[npc];
        playerRSP.text = rspList[player];

        switch(ConditionSatisfied())
        {
            case true:
                PanelManager.pManager.CloseButtonActive();
                break;
            case false:
                PanelManager.pManager.SceneReloadButtonActive();
                break;
        }

        conditionText.text = "결과";
    }

    public string ConditionChoose() // 통과 조건 WIN, LOSE, SAME 중에서 선택
    {
        conditionInt = Random.Range(0, 3);
        switch(conditionInt)
        {
            case 0:
                condition = "이겨야";
                break;
            case 1:
                condition = "져야";
                break;
            case 2:
                condition = "비겨야";
                break;
        }
        return condition;
    }

    public bool ConditionSatisfied() // NPC - Player의 관계가 Condition을 만족했는지 체크합니다.
    {
        bool satisfied = false;
        switch(conditionInt)
        {
            case 0: // NEED TO WIN
                if(player == 0) // Player: Rock
                {
                    // NPC: Scissor
                    satisfied = (npc == 1 ? satisfied = true : satisfied = false);
                }
                else if(player == 1) // Player: Scissor
                {
                    //NPC: Paper
                    satisfied = (npc == 2 ? satisfied = true : satisfied = false);
                }
                else // Player: Paper
                {
                    //NPC: Rock
                    satisfied = (npc == 0 ? satisfied = true : satisfied = false);
                }
                break;
            case 1: // NEED TO LOSE
                if (player == 0) // Player: Rock
                {
                    // NPC: Paper
                    satisfied = (npc == 2 ? satisfied = true : satisfied = false);
                }
                else if (player == 1) // Player: Scissor
                {
                    //NPC: Rock
                    satisfied = (npc == 0 ? satisfied = true : satisfied = false);
                }
                else // Player: Paper
                {
                    //NPC: Scissor
                    satisfied = (npc == 1 ? satisfied = true : satisfied = false);
                }
                break;
            case 2: // NEED TO SAME
                if (player == 0) // Player: Rock
                {
                    // NPC: Rock
                    satisfied = (npc == 0 ? satisfied = true : satisfied = false);
                }
                else if (player == 1) // Player: Scissor
                {
                    //NPC: Scissor
                    satisfied = (npc == 1 ? satisfied = true : satisfied = false);
                }
                else // Player: Paper
                {
                    //NPC: Paper
                    satisfied = (npc == 2 ? satisfied = true : satisfied = false);
                }
                break;
        }
        return satisfied;
    }
}
