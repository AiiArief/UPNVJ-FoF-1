using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManagerNPC : EntityManager
{
    public override void SetupEntitiesOnLevelStart()
    {
        base.SetupEntitiesOnLevelStart();
        _AssignNPCsToGrid();
        _SetNPCsIsActive();
    }

    public List<EntityCharacterNPC> GetNPCPlayableList()
    {
        var playableNPCs = new List<EntityCharacterNPC>();
        foreach (EntityCharacterNPC npc in entities)
        {
            if (npc.isUpdateAble)
            {
                playableNPCs.Add(npc);
            }
        }

        return playableNPCs;
    }

    /// <summary>
    /// Add NPC in realtime (not on start, etc etc)
    /// NOTE : There'll be error if adding while iterating, use with cautions
    /// </summary>
    /// <param name="npc"></param>
    public void AddNPCRealtime(EntityCharacterNPC npc)
    {
        entities.Add(npc);
        npc.SetIsUpdateAble(true);
        npc.AssignToLevelGrid();
    }

    private void _AssignNPCsToGrid()
    {
        foreach(EntityCharacterNPC npc in entities)
        {
            npc.AssignToLevelGrid();
        }
    }

    private void _SetNPCsIsActive()
    {
        foreach(EntityCharacterNPC npc in entities)
        {
            npc.SetIsUpdateAble(true);
        }
    }
}
