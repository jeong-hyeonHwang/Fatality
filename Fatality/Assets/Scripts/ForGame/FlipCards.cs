using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* 필요 요소들
 * <Canvas 위의 요소들>
 * FlipCards의 Condition을 띄울 Text
 * 현재 맞춘 카드 페어 수 띄울 Text
 * 플레이어가 클릭할 카드 버튼들
 * 카드 하이라이트 색상
 * 몇 초 대기 후 카드의 번호를 지울 것인
 */
public class FlipCards : MonoBehaviour
{
    Text resultText;

    Button[] cards; // 카드 버튼 저장할 공간
    Text[] cardText; // 카드 버튼의 Text 저장할 공간
    int sameCardNum; // 같은 숫자 카드의 수

    [SerializeField]
    List<int> cardNum;

    bool clickedFirst; // 두 카드 선택 중, 첫번째 카드 선택 구간인가
    string beforeClicked; // 첫번째 선택한 카드의 값 담는 곳

    int endCardPairNum; // 카드 번호의 한계 숫자
    int rightPairFor; // 현재 맞춘 카드 페어의 수
    
    public Color32 textHighlightColor; // 텍스트가 보일 때의 컬러
    Color cardColor; // 카드의 색
    public float waitFor; // 몇 초 후 text를 사라지게 만들지

    int isClear;

    private void Awake()
    {
        isClear = 2;

        resultText = GameObject.Find("ResultText").GetComponent<Text>();
        cardNum = new List<int>();

        cards = GameObject.Find("CardList").GetComponentsInChildren<Button>();
        cardText = new Text[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            cardText[i] = cards[i].gameObject.GetComponentInChildren<Text>();
            cardText[i].color = textHighlightColor;
            cards[i].interactable = false;
        }


        cardColor = cards[0].GetComponent<Image>().color;

        sameCardNum = 2; // 11 22 33...

        clickedFirst = false;
        CardNumChoose();
        resultText.text = Mathf.FloorToInt(waitFor) + " 초";
    }

    private void Start()
    {
        StartCoroutine(CardTextColorChange());
    }

    IEnumerator CardTextColorChange()
    {
        var currentTime = 0f;
        var currentInt = 0;
        while(currentTime <= waitFor)
        {
            currentTime += Time.deltaTime;
            if(Mathf.FloorToInt(currentTime) > currentInt)
            {
                currentInt = Mathf.FloorToInt(currentTime);
                resultText.text = (Mathf.FloorToInt(waitFor) - currentInt) + " 초";
            }
            yield return null;
        }

        resultText.text = "알맞은 숫자쌍을 찾으세요!";

        for (int i =0; i < cardText.Length; i++)
        {
            cardText[i].color = cardColor;
            cards[i].interactable = true;
        }
    }

    
    public void CardNumChoose() // 각각의 Card에 숫자를 할당
    {
        endCardPairNum = cards.Length / sameCardNum;
        int[] cardLimitCheck = new int[endCardPairNum];

        int currentCardIn = Random.Range(1, endCardPairNum + 1);
        for (int i =0; i < cardText.Length;)
        {
            if(cardLimitCheck[currentCardIn - 1] != sameCardNum)
            {
                cardNum.Add(currentCardIn);
                cardText[i].text = cardNum[i].ToString();
                cardLimitCheck[currentCardIn - 1] += 1;
                i++;
            }
                currentCardIn = Random.Range(1, endCardPairNum + 1);
        }
    }

    public void ClickedCard() // 카드 클릭 시
    {
        var obj = EventSystem.current.currentSelectedGameObject;
        obj.GetComponentInChildren<Text>().color = textHighlightColor;
        if (clickedFirst == false) // 두 카드 클릭 중 첫 카드 클릭 시
        {
            beforeClicked = obj.GetComponentInChildren<Text>().text;
            obj.GetComponent<Button>().interactable = false;
            clickedFirst = true;
        }
        else // 두 카드 클릭 중 두 번째 카드 클릭 시
        {
            var currentClicked = obj.GetComponentInChildren<Text>().text;
            if (beforeClicked == currentClicked) // 첫 카드의 숫자와 두 번째 카드의 값이 일치하는지 판별
            {
                rightPairFor += 1;
                resultText.text = rightPairFor + " 쌍";
                clickedFirst = false;
                beforeClicked = null;
                obj.GetComponent<Button>().interactable = false;
                ClearCheck();
            }
            else
            {
                resultText.text = "";
                for(int i =0; i < cards.Length; i++)
                {
                    cardText[i].color = textHighlightColor;
                    cards[i].interactable = false;
                }
                isClear = 0;
                PanelManager.pManager.ActiveButtonDecide(isClear);
            }
        }
    }

    public void ClearCheck()
    {
        if(rightPairFor == endCardPairNum)
        {
            resultText.text = "";
            isClear = 1;
        }

        PanelManager.pManager.ActiveButtonDecide(isClear);
    }
}
