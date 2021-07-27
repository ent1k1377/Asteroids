using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector2 _boundaryScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Move(collision.gameObject);
    }

    private void Move(GameObject GO)
    {
        GO.transform.position *= _boundaryScale;
    }
}
