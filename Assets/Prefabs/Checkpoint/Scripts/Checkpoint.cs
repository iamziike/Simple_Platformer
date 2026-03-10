using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckpointType
{
    Start,
    Middle,
    End
}

public class Checkpoint : MonoBehaviour
{
    Animator animator;
    bool isActivated;
    [SerializeField] CheckpointType _checkpointType;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tag.Player) && (!isActivated || _checkpointType == CheckpointType.Start))
        {
            animator.SetTrigger("Activated");
            isActivated = true;
        }
    }

    public CheckpointType CheckpointType
    {
        get { return _checkpointType; }
    }
}
