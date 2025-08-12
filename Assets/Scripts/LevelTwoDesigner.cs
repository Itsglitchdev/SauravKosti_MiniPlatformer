using System;
using System.Collections;
using UnityEngine;

public class LevelTwoDesigner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject blockMoveable;
    [SerializeField] private GameObject pushBlock;
    [SerializeField] private GameObject pushBlock2;
    [SerializeField] private GameObject pushBlock3;
    [SerializeField] private GameObject pushBlock4;
    [SerializeField] private GameObject stackBlock1;
    [SerializeField] private GameObject stackBlock2;
    [SerializeField] private GameObject stackBlock3;
    [SerializeField] private GameObject[] swingBlocks;

    void OnEnable()
    {
        EventBus.OnBlockTrigger11 += BlockTrigger11;
        EventBus.OnBlockTrigger12 += BlockTrigger12;
        EventBus.OnBlockTrigger13 += BlockTrigger13;
        EventBus.OnBlockTrigger14 += BlockTrigger14;
        EventBus.OnBlockTrigger15 += BlockTrigger15;
        EventBus.OnBlockTrigger16 += BlockTrigger16;
        EventBus.OnBlockTrigger17 += BlockTrigger17;
        EventBus.OnBlockTrigger18 += BlockTrigger18;
        EventBus.OnBlockTrigger19 += BlockTrigger19;
    }

    void OnDisable()
    {
        EventBus.OnBlockTrigger11 -= BlockTrigger11;
        EventBus.OnBlockTrigger12 -= BlockTrigger12;
        EventBus.OnBlockTrigger13 -= BlockTrigger13;
        EventBus.OnBlockTrigger14 -= BlockTrigger14;
        EventBus.OnBlockTrigger15 -= BlockTrigger15;
        EventBus.OnBlockTrigger16 -= BlockTrigger16;
        EventBus.OnBlockTrigger17 -= BlockTrigger17;
        EventBus.OnBlockTrigger18 -= BlockTrigger18;
        EventBus.OnBlockTrigger19 -= BlockTrigger19;
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


    void BlockTrigger12()
    {
        StartCoroutine(PushBlockCoroutine(pushBlock));
    }
    void BlockTrigger13()
    {
        StartCoroutine(PushBlockCoroutine(pushBlock2));
    }
    void BlockTrigger14()
    {
        StartCoroutine(PushBlockCoroutine(pushBlock3));
    }
    void BlockTrigger15()
    {
        StartCoroutine(PushBlockCoroutine(pushBlock4));
    }
    IEnumerator PushBlockCoroutine(GameObject blockToPush)
    {
        if (blockToPush == null) yield break;

        yield return new WaitForSeconds(0.5f);

        Vector3 start = blockToPush.transform.position;
        Vector3 target = start + new Vector3(0f, 20f, 0f);

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            blockToPush.transform.position = Vector3.Lerp(start, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blockToPush.transform.position = target;

        target = blockToPush.transform.position + new Vector3(0f, -20f, 0f);
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            blockToPush.transform.position = Vector3.Lerp(target, start, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blockToPush.transform.position = start;
    }


    void BlockTrigger16()
    {
        StartCoroutine(StackMoveCoroutine(stackBlock1, -25f));
    }
    void BlockTrigger17()
    {
        StartCoroutine(StackMoveCoroutine(stackBlock2, -19f));
    }
    void BlockTrigger18()
    {
        StartCoroutine(StackMoveCoroutine(stackBlock3, -13f));
    }
    IEnumerator StackMoveCoroutine(GameObject block, float moveXAmount)
    {
        if (block == null) yield break;

        Vector3 start = block.transform.position;
        Vector3 target = start + new Vector3(moveXAmount, 0f, 0f);

        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            block.transform.position = Vector3.Lerp(start, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        block.transform.position = target;
    }


    void BlockTrigger19()
    {
        StartCoroutine(SwingBlocksCoroutine());
    }
    IEnumerator SwingBlocksCoroutine()
    {
        float minYScale = 8f;
        float maxYScale = 48f;
        float duration = 2f;

        bool[] isScalingUp = new bool[swingBlocks.Length];

        for (int i = 0; i < swingBlocks.Length; i++)
        {
            if (swingBlocks[i] == null) continue;

            Vector3 scale = swingBlocks[i].transform.localScale;

            float startY = (i % 2 == 0) ? maxYScale : minYScale;
            swingBlocks[i].transform.localScale = new Vector3(scale.x, startY, scale.z);

            isScalingUp[i] = (i % 2 != 0);
        }

        while (true)
        {
            float elapsed = 0f;

            Vector3[] startScales = new Vector3[swingBlocks.Length];
            Vector3[] targetScales = new Vector3[swingBlocks.Length];
            Vector3[] startPositions = new Vector3[swingBlocks.Length];
            Vector3[] targetPositions = new Vector3[swingBlocks.Length];

            for (int i = 0; i < swingBlocks.Length; i++)
            {
                if (swingBlocks[i] == null) continue;

                Vector3 currentScale = swingBlocks[i].transform.localScale;
                startScales[i] = currentScale;
                startPositions[i] = swingBlocks[i].transform.position;

                float targetYScale = isScalingUp[i] ? maxYScale : minYScale;
                float deltaY = targetYScale - currentScale.y;

                Vector3 offset = new Vector3(0f, deltaY / 2f, 0f);
                targetPositions[i] = startPositions[i] + offset;

                targetScales[i] = new Vector3(currentScale.x, targetYScale, currentScale.z);
            }

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                for (int i = 0; i < swingBlocks.Length; i++)
                {
                    if (swingBlocks[i] == null) continue;

                    swingBlocks[i].transform.localScale = Vector3.Lerp(startScales[i], targetScales[i], t);
                    swingBlocks[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < swingBlocks.Length; i++)
            {
                if (swingBlocks[i] == null) continue;

                swingBlocks[i].transform.localScale = targetScales[i];
                swingBlocks[i].transform.position = targetPositions[i];
                isScalingUp[i] = !isScalingUp[i];
            }
        }
    }

}