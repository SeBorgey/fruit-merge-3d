using System.Collections;
using UnityEngine;

public class SineSizeAnimation : MonoBehaviour
{
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private float _targetScale;
    [SerializeField] private bool _animateOnStart;

    public Transform AnimatedObject;
    private Coroutine _animationCoroutine;

    private void OnEnable()
    {
        if (_animateOnStart)
            Animate(true);
    }

    private void OnDisable()
    {
        _animationCoroutine = null;
    }

    public void Animate(bool start)
    {
        AnimatedObject.gameObject.SetActive(start);

        if (start)
            _animationCoroutine ??= StartCoroutine(AnimationCoroutine());
        else if(_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
    }

    public IEnumerator AnimationCoroutine()
    {
        Vector3 originalScale = AnimatedObject.localScale;
        Vector3 scaleDiff = (Vector3.one * _targetScale) - originalScale;

        while (true)
        {
            AnimatedObject.localScale = originalScale + (scaleDiff * ((1 + Mathf.Sin(Time.time * _animationSpeed)) / 2));
            yield return null;
        }
    }
}
