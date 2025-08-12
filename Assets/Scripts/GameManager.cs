using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelOneDesign;
    [SerializeField] private GameObject levelTwoDesign;

    private readonly Vector3 startLevelOnePos = new Vector3(-5f, 0f, 0f);

    public bool IsGameStarted { get; private set; }
    private int currentLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        EventBus.OnGameStarted += StartGame;
    }

    void OnDisable()
    {
        EventBus.OnGameStarted -= StartGame;
    }

    private void Start()
    {
        IsGameStarted = false;
        player.SetActive(false);
        levelOneDesign.SetActive(false);
        levelTwoDesign.SetActive(false);
    }

    private void StartGame()
    {
        currentLevel = 1;
        IsGameStarted = true;
        // player.transform.position = startLevelOnePos;
        player.SetActive(true);
        levelOneDesign.SetActive(true);
        EventBus.SetLevelText(currentLevel);
    }
    
}