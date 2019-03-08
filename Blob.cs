using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public BlobContainer Container;
    public bool Active = true;
    public float Radius = 1f;
    private float lastRadius = 1f;

    [HideInInspector]
    public float Factor;

    // Start is called before the first frame update
    void Start()
    {
        Factor = Radius * Radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            Container.NeedsUpdate = true;
            transform.hasChanged = false;
        }
        if (Radius != lastRadius)
        {
            lastRadius = Radius;
            Factor = Radius * Radius;
            Container.NeedsUpdate = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, Radius*Container.transform.localScale.x);
    }
}
