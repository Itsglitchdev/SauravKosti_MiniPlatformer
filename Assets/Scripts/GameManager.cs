using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelOneDesign;
    [SerializeField] private GameObject levelTwoDesign;

    private readonly Vector3 startLevelOnePos = new Vector3(-5f, 0f, 0f);
    private readonly Vector3 startLevelTwoPos = new Vector3(450f, 0f, 0f);

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
        EventBus.OnLevelOneCompleted += HandleLevelCompleted;
        EventBus.OnLevelTwoCompleted += HandleLevelTwoCompleted;
        EventBus.OnRespawn += HandleRespawn;

    }

    void OnDisable()
    {
        EventBus.OnGameStarted -= StartGame;
        EventBus.OnLevelOneCompleted -= HandleLevelCompleted;
        EventBus.OnLevelTwoCompleted -= HandleLevelTwoCompleted; 
        EventBus.OnRespawn -= HandleRespawn;
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
        player.transform.position = startLevelOnePos;
        player.SetActive(true);
        levelOneDesign.SetActive(true);
        EventBus.SetLevelText(currentLevel);
    }

    private void LevelOneComplete()
    {
        IsGameStarted = false;
        player.SetActive(false);
        levelOneDesign.SetActive(false);
        levelTwoDesign.SetActive(false);

    }

    private void HandleLevelCompleted()
    {
        LevelOneComplete();
        EventBus.TriggerLoadingMessage("Loading Level 2...");
        StartCoroutine(DelayBeforeLevelChange());
    }

    private IEnumerator DelayBeforeLevelChange()
    {
        yield return new WaitForSeconds(3f);
        EventBus.TriggerHideLoading();
        LevelChange();
    }

    private void LevelChange()
    {
        if (currentLevel == 1)
        {
            IsGameStarted = true;
            currentLevel = 2;
            player.transform.position = startLevelTwoPos;
            player.SetActive(true);
            levelOneDesign.SetActive(false);
            levelTwoDesign.SetActive(true);
            EventBus.SetLevelText(currentLevel);
        }
    }

    private void HandleLevelTwoCompleted()
    {
        IsGameStarted = false;
        player.SetActive(false);
        levelTwoDesign.SetActive(false);

        EventBus.TriggerLoadingMessage("Coming Soon...");
    }

    private void HandleRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    
    private IEnumerator RespawnCoroutine()
    {
        player.SetActive(false); 

        yield return new WaitForSeconds(2f);

        Vector3 targetPos = currentLevel == 1 ? startLevelOnePos : startLevelTwoPos;
        player.transform.position = targetPos;

        player.SetActive(true); 
    }


    
}