using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField, Header("BGMオーディオソース")]
    private AudioSource bgmAudioSource;

    [SerializeField, Header("SEオーディオソース")]
    private AudioSource seAudioSource;

    [SerializeField, Header("BGMオーディオクリップ")]
    private AudioClip[] bgmAudioClip;

    [SerializeField, Header("SEオーディオクリップ")]
    private AudioClip[] seAudioClip;

    [Header("BGMループ")]
    public bool bgmLoop = true;

    [Header("BGMミュート")]
    public bool bgmMute = true;

    public static AudioPlay instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Update()
    {
        bgmAudioSource.mute = bgmMute;
    }

    public void BGMPlay(int value)
    {
        bgmAudioSource.loop = bgmLoop;
        if (value < bgmAudioClip.Length && 0 <= value) bgmAudioSource.clip = bgmAudioClip[value];
        bgmAudioSource.Play();
    }

    public void BGMStop()
    {
        bgmAudioSource.Stop();
    }

    public void SEPlay(int value)
    {
        if (value < seAudioClip.Length && 0 <= value) seAudioSource.clip = seAudioClip[value];
        seAudioSource.Play();
    }
}
