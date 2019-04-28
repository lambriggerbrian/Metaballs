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
    protected virtual void Start()
    {
        Factor = Radius * Radius;
    }

    // Update is called once per frame
    protected virtual void Update()
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Container == null ? new Color(1, 0, 0, 0.5f) : new Color(1, 1, 1, 0.5f);
        float scale = Container == null ? 5 : Container.transform.localScale.x;
        Gizmos.DrawSphere(transform.position, Radius*scale);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.99f, 0.4f, 0);
        float scale = Container == null ? 5 : Container.transform.localScale.x;
        Gizmos.DrawWireSphere(transform.position, Radius*scale*1.3f);
    }
}
