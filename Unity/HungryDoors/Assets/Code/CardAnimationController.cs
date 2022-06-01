using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardAnimationController : MonoBehaviour
{
    public KeyCode key;
    public KeyCode useKey;
    public bool selected = false;
    Vector3 startPos = Vector3.zero;
    public GameObject particle;
    public Transform destroyPoint;
    void Start()
    {
        startPos = transform.localPosition;
    }

    public void SetCardSelected(bool isSelected)
    {
        if (selected != isSelected)
        {
            transform.DOLocalMove(isSelected ? startPos + Vector3.up : startPos, 0.1f).OnComplete(() => selected = isSelected);
        }
    }

    public void UseCard()
    {
        StartCoroutine(CardUsingEnumerator());
    }

    public IEnumerator CardUsingEnumerator()
    {
        var sequence = DOTween.Sequence();
        var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        sequence.Append(transform.DOMove(targetPos, 0.2f));
        sequence.Join(transform.DOLocalRotate(Vector3.zero, 0.1f));
        sequence.Join(transform.DOScale(Vector3.one * 0.75f, 0.2f));
        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        particle.SetActive(true);
        Vector3 startPos = transform.position;
        Vector3[] bezierPath = { startPos, transform.right * -5f, destroyPoint.position };
        var destroySequence = DOTween.Sequence();
        destroySequence.Append(transform.DOPath(bezierPath, 1f, PathType.CatmullRom).OnUpdate(() =>
        {
            var moveDirection = (startPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
            startPos = transform.position;
        }));
        destroySequence.Join(transform.DOScale(0.25f, 0.4f)).OnComplete(() => Destroy(gameObject));

    }
}
