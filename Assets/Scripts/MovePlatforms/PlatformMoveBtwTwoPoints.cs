using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveBtwTwoPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> platformPos;
    [SerializeField] private Transform platform;
    [SerializeField, Range(0.1f, 2f)] private float speed;
    [SerializeField, Range(0.5f, 2f)] private float delayTime;

    private int _i = 0;
    private float _delayTime;

    private void Start()
    {
        _delayTime = delayTime;
    }

    private void FixedUpdate()
    {
        if (platform.position != platformPos[_i].position)
        {
            platform.position = Vector3.MoveTowards(platform.position, platformPos[_i].position, Time.deltaTime * speed);
        }
        if (platform.position == platformPos[_i].position)
        {
            delayTime -= Time.deltaTime;
            if (delayTime < 0)
            {
                MoveToNextPoint();
                delayTime = _delayTime;
            }
        }
    }

    private void MoveToNextPoint()
    {
        if (_i < platformPos.Count - 1)
            _i++;
        else
            _i = 0;
    }
}
