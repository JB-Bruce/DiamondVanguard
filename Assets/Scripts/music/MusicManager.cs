using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] float fadeOutDuration;
    [SerializeField] AudioSource fightMusicLoop1;
    [SerializeField] AudioSource fightMusicLoop2;
    private float volmaxloop1;
    private float volmaxloop2;
    private int ennemyCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("there is 2 music manager");
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        ennemyCount = 0;
        volmaxloop1 = fightMusicLoop1.volume;
        volmaxloop2 = fightMusicLoop2.volume;
    }

    public void AddEnnemy()
    {
        if(ennemyCount == 0)
        {
            PlayMusic();
        }
        ennemyCount += 1;
    }
    public void RemoveEnnemy()
    {
        ennemyCount -= 1;
        if(ennemyCount == 0)
        {
            StartCoroutine(FadeOutMusic());
        }
    }

    public void PlayMusic()
    {

        if (!fightMusicLoop1.isPlaying && !fightMusicLoop2.isPlaying)
        {
            fightMusicLoop1.volume = volmaxloop1;
            fightMusicLoop2.volume = volmaxloop2;

            fightMusicLoop1.Play();
            Invoke("PlayLoop", fightMusicLoop1.clip.length);
        }
    }
    private void PlayLoop()
    {
        fightMusicLoop2.Play();
    }
    
    public IEnumerator FadeOutMusic()
    {
        float currenttimer=0f;
        while (currenttimer < fadeOutDuration)
        {
            currenttimer += Time.deltaTime;
            fightMusicLoop1.volume = Mathf.Lerp(volmaxloop1, 0, currenttimer / fadeOutDuration);
            fightMusicLoop2.volume = Mathf.Lerp(volmaxloop2, 0, currenttimer / fadeOutDuration);
            yield return null;
        }
        StopMusic();
    }

    private void StopMusic()
    {
        fightMusicLoop1.Stop();
        fightMusicLoop2.Stop();
    }
}
