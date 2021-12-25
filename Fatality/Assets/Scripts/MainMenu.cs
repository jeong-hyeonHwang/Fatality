using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public FadeManager fManager;
    void Start()
    {
        fManager = Transform.FindObjectOfType<FadeManager>();
    }
    
    public void StartGame()
    {
        StartCoroutine(fManager.ReloadScene(1));
    }
    
    public void ExitGame () 
    {
        AudioFIFO aFIFO = FindObjectOfType<AudioFIFO>();
        StartCoroutine(aFIFO.AudioFadeOut());
        StartCoroutine(fManager.FadeOutOnly());
    }
    
}
