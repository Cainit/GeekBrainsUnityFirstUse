using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform player;
    [SerializeField]float smoothSpeed = 0.1f;

    void Start()
    {
        player = PlayerController.Instance.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothFollow;
        //transform.LookAt(player);
    }
}
