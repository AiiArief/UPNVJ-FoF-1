using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCharacterNPCGuard : EntityCharacterNPC
{
    Vector3 m_guardedArea; 
    bool m_guardedAreaHasBeenSetup = false;
    float m_guardedRotation = 0.0f;

    public override void SetupWaitInput()
    {
        base.SetupWaitInput();
    
        if(!m_guardedAreaHasBeenSetup)
        {
            m_guardedAreaHasBeenSetup = true;
            m_guardedArea = transform.position;
            m_guardedRotation = transform.rotation.eulerAngles.y;
        }
    }

    public void SetGuardArea(Transform waypoint)
    {
        m_guardedArea = waypoint.position;
        _NextCurrentIdle();
    }

    protected override void _DoIdle()
    {
        if (!_CheckHasArrivedAtPoint(m_guardedArea))
        {
            _MoveToPointNPC(m_guardedArea);
        }
        else
        {
            if (m_idleWaitInputs.Length == 0)
            {
                TurnToDegreeNPC(m_guardedRotation);
            }

            base._DoIdle();
        }
    }
}
