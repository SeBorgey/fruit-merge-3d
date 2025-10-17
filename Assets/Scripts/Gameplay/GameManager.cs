using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentScore;
    public string CurrentLanguage = "en";
    public static GameManager Instance;

    private void Awake()
    {
        SaveSystem.Load();
        CurrentLanguage = SaveSystem.SavesData.Language;
        Instance = this;
    }
}
