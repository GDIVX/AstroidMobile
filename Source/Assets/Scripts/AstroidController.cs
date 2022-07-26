using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class AstroidController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 3;
    public UnityEvent<AstroidController> onRemoved;
    public float maxTravelDistance = 200;

    float traveldDistance = 0f;

    private void FixedUpdate()
    {
        if (rb == null)
        {
            throw new MissingReferenceException();
        }
        if (traveldDistance >= maxTravelDistance)
        {
            Remove();
            return;
        }
        rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force);
        traveldDistance += speed * Time.deltaTime;
    }



    public void Remove()
    {
        onRemoved?.Invoke(this);
        gameObject.SetActive(false);
        GameObject.Destroy(gameObject);
    }
}
