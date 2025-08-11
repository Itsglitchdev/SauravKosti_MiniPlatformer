using System;

public static class EventBus
{

    // Triggering Block Event
    public static Action OnBlockTrigger01;
    public static void TriggerBlockFall() => OnBlockTrigger01?.Invoke();

    // Triggering Size Event
    public static Action OnBlockTrigger02;
    public static void TriggerBlockSizeChange() => OnBlockTrigger02?.Invoke();

    // Triggering Movemet For the Try Fall player Event
    public static Action OnBlockTrigger03;
    public static void TriggerBlockMoveAndTryFall() => OnBlockTrigger03?.Invoke();

    // Triggering Spikes Goes far away 
    public static Action OnSpikeTrigger01;
    public static void TriggerSpikesMovesFarAway() => OnSpikeTrigger01?.Invoke();

    // Triggering Spikes Comes Close
    public static Action OnSpikeTrigger02;
    public static void TriggerSpikesMovesComeClose() => OnSpikeTrigger02?.Invoke();

    // Triggering Spikes Change pos
    public static Action OnSpikeTrigger03;
    public static void TriggerSpikesMovesChangePosition() => OnSpikeTrigger03?.Invoke();
    
}