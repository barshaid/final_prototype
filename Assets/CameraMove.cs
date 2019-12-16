using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float damping = 1.5f;
    public Transform player;
    public Vector2 offset = new Vector2(2f, 1f);

    private bool faceLeft;
    private int lastX;
    private float dynamicSpeed;
    private Camera cam;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer();
        cam = gameObject.GetComponent<Camera>();
    }

    public void FindPlayer()
    {
        lastX = Mathf.RoundToInt(player.position.x);
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }

    void FixedUpdate()
    {
        
        
        if (player)
        {
            float currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);
            currentX = Mathf.Clamp(transform.position.x, -10, 10);
            Vector3 target;
            if (faceLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y+dynamicSpeed, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y+dynamicSpeed, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
