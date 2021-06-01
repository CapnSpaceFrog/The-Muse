using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageInteract : MonoBehaviour
{
    private PlayerInventory inv;
    private Player player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            inv = other.gameObject.GetComponent<PlayerInventory>();
            if (inv.keySlot[0].name == "Key")
            {
                player.SendMessage("GameWon");
                anim.SetBool("open", true);
            }
        }
    }

    public void AnimationFinish()
    {
        anim.SetBool("empty", true);
        player.hasWon = true;
    }
}
