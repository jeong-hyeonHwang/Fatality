using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public float waitFor;

    public IEnumerator ReloadScene(int sceneIndex)
    {
        Animator anim = GameObject.Find("Fader").GetComponent<Animator>();
        anim.Play("FadeOut", default);
        yield return new WaitForSeconds(waitFor);
        SceneManager.LoadScene(sceneIndex);
    }

    public IEnumerator FadeOutOnly()
    {
        Animator anim = GameObject.Find("Fader").GetComponent<Animator>();
        anim.Play("FadeOut", default);
        yield return new WaitForSeconds(waitFor);
        Application.Quit();
    }
}
