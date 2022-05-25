using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEvent_PrototypeLevel : EntityEvent
{
    [SerializeField] EntityCharacterNPCMonsterFrog m_frogPrefab;

    [SerializeField] Transform[] m_spawns;

    public override void EventOnLoadLevel()
    {
        base.EventOnLoadLevel();

        player.storedStatusEffects.Add(new StoredStatusEffectAutoSkip(player, 0.25f));
    }

    public override void WaitInput()
    {
        base.WaitInput();

        float rnd = Random.Range(0, 100);
        if(rnd < 30.0f)
        {
            EntityManagerNPC entityManager = GameManager.Instance.npcManager;

            int rndSpawn = Random.Range(0, 4);
            var frog = Instantiate(m_frogPrefab, entityManager.transform, false);
            frog.transform.position = m_spawns[rndSpawn].position;
            entityManager.AddNPCRealtime(frog);
        }
    }
}
