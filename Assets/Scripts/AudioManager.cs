using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    struct AudioPlayer
    {
        public AudioSource source;
        public GameObject obj;
        public AudioPlayer(AudioSource source, GameObject obj)
        {
            this.source = source;
            this.obj = obj;
        }
    }
    private enum playerType { Music, SFX }
    [SerializeField] GameObject sfxPlayer;
    [SerializeField] GameObject musicPlayer;
    private readonly List<AudioPlayer> musicSources = new List<AudioPlayer>();
    private readonly List<AudioPlayer> sfxSources = new List<AudioPlayer>();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            // Generate SFX audio sources
            GameObject newSfxPlayer = Instantiate(sfxPlayer, transform);
            newSfxPlayer.SetActive(false);
            sfxSources.Add(new AudioPlayer(newSfxPlayer.GetComponent<AudioSource>(), newSfxPlayer));            
        }

        // Generate one music audio source
        GameObject newMusicPlayer = Instantiate(musicPlayer, transform);
        newMusicPlayer.SetActive(false);
        musicSources.Add(new AudioPlayer(newMusicPlayer.GetComponent<AudioSource>(), newMusicPlayer));
    }

    private AudioPlayer GetAudioPlayer(playerType type)
    {
        AudioPlayer GetNewAudioPlayer(playerType type)
        {
            if(type == playerType.SFX)
            {
                GameObject newSfxPlayer = Instantiate(sfxPlayer, transform);
                newSfxPlayer.SetActive(false);
                AudioPlayer newAudioPlayer = new AudioPlayer(newSfxPlayer.GetComponent<AudioSource>(), newSfxPlayer);
                sfxSources.Add(newAudioPlayer);
                return newAudioPlayer;
            }
            else if(type == playerType.Music)
            {
                GameObject newMusicPlayer = Instantiate(musicPlayer, transform);
                newMusicPlayer.SetActive(false);
                AudioPlayer newAudioPlayer = new AudioPlayer(newMusicPlayer.GetComponent<AudioSource>(), newMusicPlayer);
                musicSources.Add(newAudioPlayer);
                return newAudioPlayer;
            }
            throw new System.Exception("Can't create new audio player");
        }
        if(type == playerType.SFX)
        {
            foreach (AudioPlayer player in sfxSources)
            {
                if(!player.obj.activeInHierarchy)
                {
                    return player;
                }
            }
        }
        else if(type == playerType.Music)
        {
            foreach(AudioPlayer player in musicSources)
            {
                if (!player.obj.activeInHierarchy)
                {
                    return player;
                }
            }
        }
        return GetNewAudioPlayer(type);
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioPlayer player = GetAudioPlayer(playerType.SFX);
        player.obj.SetActive(true);
        player.source.PlayOneShot(clip);
        StartCoroutine(DisableGameObject(clip.length, player.obj));
    }
    public void PlayMusic(AudioClip clip)
    {
        AudioPlayer player = GetAudioPlayer(playerType.Music);
        player.obj.SetActive(true);
        player.source.PlayOneShot(clip);
        StartCoroutine(DisableGameObject(clip.length, player.obj));
    }

    private IEnumerator DisableGameObject(float timer, GameObject obj)
    {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }
}
