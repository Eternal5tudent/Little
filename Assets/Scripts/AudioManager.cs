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
    private enum playerType { Music, SFX, SFXLooped }
    [SerializeField] GameObject sfxPlayer;
    [SerializeField] GameObject sfxLoopedPlayer;
    [SerializeField] GameObject musicPlayer;
    private readonly List<AudioPlayer> musicSources = new List<AudioPlayer>();
    private readonly List<AudioPlayer> sfxSources = new List<AudioPlayer>();
    private readonly List<AudioPlayer> sfxLoopedSources = new List<AudioPlayer>();
    private readonly Dictionary<int, AudioPlayer> playingLoopedSFX = new Dictionary<int, AudioPlayer>();
    List<int> uniqueAudioIDUsed = new List<int>();
    int lastAudioID = 0;

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

        //Generate one SFX Looped audio source
        GameObject newSfxLoopedPlayer = Instantiate(sfxLoopedPlayer, transform);
        newSfxLoopedPlayer.SetActive(false);
        sfxLoopedSources.Add(new AudioPlayer(newSfxLoopedPlayer.GetComponent<AudioSource>(), newSfxLoopedPlayer));
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
            else if (type == playerType.SFXLooped)
            {
                GameObject newSfxLoopedPlayer = Instantiate(sfxLoopedPlayer, transform);
                newSfxLoopedPlayer.SetActive(false);
                AudioPlayer newAudioPlayer = new AudioPlayer(newSfxLoopedPlayer.GetComponent<AudioSource>(), newSfxLoopedPlayer);
                sfxSources.Add(newAudioPlayer);
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
        else if (type == playerType.SFXLooped)
        {
            foreach (AudioPlayer player in sfxLoopedSources)
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
    public void PlaySFXLooped(AudioClip clip, int uniqueID)
    {
        AudioPlayer player = GetAudioPlayer(playerType.SFXLooped);
        player.obj.SetActive(true);
        player.source.clip = clip;
        player.source.Play();
        playingLoopedSFX.Add(uniqueID, player);
    }

    public void StopSFXLooped(int uniqueID)
    {
        bool audioPlayerExists = playingLoopedSFX.TryGetValue(uniqueID, out AudioPlayer player);
        if(audioPlayerExists)
        {
            player.source.Stop();
            player.obj.SetActive(false);
            playingLoopedSFX.Remove(uniqueID);
        }
       
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

    public int GetUniqueAudioID()
    {
        for(int i = 0; i<500; i++)
        {
            if(!uniqueAudioIDUsed.Contains(i))
            {
                return i;
            }
        }
        Debug.LogError("Couldn't find a free unique audio id!");
        return Random.Range(500, 1000);
    }
}
