using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void SaveHighScore(int score)
    {
        SaveData data = new SaveData();
        data.highScore = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(savePath, json);
    }

    public int LoadHighScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.highScore;
        }
        return 0;
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
    }
}
