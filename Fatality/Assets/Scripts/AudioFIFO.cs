using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFIFO : MonoBehaviour
{
    AudioSource bgm;

    float resTime = 1f;
    public float maxVolume = 0.28f;
    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
        bgm.volume = maxVolume / 2;
    }

    private void Start()
    {
        StartCoroutine(AudioFadeIn());
    }

    IEnumerator AudioFadeIn()
    {
        bgm.Play();
        var currentTime = 0f;

        while(currentTime < resTime)
        {
            bgm.volume = Mathf.Lerp(bgm.volume, maxVolume, currentTime / resTime);
            currentTime += Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator AudioFadeOut()
    {
        var currentTime = 0f;

        while (currentTime < resTime)
        {
            bgm.volume = Mathf.Lerp(bgm.volume, 0, currentTime / resTime);
            currentTime += Time.deltaTime;

            yield return null;
        }
    }

}
