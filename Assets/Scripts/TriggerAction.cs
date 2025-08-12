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
            case TriggerActionType.LevelOneCompleted:
                EventBus.TriggerLevelOneCompleted();
                break;     
            case TriggerActionType.BlockTrigger_11:
                EventBus.BlockTrigger11();
                break;     
            case TriggerActionType.BlockTrigger_12:
                EventBus.BlockTrigger12();
                break;  
            case TriggerActionType.BlockTrigger_13:
                EventBus.BlockTrigger13();
                break; 
            case TriggerActionType.BlockTrigger_14:
                EventBus.BlockTrigger14();
                break;
            case TriggerActionType.BlockTrigger_15:
                EventBus.BlockTrigger15();
                break;       
            case TriggerActionType.BlockTrigger_16:
                EventBus.BlockTrigger16();
                break;
            case TriggerActionType.BlockTrigger_17:
                EventBus.BlockTrigger17();
                break;
            case TriggerActionType.BlockTrigger_18:
                EventBus.BlockTrigger18();
                break;
            case TriggerActionType.BlockTrigger_19:
                EventBus.BlockTrigger19();
                break;         
            case TriggerActionType.Respwan:
                EventBus.TriggerRespawn();
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
