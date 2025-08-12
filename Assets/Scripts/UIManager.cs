using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject loadingPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI loadingText;

    void OnEnable()
    {
        EventBus.OnLevelTextSet += UpdateLevelText;
        EventBus.OnShowLoadingMessage += ShowLoadingText;
        EventBus.OnHideLoading += HideLoadingPanel;
    }

    void OnDisable()
    {
        EventBus.OnLevelTextSet -= UpdateLevelText;
        EventBus.OnShowLoadingMessage -= ShowLoadingText;
        EventBus.OnHideLoading -= HideLoadingPanel;
    }

    void Start()
    {
        InitializedUI();
        ButtonEventHandlers();
    }

    void InitializedUI()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    void ButtonEventHandlers()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        EventBus.OnGameStarted();
    }

    void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

    private void ShowLoadingText(string message)
    {
        loadingText.text = message;
        gamePanel.SetActive(false);
        loadingPanel.SetActive(true);
    }

    private void HideLoadingPanel()
    {
        loadingPanel.SetActive(false);  
        gamePanel.SetActive(true);
    }
    

}