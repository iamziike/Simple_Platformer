using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStateField
{
    isAllowedDefaultMovement,
    isAllowedDefaultJump,
    canUseAbility
}

public class PlayerState : MonoBehaviour
{
    [SerializeField] public bool isAllowedDefaultMovement { get; private set; } = true;
    [SerializeField] public bool isAllowedDefaultJump { get; private set; } = true;
    [SerializeField] public bool canUseAbility { get; private set; } = false;

    public void SetState(IDictionary<PlayerStateField, bool> updates)
    {
        if (updates == null)
        {
            return;
        }

        if (updates.TryGetValue(PlayerStateField.isAllowedDefaultMovement, out var nextAllowedDefaultMovement))
        {
            isAllowedDefaultMovement = nextAllowedDefaultMovement;
        }

        if (updates.TryGetValue(PlayerStateField.isAllowedDefaultJump, out var nextAllowedDefaultJump))
        {
            isAllowedDefaultJump = nextAllowedDefaultJump;
        }

        if (updates.TryGetValue(PlayerStateField.canUseAbility, out var nextCanUseAbility))
        {
            canUseAbility = nextCanUseAbility;
        }
    }

    public void PauseDefaultMovement(float duration)
    {
        isAllowedDefaultMovement = false;
        StartCoroutine(ResumeDefaultMovementAfterDelay(duration));
    }

    private IEnumerator ResumeDefaultMovementAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAllowedDefaultMovement = true;
    }
}