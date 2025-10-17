using Dan.Models;
using System.Collections.Generic;
using UnityEngine;

public class FullLeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderboardPlayerEntry _entryPrefab;
    [SerializeField] private Transform _entriesContainer;
    [SerializeField] private int _maxEntries = 50;

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
        foreach (Transform child in _entriesContainer)
            Destroy(child.gameObject);

        Entry[] entries = _manager.Entries;
        Entry playerEntry = _manager.PlayerEntry;

        int playerIndex = playerEntry.Rank - 1;
        int lastIndex = entries.Length - 1;

        if (entries.Length > _maxEntries) lastIndex = _maxEntries - 1;

        for (int i = 0; i <= lastIndex; i++)
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
