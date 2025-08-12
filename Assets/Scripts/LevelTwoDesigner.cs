using System.Collections;
using UnityEngine;

public class LevelTwoDesigner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject blockMoveable;

    void OnEnable()
    {
        EventBus.OnBlockTrigger11 += BlockTrigger11;
    }

    void OnDisable()
    {
        EventBus.OnBlockTrigger11 -= BlockTrigger11;
    }

    void BlockTrigger11()
    {
        StartCoroutine(BlockMoveableCoroutine());
    }

    IEnumerator BlockMoveableCoroutine()
    {
        yield return new WaitForSeconds(3f);

        if (blockMoveable == null) yield break;
        
        Vector3 start = blockMoveable.transform.position;
        Vector3 target = start + new Vector3(20f, 0f, 0f);

        float duration = 2.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            blockMoveable.transform.position = Vector3.Lerp(start, target, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        blockMoveable.transform.position = target;
    }

}