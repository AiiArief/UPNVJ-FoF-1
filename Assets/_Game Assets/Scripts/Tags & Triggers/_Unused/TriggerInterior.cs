using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInterior : MonoBehaviour
{
    [SerializeField] MeshRenderer m_roof;
    bool m_isPlayerHere = false;

    private void OnTriggerStay(Collider other)
    {
        if (m_isPlayerHere)
            return;

        EntityCharacterPlayer player = other.GetComponent<EntityCharacterPlayer>();
        if (player)
        {
            m_isPlayerHere = true;
            m_roof.enabled = false;
            foreach (Transform mesh in m_roof.transform)
            {
                mesh.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!m_isPlayerHere) // cara ngecek kalo masih ada player lain gmana ya
            return;

        EntityCharacterPlayer player = other.GetComponent<EntityCharacterPlayer>();
        if (player)
        {
            m_isPlayerHere = false;
            m_roof.enabled = true;
            foreach (Transform mesh in m_roof.transform)
            {
                mesh.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
