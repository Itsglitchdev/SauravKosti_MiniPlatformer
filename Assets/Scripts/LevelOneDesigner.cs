using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelOneDesigner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] stairsOfBlocks;
    [SerializeField] private GameObject[] blockss;
    [SerializeField] private GameObject blockFall1;
    [SerializeField] private GameObject blockSizeAndShrink2;
    [SerializeField] private GameObject spike1;
    [SerializeField] private GameObject spike2;
    [SerializeField] private GameObject spike3;
    [SerializeField] private GameObject block1;
    [SerializeField] private GameObject block2;
    [SerializeField] private GameObject block3;


    private void OnEnable()
    {
        EventBus.OnBlockTrigger01 += BlockTrigger01;
        EventBus.OnBlockTrigger02 += BlockTrigger02;
        EventBus.OnBlockTrigger03 += BlockTrigger03;
        EventBus.OnBlockTrigger04 += BlockTrigger04;
        EventBus.OnBlockTrigger05 += BlockTrigger05;
        EventBus.OnBlockTrigger06 += BlockTrigger06;
        EventBus.OnBlockTrigger07 += BlockTrigger07;
        EventBus.OnBlockTrigger08 += BlockTrigger08;
        EventBus.OnBlockTrigger09 += BlockTrigger09;
        EventBus.OnLevelOneCompleted += LevelOneCompleted;
    }

    private void OnDisable()
    {
        EventBus.OnBlockTrigger01 -= BlockTrigger01;
        EventBus.OnBlockTrigger02 -= BlockTrigger02;
        EventBus.OnBlockTrigger03 -= BlockTrigger03;
        EventBus.OnBlockTrigger04 -= BlockTrigger04;
        EventBus.OnBlockTrigger05 -= BlockTrigger05;
        EventBus.OnBlockTrigger06 -= BlockTrigger06;
        EventBus.OnBlockTrigger07 -= BlockTrigger07;
        EventBus.OnBlockTrigger08 -= BlockTrigger08;
        EventBus.OnBlockTrigger09 -= BlockTrigger09;
        EventBus.OnLevelOneCompleted -= LevelOneCompleted;
    }


    void BlockTrigger01()
    {
        StartCoroutine(BlockFall01Coroutine());
    }
    IEnumerator BlockFall01Coroutine()
    {
        if (blockFall1 == null) yield break;

        Vector3 startPos = blockFall1.transform.position;
        Vector3 endPos = new Vector3(startPos.x, -80f, startPos.z);

        float fallDuration = 2.5f;
        float elapsed = 0f;

        while (elapsed < fallDuration)
        {
            float t = elapsed / fallDuration;
            blockFall1.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blockFall1.transform.position = endPos;
        Destroy(blockFall1);
    }


    void BlockTrigger02()
    {
        StartCoroutine(BlockThroughStairsBlinking());
    }
    IEnumerator BlockThroughStairsBlinking()
    {
        for (int i = 0; i < stairsOfBlocks.Length; i++)
        {
            if (stairsOfBlocks[i] != null)
                stairsOfBlocks[i].SetActive(true);
        }

        int index = 0;

        while (true)
        {
            for (int i = 0; i < stairsOfBlocks.Length; i++)
            {
                if (stairsOfBlocks[i] != null)
                    stairsOfBlocks[i].SetActive(true);
            }

            if (stairsOfBlocks[index] != null)
                stairsOfBlocks[index].SetActive(false);

            yield return new WaitForSeconds(3f);

            index = (index + 1) % stairsOfBlocks.Length;
        }
    }


    void BlockTrigger03()
    {
        StartCoroutine(BlocksFallingDowns());
    }
    IEnumerator BlocksFallingDowns()
    {
        yield return new WaitForSeconds(0.40f);

        foreach (GameObject block in blockss)
        {
            if (block == null) continue;

            float fallDistance = 10f;
            float fallSpeed = 0.45f;

            Vector3 startPos = block.transform.position;
            Vector3 targetPos = startPos - new Vector3(0f, fallDistance, 0f);
            float elapsed = 0f;

            while (elapsed < fallSpeed)
            {
                float t = elapsed / fallSpeed;
                block.transform.position = Vector3.Lerp(startPos, targetPos, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            block.transform.position = targetPos;
            yield return new WaitForSeconds(0.1f);
        }
    }


    void BlockTrigger04()
    {
        StartCoroutine(BlockShrinkAndMoveBlock());
    }
    IEnumerator BlockShrinkAndMoveBlock()
    {
        if (blockSizeAndShrink2 == null) yield break;


        Vector3 originalScale = blockSizeAndShrink2.transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x / 4.5f, originalScale.y, originalScale.z);

        Vector3 originalPosition = blockSizeAndShrink2.transform.position;
        float widthReduction = originalScale.x - targetScale.x;

        float duration = 5f;
        float elapsed = 0f;

        // Shrink block
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newScaleX = Mathf.Lerp(originalScale.x, targetScale.x, t);

            blockSizeAndShrink2.transform.localScale = new Vector3(newScaleX, originalScale.y, originalScale.z);

            // Keep left side anchored
            float shift = (originalScale.x - newScaleX) / 2f;
            blockSizeAndShrink2.transform.position = new Vector3(
                originalPosition.x + shift,
                originalPosition.y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        blockSizeAndShrink2.transform.localScale = targetScale;
        blockSizeAndShrink2.transform.position = new Vector3(
            originalPosition.x + widthReduction / 2f,
            originalPosition.y,
            originalPosition.z
        );

        // Move block left using Lerp
        Vector3 moveStart = blockSizeAndShrink2.transform.position;
        Vector3 moveTarget = moveStart + new Vector3(-15f, 0f, 0f);
        float moveDuration = 0.25f;
        elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            blockSizeAndShrink2.transform.position = Vector3.Lerp(moveStart, moveTarget, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blockSizeAndShrink2.transform.position = moveTarget;
    }


    void BlockTrigger05()
    {
        StartCoroutine(BlockThroughSpikePosGoFar());
    }
    IEnumerator BlockThroughSpikePosGoFar()
    {
        if (spike1 == null) yield break;

        Vector3 start = spike1.transform.position;
        Vector3 target = start + new Vector3(4f, 0f, 0f);

        float duration = 0.25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            spike1.transform.position = Vector3.Lerp(start, target, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        spike1.transform.position = target;
    }


    void BlockTrigger06()
    {
        StartCoroutine(BlockThroughSpikeComeClose());
    }
    IEnumerator BlockThroughSpikeComeClose()
    {
        if (spike2 == null) yield break;

        Vector3 start = spike2.transform.position;
        Vector3 target = start + new Vector3(-4f, 0f, 0f);

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            spike2.transform.position = Vector3.Lerp(start, target, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        spike2.transform.position = target;
    }


    void BlockTrigger07()
    {
        StartCoroutine(BlockThroughSpikePosDanger());
    }
    IEnumerator BlockThroughSpikePosDanger()
    {
        if (spike3 == null) yield break;

        Vector3 start = spike3.transform.position;
        Vector3 target = start + new Vector3(-7f, 0f, 0f);

        float duration = 0.75f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            spike3.transform.position = Vector3.Lerp(start, target, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        spike3.transform.position = target;
    }


    private void BlockTrigger08()
    {
        StartCoroutine(BlockPushBackAndMove());
    }
    IEnumerator BlockPushBackAndMove()
    {
        if (block1 == null) yield break;

        Vector3 startPos = block1.transform.position;
        Vector3 upPos = startPos + new Vector3(0f, 10f, 0f);
        Vector3 leftPos = upPos + new Vector3(-3.75f, 0f, 0f);
        Vector3 downPos = leftPos + new Vector3(0f, -10f, 0f);

        float elapsed, duration;

        // Move Up
        elapsed = 0f;
        duration = 0.5f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            block1.transform.position = Vector3.Lerp(startPos, upPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        block1.transform.position = upPos;

        // Move Left
        elapsed = 0f;
        duration = 0.25f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            block1.transform.position = Vector3.Lerp(upPos, leftPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        block1.transform.position = leftPos;

        // Move Down
        elapsed = 0f;
        duration = 0.5f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            block1.transform.position = Vector3.Lerp(leftPos, downPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        block1.transform.position = downPos;
    }


    private void BlockTrigger09()
    {
        StartCoroutine(BlockPushBackAndMoveAnotherOne());
    }
    IEnumerator BlockPushBackAndMoveAnotherOne()
    {

        // Block2: Move +5 in X 
        if (block2 != null)
        {
            Vector3 start = block2.transform.position;
            Vector3 target = start + new Vector3(5f, 0f, 0f);
            float elapsed = 0f;
            float duration = 0.5f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                block2.transform.position = Vector3.Lerp(start, target, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            block2.transform.position = target;
        }

        // Block3: Move +10 in Y, then +5 in X, then -10 in Y
        if (block3 != null)
        {
            Vector3 start = block3.transform.position;
            Vector3 up = start + new Vector3(0f, 10f, 0f);
            Vector3 right = up + new Vector3(5f, 0f, 0f);
            Vector3 down = right + new Vector3(0f, -10f, 0f);

            float elapsed, duration;

            // Move up
            elapsed = 0f;
            duration = 0.5f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                block3.transform.position = Vector3.Lerp(start, up, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            block3.transform.position = up;

            // Move right
            elapsed = 0f;
            duration = 0.5f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                block3.transform.position = Vector3.Lerp(up, right, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            block3.transform.position = right;

            // Move down
            elapsed = 0f;
            duration = 0.5f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                block3.transform.position = Vector3.Lerp(right, down, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
            block3.transform.position = down;
        }
    }

    private void LevelOneCompleted()
    {
        Debug.Log("Level 2 Loading");
    }


}
