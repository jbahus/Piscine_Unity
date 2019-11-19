using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleportTo;

    void OnCollisionEnter2D(Collision2D col)
    {
        Transform colTransform = col.collider.gameObject.transform;
        colTransform.position = new Vector3(teleportTo.transform.position.x, teleportTo.transform.position.y, colTransform.position.z);
    }
}
