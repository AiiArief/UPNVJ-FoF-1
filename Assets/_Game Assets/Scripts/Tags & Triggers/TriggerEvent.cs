using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventTriggerType
{
    PlayerTouch, NPCOnlyTouch, AnyCharacterTouch
}

public class TriggerEvent : MonoBehaviour
{
    // cari cara biar eventnya bisa di search

    [SerializeField] EventTriggerType m_triggerType = EventTriggerType.PlayerTouch;

    [SerializeField] UnityEvent m_event;

    private void OnTriggerEnter(Collider other)
    {
        if(m_triggerType == EventTriggerType.PlayerTouch)
        {
            EntityCharacterPlayer player = other.GetComponent<EntityCharacterPlayer>();
            if (player)
            {
                if (m_event.GetPersistentEventCount() > 0)
                {
                    m_event.Invoke();
                    return;
                }
            }
        }
    }
}
