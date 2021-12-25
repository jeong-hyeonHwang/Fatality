using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 필요 요소들
 * <Canvas 위의 요소들>
 * roulette의 값에 해당하는 Text
 * roulette을 활성화시킬 Button
 */
public class Roulette : MonoBehaviour
{
    Text aboveText;
    Text rouletteText; // 갱신할 텍스트값
    Button rouletteButton; // 룰렛 활성화 버튼
    string[] textValue = {"통과", "실패"};
    bool rouletteIng; // 룰렛 버튼 기능 변경을 위한 bool

    public float changeTime; // 난이도 조절을 위한 글자 변경 주기값

    int isClear;

    public void Awake()
    {
        isClear = 2;

        aboveText = GameObject.Find("AboveText").GetComponent<Text>();
        rouletteText = GameObject.Find("RouletteT").GetComponent<Text>();
        rouletteButton = GameObject.Find("RouletteButton").GetComponent<Button>();
        rouletteIng = false;

        aboveText.text = "'통과'를 클릭하세요";
        rouletteText.text = "준비";
    }

    // Roulette 버튼 누른 경우
    public void RouletteButton()
    {
        if (rouletteIng == false) // roulette이 돌아가는 중이 아니었다면
        {
            rouletteButton.GetComponentInChildren<Text>().text = "그만!";
            StartCoroutine(ChangeRouletteText());
        }
        else // roulette이 돌아가는 중이었다면
        {
            rouletteIng = false;
            rouletteButton.GetComponentInChildren<Text>().text = "";
            if (rouletteText.text == textValue[0])
            {
                isClear = 1;
            }
            else
            {
                isClear = 0;
            }
            PanelManager.pManager.ActiveButtonDecide(isClear);
        }
    }

    IEnumerator ChangeRouletteText() // 룰렛 텍스트를 textValue에 저장된 string의 값으로 순차적으로 변경시켜줍니다.
    {
        int indexValue = 0;
        rouletteIng = true;
        while(rouletteIng == true)
        {
            rouletteText.text = textValue[indexValue];
            indexValue += 1;
            indexValue %= 2;           
            yield return new WaitForSeconds(changeTime);
        }       
    }
}
