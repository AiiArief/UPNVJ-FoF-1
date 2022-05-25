using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManagerEvent: EntityManager
{
    [SerializeField] EntityEvent m_levelEvent;
    public EntityEvent levelEvent { get { return m_levelEvent; } }

    [SerializeField] Transform m_triggerCheckpointsParent;
    public Transform triggerCheckpointsParent { get { return m_triggerCheckpointsParent; } }
    public int currentcheckpoint { get; private set; } = 0;

    [SerializeField] EntityLevelFX m_levelFX;

    public List<Action> afterInputActionList { get; private set; } = new List<Action>();

    public override void SetupEntitiesOnLevelStart()
    {
        base.SetupEntitiesOnLevelStart();
        
        currentcheckpoint = PlayerPrefs.GetInt(ProfileManager.PLAYERPREFS_CURRENTSCENECHECKPOINT, 0);
        _RefreshAllCheckpoints();

        m_levelEvent.EventOnLoadLevel();
        _ExecuteFirstAction();

        m_levelFX.FXOnLoadLevel();
    }

    public override void SetupEntitiesOnAfterInputStart()
    {
        base.SetupEntitiesOnAfterInputStart();
        _ExecuteFirstAction();
    }

    public void ChangeCheckpoint(int checkpoint)
    {
        currentcheckpoint = checkpoint;
        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_CURRENTSCENECHECKPOINT, checkpoint);
        _RefreshAllCheckpoints();
    }

    private void _ExecuteFirstAction()
    {
        if(afterInputActionList.Count > 0)
        {
            afterInputActionList[0].Invoke();
            afterInputActionList.RemoveAt(0);
        }
    }

    private void _RefreshAllCheckpoints()
    {
        foreach(Transform child in m_triggerCheckpointsParent)
        {
            child.GetComponent<TriggerCheckpoint>().SetIsCheckpointHere(currentcheckpoint == child.transform.GetSiblingIndex());
        }
    }
}
