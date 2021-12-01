using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Player.position + new Vector3(0.0f, 0.0f, -10.0f) ;
    }
}
