
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip -------------")]
    public AudioClip lever, playerAttack, spiderAttack, chest, coffin, heal, takeDamage, fall, playerDeath, ambient, gate, skeletonAttack, bossAttack;

    private float musicPauseTime = 0f;

    private void Awake()
    {      
        PlayMusic(ambient);
        DontDestroyOnLoad(gameObject);
    }


    // Método para tocar o efeito sonoro
    public void PlaySFX(AudioClip clip)
    {
        if(clip == playerAttack || clip == spiderAttack || clip == skeletonAttack || clip == bossAttack)
        {
            SFXSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
        }
        SFXSource.PlayOneShot(clip);
    }

    // Método para tocar a música (se necessário)
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void HandlePlayStopMusic(AudioClip clip)
    {
        if (musicSource.isPlaying && musicSource.clip == clip)
        {
            musicPauseTime = musicSource.time;
            musicSource.Pause();
        }
        else if (!musicSource.isPlaying && musicSource.clip == clip)
        {
            musicSource.clip = clip;
            musicPauseTime = 0f;
            musicSource.Play();
        }
    }
}
