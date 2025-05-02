using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else Destroy(gameObject);
    }


    [System.Serializable]
    public class StageData
    {
        public string stageName;
        public int stars; // 0~3 ���� ��
    }

    [System.Serializable]
    public class GameSaveData
    {
        public List<StageData> stageDataList = new List<StageData>();
    }
    private string SavePath => Application.persistentDataPath + "/save.json";

    public GameSaveData saveData = new GameSaveData();
    
    public void Save()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("���� �Ϸ�!");
    }

    public void Load()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            saveData = JsonUtility.FromJson<GameSaveData>(json);
            Debug.Log("�ҷ����� �Ϸ�!");
        }
        else
        {
            saveData = new GameSaveData(); // ó�� ���� ��
            Debug.Log("���̺� ���� ����. ���� ����.");
        }
    }

    public void SetStageStar(string stageName, int stars)
    {
        var stage = saveData.stageDataList.Find(s => s.stageName == stageName);
        if (stage == null)
        {
            stage = new StageData { stageName = stageName, stars = stars };
            saveData.stageDataList.Add(stage);
        }
        else
        {
            // �� ���� �� ���Ÿ� ���
            if (stars > stage.stars)
                stage.stars = stars;
        }

        Save(); // ���� ��� ����
    }

    public int GetStageStar(string stageName)
    {
        var stage = saveData.stageDataList.Find(s => s.stageName == stageName);
        return stage != null ? stage.stars : 0;
    }
}