using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[System.Serializable]
public class LOS : MonoBehaviour
{
    [Header("Detection Properties")]
    public Collider2D CollidesWith;
    public ContactFilter2D ContactFilter;
    public List<Collider2D> ColliderList;

    private PolygonCollider2D LOSCollider;

    // Start is called before the first frame update
    void Start()
    {
        LOSCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.GetContacts(LOSCollider, ContactFilter, ColliderList);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       CollidesWith = collision;
    }
}
