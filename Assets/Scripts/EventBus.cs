using System;

public static class EventBus
{

    // Game Started
    public static Action OnGameStarted;
    public static void TriggerGameStarted() => OnGameStarted?.Invoke();

    // Level Text set
    public static Action<int> OnLevelTextSet;
    public static void SetLevelText(int level) => OnLevelTextSet?.Invoke(level);

    // Level Complete
    public static Action OnLevelOneCompleted;
    public static void TriggerLevelOneCompleted() => OnLevelOneCompleted?.Invoke();

    // Show loading text
    public static Action<string> OnShowLoadingMessage;
    public static void TriggerLoadingMessage(string message) => OnShowLoadingMessage?.Invoke(message);

    // Hide loading panel
    public static Action OnHideLoading;
    public static void TriggerHideLoading() => OnHideLoading?.Invoke();

    public static Action OnBlockTrigger01;
    public static void BlockTrigger01() => OnBlockTrigger01?.Invoke();

    public static Action OnBlockTrigger02;
    public static void BlockTrigger02() => OnBlockTrigger02?.Invoke();

    public static Action OnBlockTrigger03;
    public static void BlockTrigger03() => OnBlockTrigger03?.Invoke();

    public static Action OnBlockTrigger04;
    public static void BlockTrigger04() => OnBlockTrigger04?.Invoke();

    public static Action OnBlockTrigger05;
    public static void BlockTrigger05() => OnBlockTrigger05?.Invoke();

    public static Action OnBlockTrigger06;
    public static void BlockTrigger06() => OnBlockTrigger06?.Invoke();

    public static Action OnBlockTrigger07;
    public static void BlockTrigger07() => OnBlockTrigger07?.Invoke();

    public static Action OnBlockTrigger08;
    public static void BlockTrigger08() => OnBlockTrigger08?.Invoke();

    public static Action OnBlockTrigger09;
    public static void BlockTrigger09() => OnBlockTrigger09?.Invoke();

    public static Action OnBlockTrigger11;
    public static void BlockTrigger11() => OnBlockTrigger11?.Invoke();

    
}