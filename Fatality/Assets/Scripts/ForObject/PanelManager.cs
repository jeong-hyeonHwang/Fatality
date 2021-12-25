using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager pManager;
    public GameObject[] panelList;
    public bool panelOpenStatus;

    GameObject activePanelCalledFrom;
    GameObject currentActivePanel;

    public GameObject sceneReloadButton;
    GameObject sceneExitButton;
    GameObject panelCloseButton;

    ClearStatusChecker csChecker;

    GameObject shadow;

    Text clearWord;
    Text buttonExplainer;

    FadeManager fManager;

    private void Awake()
    {
        pManager = this;

        panelList = GameObject.FindGameObjectsWithTag("GamePanel");
        for(int i =0; i < panelList.Length; i++)
        {
            panelList[i].SetActive(false);
        }
        panelOpenStatus = false;
        sceneExitButton = GameObject.Find("ExitButton");
        sceneReloadButton = GameObject.Find("SceneReloadButton");
        panelCloseButton = GameObject.Find("PanelCloseButton");
        sceneExitButton.SetActive(false);
        sceneReloadButton.SetActive(false);
        panelCloseButton.SetActive(false);

        csChecker = FindObjectOfType<ClearStatusChecker>();

        shadow = GameObject.Find("Shadow");
        shadow.SetActive(false);

        clearWord = GameObject.Find("ClearWord").GetComponent<Text>();
        clearWord.text = "";

        buttonExplainer = GameObject.Find("ButtonExplainer").GetComponent<Text>();
        buttonExplainer.text = "";

        fManager = FindObjectOfType<FadeManager>();

    }

    public void OpenPanel(GameObject obj)
    {
        panelOpenStatus = true;
        activePanelCalledFrom = obj;
        GameObject panelIs = obj.GetComponent<AllocatedPanelIs>().panelIs;
        currentActivePanel = panelIs;
        panelIs.SetActive(true);
        if(panelIs.name == "ClearPanel")
        {
            sceneReloadButton.SetActive(true);
            sceneExitButton.SetActive(true);
        }
    }
    
    public void CloseButtonActive()
    {
        clearWord.text = "성공!";
        buttonExplainer.text = "X버튼을 클릭하세요";
        csChecker.snowParticles[csChecker.currentClearStage].sprite = csChecker.clearedSnowParticleImg.sprite;
        shadow.SetActive(true);
        panelCloseButton.SetActive(true);
    }

    public void ClosePanel()
    {
        clearWord.text = "";
        buttonExplainer.text = "";
        panelCloseButton.SetActive(false);        
        shadow.SetActive(false);
        csChecker.ClearStageNumIncrease();
        panelOpenStatus = false;
        currentActivePanel.SetActive(false);
        activePanelCalledFrom.SetActive(false);
        currentActivePanel = null;

    }

    public void SceneReloadButtonActive()
    {
        clearWord.text = "실패...";
        buttonExplainer.text = "RESTART버튼 클릭 시 재시작";
        shadow.SetActive(true);
        sceneExitButton.SetActive(true);
        sceneReloadButton.SetActive(true);
    }

    public void SceneReload()
    {
        StartCoroutine(fManager.ReloadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void ExitButtonPressed()
    {
        StartCoroutine(fManager.ReloadScene(0));
    }

    public void ActiveButtonDecide(int isClear)
    {
        if (isClear == 1)
        {
            PanelManager.pManager.CloseButtonActive();
        }
        else if (isClear == 0)
        {
            PanelManager.pManager.SceneReloadButtonActive();
        }
    }
}
