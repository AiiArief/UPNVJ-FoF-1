using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCharacterNPC2D1BitSwitch : EntityCharacterNPC
{
    [SerializeField] EntityCharacterNPC2D1BitDoor[] m_switchForDoors;
    public EntityCharacterNPC2D1BitDoor[] switchForDoors { get { return m_switchForDoors; } }

    StoredStatusEffectCustom m_autoCloseEffect;

    public override void WaitInput()
    {
        base.WaitInput();

        _DoIdle();
    }

    public override void AfterInput()
    {
        base.AfterInput();

        afterActionHasDone = true;
    }

    public void UseSwitch(int autoSwitchTurn = 0)
    {
        foreach(EntityCharacterNPC2D1BitDoor door in m_switchForDoors)
        {
            door.SetDoorIsClosed(!door.currentIsClosed, false);
        }

        if(autoSwitchTurn > 0)
        {
            if(m_autoCloseEffect == null)
            {
                m_autoCloseEffect = new StoredStatusEffectCustom(() => { },
                () => {
                    autoSwitchTurn = (autoSwitchTurn < 0) ? autoSwitchTurn : Mathf.Max(0, autoSwitchTurn - 1);
                    if (autoSwitchTurn == 0)
                    {
                        foreach (EntityCharacterNPC2D1BitDoor door in m_switchForDoors)
                        {
                            door.SetDoorIsClosed(!door.currentIsClosed, false);
                        }
                        storedStatusEffects.Remove(m_autoCloseEffect);
                        m_autoCloseEffect = null;
                    }
                });

                storedStatusEffects.Add(m_autoCloseEffect);
            }
        }
    }
}
