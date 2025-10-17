using UnityEngine;
using Dan.Main;
using System;
using Dan.Models;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public Entry[] Entries { get; private set; }
    public Entry PlayerEntry { get; private set; }
    public static Action OnEntriesLoaded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckHighscore();
    }

    private void CheckHighscore()
    {
        int highscore = SaveSystem.SavesData.Highscore;
        
        Leaderboards.FruitMerge3D_LB1.GetPersonalEntry(entry =>
        {
            if (entry.Score >= highscore)
            {
                SaveSystem.SavesData.Highscore = entry.Score;
                SaveSystem.Save();
            }
            LoadEntries();
        });
    }

    private void LoadEntries()
    {
        Leaderboards.FruitMerge3D_LB1.GetEntries(entries =>
        {
            Entries = entries;
            foreach (var entry in Entries)
            {
                if(entry.IsMine())PlayerEntry = entry;
            }
            OnEntriesLoaded?.Invoke();
        });
    }

    public void UploadEntry(string username,int score)
    {
        Leaderboards.FruitMerge3D_LB1.UploadNewEntry(username, score, isSuccessful =>
        {
            if (isSuccessful)
                LoadEntries();
        });
    }

    public void UploadPlayerEntry()
    {
        UploadEntry(SaveSystem.SavesData.UserName, GameManager.Instance.CurrentScore);
    }
}
