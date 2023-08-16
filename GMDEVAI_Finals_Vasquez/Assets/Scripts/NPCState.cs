using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : StateMachineBehaviour
{
    public GameObject NPC;

    public GameObject prey;

    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        prey = NPC.GetComponent<CrocAI>().GetFood();
    }
}
