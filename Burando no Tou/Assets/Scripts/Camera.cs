using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 defaultPosition = new Vector3(0,0,-10);
    public GameObject player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + defaultPosition;
    }
}