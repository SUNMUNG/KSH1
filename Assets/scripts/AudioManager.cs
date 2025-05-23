using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public GameObject sfxAudioSourcePrefab;
    public int sfxPoolSize = 10;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    [Range(0f, 1f)]
    public float bgmVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    [Header("UI References")]
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

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
    public AudioClip sfxItem_Score_High;
    public AudioClip sfxItem_Score_Medium;
    public AudioClip sfxItem_Score_Low;
    public AudioClip sfxItem_Health;
    public AudioClip sfxHit;
    public AudioClip sfxItem_Ult;
    public AudioClip sfxItem_Ult_used;
    public AudioClip sfxExplosion;

    private List<AudioSource> sfxSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 볼륨 설정 로드
            LoadVolumeSettings();

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

    void Start()
    {
        // 슬라이더 초기값 설정
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }

        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.value = bgmVolume;
            bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
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
        // 슬라이더 자동 연결
        ConnectVolumeSliders();
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
                source.volume = masterVolume * sfxVolume * volume;
                source.Play();
                return;
            }
        }

        // 모두 사용 중이면 첫 번째 소스를 강제 사용
        sfxSources[0].clip = clip;
        sfxSources[0].volume = masterVolume * sfxVolume * volume;
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

    private void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        ApplyVolumeSettings();
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
        ApplyVolumeSettings();
    }

    public void OnMasterVolumeChanged(float value)
    {
        masterVolume = value;
        ApplyVolumeSettings();
        SaveVolumeSettings();
    }

    public void OnBGMVolumeChanged(float value)
    {
        bgmVolume = value;
        ApplyVolumeSettings();
        SaveVolumeSettings();
    }

    public void OnSFXVolumeChanged(float value)
    {
        sfxVolume = value;
        ApplyVolumeSettings();
        SaveVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = masterVolume * bgmVolume;
        }

        foreach (var source in sfxSources)
        {
            if (source != null)
            {
                source.volume = masterVolume * sfxVolume;
            }
        }
    }

    private void ConnectVolumeSliders()
    {
        // 모든 슬라이더 찾기 (비활성화된 오브젝트 포함)
        Slider[] allSliders = Resources.FindObjectsOfTypeAll<Slider>();
        Debug.Log($"발견된 슬라이더 개수: {allSliders.Length}");
        
        foreach (Slider slider in allSliders)
        {
            // 씬에 있는 오브젝트만 처리 (프리팹 등은 제외)
            if (!slider.gameObject.scene.isLoaded) continue;

            Debug.Log($"슬라이더 이름: {slider.name}");
            // 슬라이더 이름에 따라 자동 연결
            if (slider.name.Contains("MasterVolume"))
            {
                Debug.Log("마스터 볼륨 슬라이더 연결됨");
                ConnectMasterVolumeSlider(slider);
            }
            else if (slider.name.Contains("BGMVolume"))
            {
                Debug.Log("BGM 볼륨 슬라이더 연결됨");
                ConnectBGMVolumeSlider(slider);
            }
            else if (slider.name.Contains("SFXVolume"))
            {
                Debug.Log("SFX 볼륨 슬라이더 연결됨");
                ConnectSFXVolumeSlider(slider);
            }
        }
    }

    private void ConnectMasterVolumeSlider(Slider slider)
    {
        Debug.Log($"마스터 볼륨 슬라이더 초기값 설정: {masterVolume}");
        slider.value = masterVolume;
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnMasterVolumeChanged);
    }

    private void ConnectBGMVolumeSlider(Slider slider)
    {
        Debug.Log($"BGM 볼륨 슬라이더 초기값 설정: {bgmVolume}");
        slider.value = bgmVolume;
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    private void ConnectSFXVolumeSlider(Slider slider)
    {
        Debug.Log($"SFX 볼륨 슬라이더 초기값 설정: {sfxVolume}");
        slider.value = sfxVolume;
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    void Update()
    {
        
    }

}
