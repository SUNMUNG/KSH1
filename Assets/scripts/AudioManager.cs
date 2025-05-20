using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public GameObject sfxAudioSourcePrefab;
    public int sfxPoolSize = 10;

    [Header("Audio Clips")]
    public AudioClip bgmMain;
    public AudioClip bgmStage1;
    public AudioClip bgmStage2;
    public AudioClip bgmStage3;
    public AudioClip bgmStage4;
    public AudioClip bgmBoss1;
    public AudioClip sfxUI_Click;
    public AudioClip sfxShoot;
    public AudioClip sfxItem_Power;
    public AudioClip sfxItem_Score;
    public AudioClip sfxItem_Health;
    public AudioClip sfxItem_Ult;
    public AudioClip sfxExplosion;

    private List<AudioSource> sfxSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // bgmSource가 Inspector에서 연결 안 됐으면 직접 추가
            if (bgmSource == null)
            {
                bgmSource = gameObject.GetComponent<AudioSource>();
                if (bgmSource == null)
                {
                    Debug.LogWarning("bgmSource가 연결되어 있지 않습니다!");
                    bgmSource = gameObject.AddComponent<AudioSource>();
                }
            }

            // SFX 프리팹 자동 로드
            LoadSFXPrefab();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadSFXPrefab()
    {
        // Resources 폴더에서 SFX 프리팹 로드
        sfxAudioSourcePrefab = Resources.Load<GameObject>("AudioSourcePrefab/SFXAudioSource");
        
        if (sfxAudioSourcePrefab == null)
        {
            Debug.LogError("Resources/Audio/SFXAudioSource 프리팹을 찾을 수 없습니다!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 AudioSource 재설정
        SetupAudioSources();
    }

    private void SetupAudioSources()
    {
        // 기존 AudioSource 제거
        if (bgmSource != null)
        {
            Destroy(bgmSource);
        }
        foreach (var source in sfxSources)
        {
            if (source != null)
            {
                Destroy(source.gameObject);
            }
        }
        sfxSources.Clear();

        // BGM AudioSource 새로 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        // SFX 프리팹이 없으면 다시 로드 시도
        if (sfxAudioSourcePrefab == null)
        {
            LoadSFXPrefab();
        }

        // SFX AudioSource 풀 새로 생성
        if (sfxAudioSourcePrefab != null)
        {
            for (int i = 0; i < sfxPoolSize; i++)
            {
                GameObject obj = Instantiate(sfxAudioSourcePrefab, transform);
                AudioSource source = obj.GetComponent<AudioSource>();
                if (source != null)
                {
                    sfxSources.Add(source);
                }
            }
        }
        else
        {
            Debug.LogError("SFX 프리팹을 로드할 수 없어 SFX 풀을 생성할 수 없습니다!");
        }

        // 현재 씬에 맞는 BGM 재생
        PlaySceneBGM();
    }

    private void PlaySceneBGM()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        AudioClip clipToPlay = null;

        switch (currentScene)
        {
            case "MainMenu":
                clipToPlay = bgmMain;
                break;
            case "SettingMenu":
                clipToPlay = bgmMain;
                break;
            case "Stage1":
                clipToPlay = bgmStage1;
                break;
            case "Stage2":
                clipToPlay = bgmStage2;
                break;
            case "Stage3":
                clipToPlay = bgmStage3;
                break;
            case "Stage4":
                clipToPlay = bgmStage4;
                break;
            case "Boss1":
                clipToPlay = bgmBoss1;
                break;
        }

        if (clipToPlay != null)
        {
            PlayBGM(clipToPlay);
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }

        // 모두 사용 중이면 첫 번째 소스를 강제 사용
        sfxSources[0].clip = clip;
        sfxSources[0].volume = volume;
        sfxSources[0].Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
