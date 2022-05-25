using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCharacterNPCMonsterFrog : EntityCharacterNPC
{
    EntityCharacterPlayer m_targetPlayer;

    public override void WaitInput()
    {
        base.WaitInput();

        if (Vector3.Distance(transform.position, m_targetPlayer.transform.position) > 1.5f)
            SimpleMoveToPlayer();
        else
            AttackPlayer();
    }

    public override void AfterInput()
    {
        base.AfterInput();

        afterActionHasDone = true;
    }

    public void SimpleMoveToPlayer()
    {
        Vector3 dir = (m_targetPlayer.currentNode.realWorldPos - transform.position).normalized;
        storedActions.Add(new StoredActionMove(this, dir, true));
    }

    public void AttackPlayer()
    {
        Vector3 dir = (m_targetPlayer.currentNode.realWorldPos - transform.position).normalized;
        storedActions.Add(new StoredActionAttack(this, dir));
    }

    public void Hit()
    {
        animator.SetTrigger("hit");
        storedStatusEffects.Add(new StoredStatusEffectCustom(() => { }, () => { SetIsUpdateAble(false); }));
    }

    protected override void _AssignComponent()
    {
        base._AssignComponent();

        m_targetPlayer = GameManager.Instance.playerManager.GetPlayerPlayableList()[0];
    }
}
