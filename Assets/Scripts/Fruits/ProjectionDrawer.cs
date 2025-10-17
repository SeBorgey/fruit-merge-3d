using UnityEngine;

public class ProjectionDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _linePoints = 16;
    [SerializeField] private float _timeBetweenPoints = 0.02f;

    public void DrawProjection(Vector3 velocity)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = Mathf.CeilToInt(_linePoints / _timeBetweenPoints) + 1;

        Vector3 pos = transform.position;

        for (int i = 0; i < _linePoints; i ++)
        {
            float time = i * _timeBetweenPoints;
            Vector3 point = pos + time * velocity;
            point.y = pos.y + velocity.y * time + (Physics.gravity.y / 2f * time * time);

            _lineRenderer.SetPosition(i, point);
        }
    }
}
