using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float RunForce;
    public Transform LookAheadPoint;
    public Transform LookInFrontPoint;
    public LayerMask GroundLayerMask;
    public LayerMask WallLayerMask;
    public bool isGroundAhead;

    private Rigidbody2D RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAhead();
        LookInFront();
        MoveEnemy();
    }

    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, LookAheadPoint.position,GroundLayerMask);
        if(hit)
        {
            isGroundAhead = true;
            //Debug.Log("There is Ground Ahead");
        }
        else
        {
            isGroundAhead = false;
            //Debug.Log("No Ground Ahead");
        }
    }

    private void LookInFront()
    {
        var hit = Physics2D.Linecast(transform.position, LookInFrontPoint.position, WallLayerMask);
        if (hit)
        {
            Flip();
            //Debug.Log("Wall Ahead");
        }
    }

    private void MoveEnemy()
    {
       // transform.position += new Vector3(Speed * Direction * Time.deltaTime , 0.0f);
       if(isGroundAhead)
        {
            RigidBody.AddForce(Vector2.left * RunForce * transform.localScale.x);
            RigidBody.velocity *= 0.99f;
        }
       else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, LookAheadPoint.position);
        Gizmos.DrawLine(transform.position, LookInFrontPoint.position);
    }
}
