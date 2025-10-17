using System.Collections;
using UnityEngine;

public class UIScaleAnimation : MonoBehaviour
{
    [SerializeField] private bool _autoStart;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _cycleDelay = 1f;
    [SerializeField] private AnimationCurve _curveX;

    private Coroutine _animation;

    private void OnEnable()
    {
        if (_autoStart)
            Play();
    }

    public void Play()
    {
        Stop();
        _animation = StartCoroutine(Working());
    }

    public void Stop()
    {
        if (_animation != null)
        {
            StopCoroutine(_animation);
            _animation = null;
        }
    }

    private IEnumerator Working()
    {
        while (true)
        {
            var progress = 0f;
            while (progress < 1f)
            {
                transform.localScale = new Vector3(
                    _curveX.Evaluate(progress),
                    _curveX.Evaluate(progress),
                    1f);
                progress += Time.unscaledDeltaTime / _duration;
                yield return null;
            }
            progress = 1f;
            transform.localScale = new Vector3(
                _curveX.Evaluate(progress),
                _curveX.Evaluate(progress),
                1f);
            yield return new WaitForSeconds(_cycleDelay);
        }
    }
}
