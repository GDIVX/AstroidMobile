using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class DragController : MonoBehaviour
{
    public float speed = 1f;
    public bool snap = false;
    Vector3 offset;
    private void OnMouseDown()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePos - transform.position;
    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, transform.position.z);


        var move = snap ? mousePos : Vector3.Lerp(transform.position, mousePos, Time.deltaTime * speed);

        Cursor.lockState = CursorLockMode.Confined;

        transform.position = move;
    }

    private void OnMouseUp()
    {
    }
}
