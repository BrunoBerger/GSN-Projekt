using System;
using DefaultNamespace;
using UnityEngine;

public class ThePoLice : MonoBehaviour
{
    public Vector3 lightOffset = Vector3.left;
    public MirrorAxis mirrorAxis = MirrorAxis.X;
    public Vector3 rotationStart = new Vector3(15, 90, 0);
    public Vector3 rotationStop = new Vector3(15, -90, 0);
    public float timeToRotate = 1.0f;
    public float lightIntensity = 1.0f;
    public Color leftColor = Color.red;
    public Color rightColor = Color.blue;

    private Vector3 _rotationDifference;
    private bool _mirrored;
    private Light _lightSource;


    // Start is called before the first frame update
    void Start()
    {
        _rotationDifference = rotationStop - rotationStart;
        _mirrored = false;
        _lightSource = GetComponentInChildren<Light>();
        _lightSource.intensity = lightIntensity;
        _lightSource.transform.SetPositionAndRotation(transform.position + lightOffset, Quaternion.Euler(rotationStart));
        _lightSource.color = leftColor;
    }

    // Update is called once per frame
    void Update()
    {
        var completeAnimationTime = 2 * timeToRotate;
        var currentAnimationTime = Time.time / completeAnimationTime;
        currentAnimationTime = (currentAnimationTime - MathF.Floor(currentAnimationTime)) * completeAnimationTime;
        var normalizedCurrentAnimationTime = Time.time / timeToRotate;
        normalizedCurrentAnimationTime -= MathF.Floor(normalizedCurrentAnimationTime);
        if (currentAnimationTime >= timeToRotate && !_mirrored)
        {
            // mirror
            var mirroredOffset = lightOffset;
            switch (mirrorAxis)
            {
                case MirrorAxis.X:
                    mirroredOffset.x *= -1;
                    break;
                case MirrorAxis.Y:
                    mirroredOffset.y *= -1;
                    break;
                case MirrorAxis.Z:
                    mirroredOffset.z *= -1;
                    break;
            }
            _lightSource.transform.SetPositionAndRotation(transform.position + mirroredOffset,
                Quaternion.Euler(rotationStart));
            _lightSource.transform.Rotate(normalizedCurrentAnimationTime * _rotationDifference);
            _lightSource.color = rightColor;
            _mirrored = true;
        }
        else if (currentAnimationTime < timeToRotate && _mirrored)
        {
            // mirror back
            _lightSource.transform.SetPositionAndRotation(transform.position + lightOffset,
                Quaternion.Euler(rotationStart));
            _lightSource.transform.Rotate(normalizedCurrentAnimationTime * _rotationDifference);
            _lightSource.color = leftColor;
            _mirrored = false;
        }
        else
        {
            _lightSource.transform.Rotate(_rotationDifference * (Time.deltaTime / timeToRotate));
        }
        _lightSource.intensity = lightIntensity * Mathf.Sin(normalizedCurrentAnimationTime * Mathf.PI);
    }
}
