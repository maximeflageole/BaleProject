using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    protected BaseCharacter m_owner;

    [field: SerializeField]
    public AbilityData Data { get; protected set; }

    protected float m_currentCooldown;
    public float CurrentCast { get; protected set; }
    public bool InCooldown { get; protected set; }
    public bool IsCasting { get; protected set; }

    public virtual void SetAbilityData(AbilityData data)
    {
        if (Data != null)
        {
            m_owner.OnUnequipAbility(Data);
        }
        Data = data;
        m_currentCooldown = Data.Cooldown;
        InCooldown = true;
    }

    public void OnTryCast()
    {
        if (!InCooldown)
        {
            m_owner.OnTryCast(this);
        }
    }

    public void BeginCast()
    {
        IsCasting = true;
        CurrentCast = 0.0f;
        InCooldown = true;
        m_currentCooldown = Data.Cooldown;
    }

    protected virtual void Update()
    {
        if (IsCasting)
        {
            CurrentCast += Time.deltaTime;
            if (CurrentCast >= Data.CastingTime)
            {
                IsCasting = false;
                CurrentCast = 0.0f;
                m_owner.OnEndCast();
            }
        }
        else if (InCooldown)
        {
            m_currentCooldown -= Time.deltaTime;
            if (m_currentCooldown <= 0.0f)
            {
                InCooldown = false;
                m_currentCooldown = Mathf.Max(m_currentCooldown, 0.0f);
            }
        }
    }

    public void InstantiateSlot(BaseCharacter owner)
    {
        m_owner = owner;
    }
}
