using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FollowScript : StateMachineBehaviour
{
    private Transform findPlayer;
    public float speed;
    
    //starts 
    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        findPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //updates
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
      // anim.transform.position = Vector3.MoveTowards(anim.transform.position, findPlayer.position, speed * Time.deltaTime);
    }
    //stops
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
