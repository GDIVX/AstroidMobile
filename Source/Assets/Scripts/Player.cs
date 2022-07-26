using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Astroid")){
            GameManager.instance.SetState(GameState.Eneded);
            Debug.Log("Gamve over");
            gameObject.SetActive(false);
        }
    }
}
