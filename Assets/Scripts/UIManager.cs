using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelText;


    void Start()
    {
        InitializedUI();
        ButtonEventHandlers();
    }

    void InitializedUI()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);

    }

    void ButtonEventHandlers()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    
    

}