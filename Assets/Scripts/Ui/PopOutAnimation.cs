using System.Collections;
using UnityEngine;

public class PopOutAnimation : MonoBehaviour
{
    [SerializeField] private Transform _animatedObject;
    [SerializeField] private bool _playOnEnable;
    [SerializeField] private AnimationCurve _popCurve;
    [SerializeField] private float _popDuration = 0.25f;

    private Coroutine _popCoroutine;
    private Vector3 _originalScale;

    private void OnEnable()
    {
        if(_originalScale == Vector3.zero)
            _originalScale = _animatedObject.localScale;

        if (_playOnEnable)
        {
            Animate();
        }
    }

    public void Animate()
    {
        if (_popCoroutine != null) StopCoroutine(_popCoroutine);
        _popCoroutine = StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        float progress = 0;

        while (progress < 1f) 
        {
            progress += Time.unscaledDeltaTime / _popDuration;
            float multiplier = _popCurve.Evaluate(progress);
            _animatedObject.localScale = _originalScale * multiplier;
            yield return null;
        }

        _animatedObject.localScale = _originalScale;
        _popCoroutine = null;
    }
}
