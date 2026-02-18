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
    [Header("Player Life")]
    [SerializeField] readonly int _maxHealth = 3;
    [SerializeField] int _currentHealth = 3;

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

    #region Default Movement

    public IEnumerator PauseDefaultMovement(float duration, System.Action onResume = null)
    {
        isAllowedDefaultMovement = false;
        yield return new WaitForSeconds(duration);
        isAllowedDefaultMovement = true;
        onResume?.Invoke();
    }

    #endregion

    #region Health

    public int currentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    #endregion
}