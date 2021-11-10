using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform Player;
    public Transform PlayerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Player.position = PlayerSpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
