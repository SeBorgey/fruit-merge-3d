using Dan.Models;
using System.Collections.Generic;
using UnityEngine;

public class MiniLeaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardPlayerEntry _entryPrefab;
    [SerializeField] private Transform _entriesContainer;
    [SerializeField] private int _playersBefore = 3;
    [SerializeField] private int _playersAfter = 2;

    private List<LeaderboardPlayerEntry> _entryObjects = new();

    private LeaderboardManager _manager;

    private void OnEnable()
    {
        _manager = LeaderboardManager.Instance;
        LeaderboardManager.OnEntriesLoaded += UpdateEntries;
        UpdateEntries();
    }

    private void OnDisable()
    {
        LeaderboardManager.OnEntriesLoaded -= UpdateEntries;
    }

    private void UpdateEntries()
    {
        Debug.Log("Updading leaderboard...");
        foreach (Transform child in _entriesContainer)
            Destroy(child.gameObject);

        Entry[] entries = _manager.Entries;
        Entry playerEntry = _manager.PlayerEntry;

        int playerIndex = playerEntry.Rank - 1;
        int firstIndex = playerIndex - _playersBefore;
        int lastIndex = playerIndex + _playersAfter;
        int showTotal = _playersAfter + _playersBefore;

        if(firstIndex < 0) firstIndex = 0;
        if(lastIndex < showTotal) lastIndex = showTotal;
        if (lastIndex < playerIndex) lastIndex = playerIndex;
        if(lastIndex > entries.Length-1) lastIndex = entries.Length-1;

        for (int i = firstIndex; i <= lastIndex; i++)
        {
            var entry = Instantiate(_entryPrefab, _entriesContainer);
            _entryObjects.Add(entry);
            entry.EntryText.text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            if (entries[i].IsMine())
            {
                entry.EntryText.color = Color.yellow;
            }
        }
    }
}
