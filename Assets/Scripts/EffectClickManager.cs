using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class EffectClickManager : MonoBehaviour
{
    [SerializeField] private GameObject effectHit, effecTextDamage;
    [SerializeField] private Vector3 offSetForEffects;
    [SerializeField] private float distance = 200;
    [SerializeField] private float durationMoveText = 1.6f;
    private PoolObject<GameObject> effectPool, effectTextPool;
    private const int countEffect = 5;
    private DamagePerClick _damagePerClick;
    
    public void Initialize(DamagePerClick damagePerClick)
    {
        _damagePerClick = damagePerClick;
        effectPool = new PoolObject<GameObject>(PreloadEffect, GetAction, ReturnAction, countEffect);
        effectTextPool = new PoolObject<GameObject>(PreloadText, GetAction, ReturnAction, countEffect);
    }
    private void OnEnable()
    {
        DamagePerClick.OnClickHit += Spawn;
    }
    private void OnDisable()
    {
        DamagePerClick.OnClickHit -= Spawn;
    }
    private void Spawn(Vector3 point)
    {
        StartCoroutine(SpawnMove(point));
    }
    IEnumerator SpawnMove(Vector3 point)
    {
        GameObject effectHit = effectPool.Get();
        effectHit.transform.position = point + offSetForEffects;
        StartCoroutine(ReturnEffect(effectHit));

        GameObject effecTextDamage = effectTextPool.Get();
        effecTextDamage.GetComponent<RectTransform>().position = point + offSetForEffects;
        effecTextDamage.GetComponent<RectTransform>().localScale = Vector3.one;
        yield return new WaitForSeconds(.1f);
        effecTextDamage.GetComponent<RectTransform>().DOAnchorPosY(point.y + distance, durationMoveText - 0.1f).SetEase(Ease.InExpo);
        effecTextDamage.GetComponent<RectTransform>().DOScale(Vector3.zero, 1).SetEase(Ease.InFlash);
        effecTextDamage.GetComponent<TextMeshPro>().text = _damagePerClick.GetDamage().ToString("F0") + "+";
        StartCoroutine(ReturnText(effecTextDamage));
    }
    public GameObject PreloadEffect()
    {
        return Instantiate(effectHit);
    }
    public GameObject PreloadText()
    {
        return Instantiate(effecTextDamage);
    }
    public void GetAction(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void ReturnAction(GameObject obj)
    {
        obj.SetActive(false);
    }
    IEnumerator ReturnEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1);
        effectPool.Return(effect);
        //print("ReturnEffect");
    }
    IEnumerator ReturnText(GameObject text)
    {
        yield return new WaitForSeconds(durationMoveText);
        text.GetComponent<RectTransform>().position = Vector3.zero;
        effectTextPool.Return(text);
        //print("ReturnText");
    }
}
