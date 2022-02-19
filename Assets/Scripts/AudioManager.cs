using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

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
}
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        {
            for(int i = 0; i < sounds.Length; ++i)
            {
                GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
                sounds[i].SetSource(_go.AddComponent<AudioSource>());
                _go.transform.SetParent(this.transform);
            }
        }
    }

    public void PlaySound(string _name)
    {
        for(int i = 0; i < sounds.Length; ++i)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager sound not found in array: " + _name);
    }
}
