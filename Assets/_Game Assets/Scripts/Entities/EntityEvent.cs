using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityEvent: Entity
{
    protected UIManager um;
    protected EntityManagerEvent em;
    protected EntityCharacterPlayer player;

    [SerializeField] protected VoicePack m_voicePack;

    public virtual void EventOnLoadLevel()
    {
        _BasicOnLoadLevel();
        _TeleportPlayerToCheckpoint(em.triggerCheckpointsParent.GetChild(em.currentcheckpoint).GetComponent<TriggerCheckpoint>());
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
    }

    public void PlayerIsCapturedEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Guard", "SIAPA LU WOY!"),
                new DialogueChoice[2] {
                    new DialogueChoice("Bye! (Revert to last memory)", () => { _RetryLastCheckpointButton(); }),
                    new DialogueChoice("I want to sleep (Quit)", () => { _QuitButton(); })
                })));
    }

    protected void _BasicOnLoadLevel()
    {
        um = GameManager.Instance.uiManager;
        em = GameManager.Instance.eventManager;
        player = GameManager.Instance.playerManager.GetPlayerPlayableList()[0];
    }

    // handle kalo ga ada childnya
    protected void _TeleportPlayerToCheckpoint(TriggerCheckpoint checkpoint)
    {
        LevelManager levelManager = GameManager.Instance.levelManager;
        LevelGrid grid = levelManager.GetClosestGridFromPosition(checkpoint.transform.position);
        LevelGridNode checkpointNode = grid.ConvertPosToNode(checkpoint.transform.position);
        player.currentNode.entityListOnThisNode.Remove(player);
        player.AssignToLevelGrid(checkpointNode);
        player.transform.position = checkpointNode.realWorldPos;
        player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, checkpoint.startRotation, 0.0f));
    }

    protected void _TeleportPlayerToScene(string sceneName, int checkpointId = 0)
    {
        PlayerPrefs.SetString(ProfileManager.PLAYERPREFS_CURRENTSCENE, sceneName);
        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_CURRENTSCENECHECKPOINT, checkpointId);

        SceneManager.LoadScene(sceneName);
    }

    protected void _QuitButton()
    {
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Pak Pres", "EHH BENTAR BENTAR"))));
        um.AddUIAction(() => Application.Quit());
    }

    private void _RetryLastCheckpointButton()
    {
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene(SceneManager.GetActiveScene().name, em.currentcheckpoint));
    }

    StoredStatusEffect[] m_eventStatusEffects;
    public void AddBasicStatusEffectOnStartingEvent()
    {
        m_eventStatusEffects = new StoredStatusEffect[2] { new StoredStatusEffectEventControl(player), new StoredStatusEffectAutoSkip(player) };
        player.storedStatusEffects.Add(m_eventStatusEffects[0]);
        player.storedStatusEffects.Add(m_eventStatusEffects[1]);
    }

    public void RemoveBasicStatusEffectOnFinishEvent()
    {
        if(m_eventStatusEffects.Length > 0)
        {
            foreach(StoredStatusEffect eventStatusEffect in m_eventStatusEffects)
            {
                eventStatusEffect.isGoingToBeRemovedFlag = true;
            }
        }
    }
}
