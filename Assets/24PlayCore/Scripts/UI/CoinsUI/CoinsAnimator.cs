using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Lean.Pool;

public class CoinsAnimator : MonoSingleton<CoinsAnimator>
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Image coinPrefab;
    [SerializeField]
    private float delayBeforeAnimate;
    [Header("PopUp")]
    [SerializeField]
    private float popUpRadius;
    [SerializeField]
    private float popUpTime;
    [SerializeField]
    private Ease popUpEase;
    [Header("Move")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveDelay;
    [SerializeField]
    private Ease moveEase;
    [Header("Scale")]
    [SerializeField]
    private Vector2 pupUpScaleFromTo;
    [SerializeField]
    private Ease popIpScaleEase;

    public void Animate(Vector3 spawnPosition, System.Action onComplete)
    {
        StartCoroutine(AnimateRoutine(10, spawnPosition, onComplete));
    }

    private IEnumerator AnimateRoutine(int count, Vector3 spawnPosition, System.Action onComplete)
    {
        yield return new WaitForSecondsRealtime(delayBeforeAnimate);
        var coinsIEnumerators = new IEnumerator[count];
        for (int i = 0; i < count; i++)
        {
            var coin = LeanPool.Spawn(coinPrefab, target);
            coin.transform.position = spawnPosition;
            coinsIEnumerators[i] = ProcessCoinRoutine(coin, spawnPosition, i + 1);
        }
        yield return this.WaitAll(coinsIEnumerators);
        onComplete?.Invoke();
    }

    private IEnumerator ProcessCoinRoutine(Image coin, Vector3 spawnPosition, int waitFrames)
    {
        for (int i = 0; i < waitFrames; i++)
            yield return null;

        yield return PopUpCoinRoutine(coin, spawnPosition);
        yield return MoveToDestinationRoutine(coin);
        LeanPool.Despawn(coin);
    }

    private IEnumerator PopUpCoinRoutine(Image coin, Vector3 spawnPosition)
    {
        var endPosition = spawnPosition + (Vector3)Random.insideUnitCircle * popUpRadius * GetScreenWidthCoefficient();
        coin.transform.DOMove(endPosition, popUpTime).From(spawnPosition).SetEase(popUpEase);
        yield return new WaitForSeconds(popUpTime);
        coin.transform.DOScale(pupUpScaleFromTo.y, 2f * popUpTime).From(pupUpScaleFromTo.x).SetEase(popIpScaleEase);
    }

    private IEnumerator MoveToDestinationRoutine(Image coin)
    {
        var distance = Vector3.Distance(coin.transform.position, target.position);
        var moveTime = distance / (moveSpeed * GetScreenWidthCoefficient());
        coin.transform.DOMove(target.position, moveTime).SetEase(moveEase).SetDelay(moveDelay);
        yield return new WaitForSeconds(moveDelay + moveTime);
    }

    private float GetScreenWidthCoefficient()
    {
        return (float)Screen.width / 1080;
    }
}
