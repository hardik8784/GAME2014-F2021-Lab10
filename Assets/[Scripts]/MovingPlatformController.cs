using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    public MovingPlatformDirection Direction;
    [Range(0.1f,10.0f)]
    public float Speed;
    [Range(1,20)]
    public float Distance;
    [Range(0.05f, 0.1f)]
    public float DistanceOffSet;
    public bool isLooping;

    private Vector2 StartingPosition;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        if(isLooping)
        {
            isMoving = true;
        }
    }

    private void MovePlatform()
    {
        float PingPongValue = (isMoving) ? Mathf.PingPong(Time.time * Speed, Distance) : Distance;

        //if(Mathf.Approximately(PingPongValue, Distance - DistanceOffSet))
        if ((!isLooping) && (PingPongValue >=  Distance - DistanceOffSet))
        {
            isMoving = false;
            //Debug.Log("True");
        }

        //Debug.Log("PingPongValue : " + PingPongValue);
        switch (Direction)
        {
            case MovingPlatformDirection.HORIZONTAL:
                transform.position = new Vector2(StartingPosition.x + PingPongValue, transform.position.y);
                break;
            case MovingPlatformDirection.VERTICAL:
                transform.position = new Vector2(transform.position.x,StartingPosition.y + PingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(StartingPosition.x + PingPongValue, StartingPosition.y + PingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(StartingPosition.x + PingPongValue, StartingPosition.y - PingPongValue);
                break;
        }

       
    }
}
