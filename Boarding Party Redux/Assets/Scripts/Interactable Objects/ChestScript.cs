using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : InteractableObject
{
    [Header("Chest Script")]
    public int score;
    public Animator anim;

    public override void Awake()
    {
        base.Awake();

        anim = GetComponentInChildren<Animator>();
    }

    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        GivePoints(score, player);
    }
    
    public virtual void GivePoints(int scoreToGive, PlayerController player = null)
    {
        anim.SetTrigger("open");
        GeneralManager.UpdateScore(scoreToGive);

        if (player != null)
        {
            player.UpdateScore(scoreToGive);
        }
        else
        {

        }
    }
}
