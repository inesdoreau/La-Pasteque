using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopLimit : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit fruitScript = collision.gameObject.GetComponent<Fruit>();
            if (fruitScript.hasBeenDropped)
            {
                print("Game Over");
                AudioManager.Instance.PlaySFX("GameOverSoft");
                GameManager.Instance.GameOver();
            }
        }
    }
}
