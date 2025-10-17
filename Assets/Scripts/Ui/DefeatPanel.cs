using Dan.Main;
using System;
using TMPro;
using UnityEngine;


public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private ParticleSystem _highscoreParticles;
    [SerializeField] private TMP_Text _highscoreText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private UIPanelFader _localScore;
    [SerializeField] private UIPanelFader _leaderboard;
    [SerializeField] private NameInputPanel _nameEnterPanel;

    [Header("Translations")]
    [SerializeField] private TranslatedText _newBestTranslation;
    [SerializeField] private TranslatedText _personalBestTranslation;

    private void OnEnable()
    {
        UpdateHighscore();
    }

    private void OnDisable()
    {
        _nameEnterPanel.OnNameSubmitted -= OnNameSubmitted;
        LeaderboardManager.OnEntriesLoaded -= ShowBoard;
    }

    private void OnNameSubmitted()
    {
        _nameEnterPanel.PanelFader.FadePanel(false);
        LeaderboardManager.Instance.UploadPlayerEntry();
        LeaderboardManager.OnEntriesLoaded += ShowBoard;
    }

    private void ShowBoard()
    {
        _leaderboard.FadePanel(true);
    }

    private void UpdateHighscore()
    {
        int highscore = SaveSystem.SavesData.Highscore;
        int score = GameManager.Instance.CurrentScore;
        _scoreText.text = score.ToString();

        Debug.Log("Local score -> " + score + " lbScore -> " + highscore);

        if (score > highscore)
        {
            SaveSystem.SavesData.Highscore = score;
            SaveSystem.Save();

            _highscoreParticles.Play();
            _highscoreText.text = _newBestTranslation.Text;
            
            if (SaveSystem.SavesData.UserName == "New Player")
            {
                _nameEnterPanel.OnNameSubmitted += OnNameSubmitted;
                _nameEnterPanel.gameObject.SetActive(true);
            }
            else
            {
                LeaderboardManager.Instance.UploadPlayerEntry();
                _leaderboard.gameObject.SetActive(true);
            }
        }
        else
        {
            _localScore.gameObject.SetActive(true);
            _highscoreText.text = _personalBestTranslation.Text + ": " + highscore;
        }
    }
}
