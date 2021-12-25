using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearStatusChecker : MonoBehaviour
{
    [SerializeField]
    public Image[] snowParticles;
    public SpriteRenderer clearedSnowParticleImg;

    Text clearStatus;

    public int currentClearStage;
    public int allClearStage;

    Text clearTime;

    GameObject clearPanelActivator;
    GameObject clearPanel;

    private void Awake()
    {
        snowParticles = GameObject.Find("SNOW").GetComponentsInChildren<Image>();
        clearedSnowParticleImg = GameObject.Find("ChangSnowParticleSprite").GetComponent<SpriteRenderer>();
        currentClearStage = 0;
        allClearStage = 6;
        clearStatus = GameObject.Find("SixStageClearStatus").GetComponent<Text>();
        ClearStatusTextChange(currentClearStage);

        clearTime = GameObject.Find("ClearTimeText").GetComponent<Text>();
        clearTime.gameObject.SetActive(false);
        clearPanelActivator = GameObject.Find("ClearPanelActivator");
        clearPanelActivator.SetActive(false);
        clearPanel = GameObject.Find("ClearPanel");
        clearPanel.SetActive(false);
    }

    //private void FixedUpdate()
    //{
    //    Debug.Log(Mathf.FloorToInt(Time.timeSinceLevelLoad));
    //}

    public void ClearStageNumIncrease()
    {
        currentClearStage += 1;
        ClearStatusTextChange(currentClearStage);
        if(currentClearStage == allClearStage)
        {
            clearPanelActivator.SetActive(true);
            clearTime.gameObject.SetActive(true);
            var time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
            var min = time / 60;
            var sec = time % 60;
            clearTime.text = min + "m " + sec + "s";
            clearStatus.text = "ALL CLEAR";
        }
    }

    public void ClearStatusTextChange(int num)
    {
        clearStatus.text = num + " / " + allClearStage;
    }
}
