using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour
{
    private PlayerInventory inv;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inv = other.gameObject.GetComponent<PlayerInventory>();
            inv.keySlot[0] = gameObject;
            gameObject.SetActive(false);
        }
    }
}
