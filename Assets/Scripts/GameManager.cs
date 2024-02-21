using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{
    public const string BestScoreKey = "BestScore";

    public List<Locale> languages;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endGamePanel;


    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    int currentScore = 0;

    public Transform allFruits;

    #region Instance
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton for GameManager already exist, destroying the old one");
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ResetScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePanel.activeInHierarchy)
            {
                Pause();
            }
            else if (pausePanel.activeInHierarchy)
            {
                PlayGame();
            }
        }

    }

    public void ResetScene()
    {
        ResetScore();
        foreach (Transform transform in allFruits)
        {
            Destroy(transform.gameObject);
        }

    }

    public void SelectNextLocale()
    {
        int localePosition = languages.FindIndex(x => x == LocalizationSettings.SelectedLocale);
        if(localePosition == languages.Count - 1)
        {
            LocalizationSettings.SelectedLocale = languages[0];
        }
        else
        {
            LocalizationSettings.SelectedLocale = languages[localePosition + 1];
        }
    }
    public void SelectPreviousLocale()
    {
        int localePosition = languages.FindIndex(x => x == LocalizationSettings.SelectedLocale);
        if (localePosition == 0)
        {
            LocalizationSettings.SelectedLocale = languages[languages.Count - 1];
        }
        else
        {
            LocalizationSettings.SelectedLocale = languages[localePosition - 1];
        }
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
        endScoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        endScoreText.text = currentScore.ToString();
    }

    public int GetBestScore()
    {
       return PlayerPrefs.GetInt(BestScoreKey);
    }

    public void SaveBestScore()
    {
        if(currentScore > GetBestScore())
        {
            PlayerPrefs.SetInt(BestScoreKey, currentScore);
            PlayerPrefs.Save();
        }
        bestScoreText.text = GetBestScore().ToString();
    }

    public void PlayGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        endGamePanel.SetActive(false); 
    }

    public void GameOver()
    {
        SaveBestScore();
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        endGamePanel.SetActive(true);
    }

    public void Pause()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        endGamePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #region UI
    public void UpScaleText(Transform _transform)
    {
        _transform.DOScale(1.25f, 0.5f);
    }

    public void DownScaleText(Transform _transform)
    {
        _transform.DOScale(1f, 0.5f);
    }

    public void JellyText(Transform _transform)
    {
        _transform.DOShakePosition(1);
    }
    #endregion
}
