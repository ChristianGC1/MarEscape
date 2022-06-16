using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    Vector2 mousePosition;

    [Range(1, 5)]
    public float maxMouseDistance;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        mousePosition = Vector2.ClampMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position, maxMouseDistance);

        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }
}
