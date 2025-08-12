using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    [SerializeField] private TriggerActionType actionType;
    [SerializeField] private bool destroyAfterTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        switch (actionType)
        {
            case TriggerActionType.BlockTrigger_01:
                EventBus.BlockTrigger01();
                break;
            case TriggerActionType.BlockTrigger_02:
                EventBus.BlockTrigger02();
                break;
            case TriggerActionType.BlockTrigger_03:
                EventBus.BlockTrigger03();
                break;
            case TriggerActionType.BlockTrigger_04:
                EventBus.BlockTrigger04();
                break;
            case TriggerActionType.BlockTrigger_05:
                EventBus.BlockTrigger05();
                break;
            case TriggerActionType.BlockTrigger_06:
                EventBus.BlockTrigger06();
                break;
            case TriggerActionType.BlockTrigger_07:
                EventBus.BlockTrigger07();
                break;
            case TriggerActionType.BlockTrigger_08:
                EventBus.BlockTrigger08();
                break;
            case TriggerActionType.BlockTrigger_09:
                EventBus.BlockTrigger09();
                break;   
            case TriggerActionType.BlockTrigger_10:
                EventBus.BlockTrigger10();
                break;      
            case TriggerActionType.DestroyObject:
                Destroy(gameObject);
                break;
            

            default:
                Debug.LogWarning("Unhandled trigger action: " + actionType);
                break;
        }

        if (destroyAfterTrigger && actionType != TriggerActionType.DestroyObject)
            Destroy(gameObject);
    }
}
