using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPatrol : Blob
{
    public bool ScaleTime = false;
    public float Speed = 1f;
    public Vector3 Target;
    private Vector3 origin;
    private float startTime;

    protected override void Start()
    {
        base.Start();
        startTime = Time.time;
        origin = transform.position;
    }

    protected override void Update()
    {
        float t = ScaleTime ? Speed * ((Mathf.Sin(Time.time - startTime) + 1) / 2) : Speed * Mathf.Sin(Time.time - startTime);
        transform.position = Vector3.Lerp(origin, Target, t);

        base.Update();
    }
}
