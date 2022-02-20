using UnityEngine;


[System.Serializable]
public class Sound
{
    [Tooltip("The name which the system should search for in order to play this clip")]
    public string name;
    public AudioClip clip;


    //I think you have to set these variables in inspector since those values 
    //override these. Might be wrong though.
    [Range(0f,1f)]
    public float volume = 0.75f;
    [Range(0f,1f)]
    public float pitch = 1.0f;
    
    private AudioSource source;


    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    public void Loop()
    {
        source.loop = true;
    }
}
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField]
    Sound[] Music;
    [SerializeField]
    Sound[] SFX;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        GameObject _musicObject = new GameObject("Music");
        _musicObject.transform.SetParent(this.transform);
        for (int i = 0; i < Music.Length; ++i)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + Music[i].name);
            Music[i].SetSource(_go.AddComponent<AudioSource>());
            _go.transform.SetParent(_musicObject.transform);
        }

        GameObject _SFX = new GameObject("SFX");
        _SFX.transform.SetParent(this.transform);
        for (int i = 0; i < SFX.Length; ++i)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + SFX[i].name);
            SFX[i].SetSource(_go.AddComponent<AudioSource>());
            _go.transform.SetParent(_SFX.transform);
        }
        PlayMusic("Outro", true);
    }


    // Finds and plays the sfx sound that matches the name given in AudioManager
    public void PlaySound(string _name)
    {
        for(int i = 0; i < SFX.Length; ++i)
        {
            if(SFX[i].name == _name)
            {
                SFX[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager sound not found in array: " + _name);
    }


    /* Finds and plays the song that matches the name given in AudioManager,
     * set loop to true to make music repeat.
    */
    public void PlayMusic(string _name, bool loop = false)
    {
        for (int i = 0; i < Music.Length; ++i)
        {
            if (Music[i].name == _name)
            {
                if(loop)
                {
                    Music[i].Loop();
                }
                Music[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager music not found in array: " + _name);
    }
}
