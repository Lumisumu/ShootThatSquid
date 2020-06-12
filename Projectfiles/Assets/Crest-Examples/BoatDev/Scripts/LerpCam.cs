using UnityEngine;

public class LerpCam : MonoBehaviour
{
    public float _lerpAlpha;
    public Transform _targetPos;
    public Transform _targetLookatPos;
    public float _lookatOffset;
    public float _minHeightAboveWater;

    void Update()
    {
        Vector3 targetPos = _targetPos.position;
        float h = 0f;
        if(Crest.OceanRenderer.Instance.CollisionProvider.SampleHeight(ref targetPos, ref h))
        {
            targetPos.y = Mathf.Max(targetPos.y, h + _minHeightAboveWater);
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, _lerpAlpha * Time.deltaTime * 60f);
        transform.LookAt(_targetLookatPos.position + _lookatOffset * Vector3.up);
	}
}
