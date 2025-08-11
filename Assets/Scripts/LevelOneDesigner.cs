using System.Collections;
using UnityEngine;

public class LevelOneDesigner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] stairs;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject sizeBlock;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject spike2;
    [SerializeField] private GameObject spike3;
    [SerializeField] private GameObject block;

    [Header("Blocks Settings")]
    private float fallDistance = 6f;
    private float fallSpeed = 10f;


    private void OnEnable()
    {
        EventBus.OnBlockTrigger01 += BlocksFalling;
        EventBus.OnBlockTrigger02 += SizeBlockEffect;
        EventBus.OnSpikeTrigger01 += SpikePositionGoesFar;
        EventBus.OnSpikeTrigger02 += SpikePositionComesClose;
        EventBus.OnSpikeTrigger03 += SpikePositionDanger;
        EventBus.OnBlockTrigger03 += BlockMoveAndTryFall;
    }

    private void OnDisable()
    {
        EventBus.OnBlockTrigger01 -= BlocksFalling;
        EventBus.OnBlockTrigger02 -= SizeBlockEffect;
        EventBus.OnSpikeTrigger01 -= SpikePositionGoesFar;
        EventBus.OnSpikeTrigger02 -= SpikePositionComesClose;
        EventBus.OnSpikeTrigger03 -= SpikePositionDanger;
        EventBus.OnBlockTrigger03 -= BlockMoveAndTryFall;
    }

    void Start()
    {
        StartCoroutine(StairsBlinking());
    }

    void EnabledAllStairs()
    {
        for (int i = 0; i < stairs.Length; i++)
        {
            if (stairs[i] != null)
                stairs[i].SetActive(true);
        }
    }

    IEnumerator StairsBlinking()
    {
        EnabledAllStairs();

        int index = 0;

        while (true)
        {
            for (int i = 0; i < stairs.Length; i++)
            {
                if (stairs[i] != null)
                    stairs[i].SetActive(true);
            }

            if (stairs[index] != null)
                stairs[index].SetActive(false);

            yield return new WaitForSeconds(3f);

            index = (index + 1) % stairs.Length;
        }
    }

    void BlocksFalling()
    {
        StartCoroutine(BlocksFallDown());
    }

    IEnumerator BlocksFallDown()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject block in blocks)
        {
            if (block == null) continue;

            Vector3 startPos = block.transform.position;
            Vector3 targetPos = startPos - new Vector3(0f, fallDistance, 0f);

            while (Vector3.Distance(block.transform.position, targetPos) > 0.01f)
            {
                block.transform.position = Vector3.MoveTowards(
                    block.transform.position,
                    targetPos,
                    fallSpeed * Time.deltaTime
                );

                yield return null;
            }

            block.transform.position = targetPos;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SizeBlockEffect()
    {
        StartCoroutine(ShrinkAndSlideBlock());
    }

    IEnumerator ShrinkAndSlideBlock()
    {
        if (sizeBlock == null) yield break;

        float duration = 5f;
        float moveSpeed = 4f;

        Vector3 originalScale = sizeBlock.transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x / 4.5f, originalScale.y, originalScale.z);

        Vector3 originalPosition = sizeBlock.transform.position;
        float widthReduction = originalScale.x - targetScale.x;

        float elapsed = 0f;

        // Scale down from left side
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newScaleX = Mathf.Lerp(originalScale.x, targetScale.x, t);

            sizeBlock.transform.localScale = new Vector3(newScaleX, originalScale.y, originalScale.z);

            // Shift position right to keep left side anchored
            float shift = (originalScale.x - newScaleX) / 2f;
            sizeBlock.transform.position = new Vector3(
                originalPosition.x + shift,
                originalPosition.y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        sizeBlock.transform.localScale = targetScale;
        sizeBlock.transform.position = new Vector3(
            originalPosition.x + widthReduction / 2f,
            originalPosition.y,
            originalPosition.z
        );

        Vector3 moveTarget = sizeBlock.transform.position + new Vector3(-15f, 0f, 0f);

        while (Vector3.Distance(sizeBlock.transform.position, moveTarget) > 0.01f)
        {
            sizeBlock.transform.position = Vector3.MoveTowards(
                sizeBlock.transform.position,
                moveTarget,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        sizeBlock.transform.position = moveTarget;
    }

    void SpikePositionGoesFar()
    {
        StartCoroutine(SpikePosGoFar());
    }

    IEnumerator SpikePosGoFar()
    {
        if (spike == null) yield break;

        Vector3 start = spike.transform.position;
        Vector3 target = start + new Vector3(4f, 0f, 0f);
        float speed = 7.5f;

        while (Vector3.Distance(spike.transform.position, target) > 0.01f)
        {
            spike.transform.position = Vector3.MoveTowards(
                spike.transform.position,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        spike.transform.position = target;
    }

    void SpikePositionComesClose()
    {
        StartCoroutine(SpikePosComeClose());
    }

    IEnumerator SpikePosComeClose()
    {
        if (spike2 == null) yield break;

        Vector3 start = spike2.transform.position;
        Vector3 target = start + new Vector3(-4f, 0f, 0f);
        float speed = 7.5f;

        while (Vector3.Distance(spike2.transform.position, target) > 0.01f)
        {
            spike2.transform.position = Vector3.MoveTowards(
                spike2.transform.position,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        spike2.transform.position = target;
    }

    void SpikePositionDanger()
    {
        StartCoroutine(SpikePosDanger());
    }

    IEnumerator SpikePosDanger()
    {
        if (spike3 == null) yield break;

        Vector3 start = spike3.transform.position;
        Vector3 target = start + new Vector3(-7f, 0f, 0f);
        float speed = 5f;

        while (Vector3.Distance(spike3.transform.position, target) > 0.01f)
        {
            spike3.transform.position = Vector3.MoveTowards(
                spike3.transform.position,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        spike3.transform.position = target;
    }

    private void BlockMoveAndTryFall()
    {
        StartCoroutine(SpecialBlockMovement());
    }

    IEnumerator SpecialBlockMovement()
    {
        if (block == null) yield break;

        float speed = 10f;

        Vector3 startPos = block.transform.position;
        Vector3 upPos = startPos + new Vector3(0f, 10f, 0f);
        Vector3 leftPos = upPos + new Vector3(-3.75f, 0f, 0f);
        Vector3 downPos = leftPos + new Vector3(0f, -10f, 0f);

        // Move up
        while (Vector3.Distance(block.transform.position, upPos) > 0.01f)
        {
            block.transform.position = Vector3.MoveTowards(block.transform.position, upPos, speed * Time.deltaTime);
            yield return null;
        }

        // Move left
        while (Vector3.Distance(block.transform.position, leftPos) > 0.01f)
        {
            block.transform.position = Vector3.MoveTowards(block.transform.position, leftPos, speed * Time.deltaTime);
            yield return null;
        }

        // Move down
        while (Vector3.Distance(block.transform.position, downPos) > 0.01f)
        {
            block.transform.position = Vector3.MoveTowards(block.transform.position, downPos, speed * Time.deltaTime);
            yield return null;
        }

        // Final correction
        block.transform.position = downPos;
    }


}
