using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] public Transform target; 

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}