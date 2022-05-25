using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EntityEvent_Level0 : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        base.EventOnLoadLevel();

        // save kalo udah dialognya
        AddBasicStatusEffectOnStartingEvent();
        player.animator.gameObject.SetActive(false);

        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => { em.triggerCheckpointsParent.GetChild(0).GetComponent<TriggerCheckpoint>().teleportArea.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
        um.AddUIAction(() => { player.animator.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { player.storedActions.Add(new StoredActionTurn(player, 180.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", "Halo halo", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", "Pergi ke dalam rumah tersebut terus ke depan komputernya ya", m_voicePack))));
        um.AddUIAction(() => { player.storedActions.Add(new StoredActionTurn(player, 0.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.Move, LocalizationManager.TUTORIAL_MOVE), 5.0f); um.NextAction(); });

        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
    }

    public void RunTutorialEvent()
    {
        AddBasicStatusEffectOnStartingEvent();

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { player.storedActions.Add(new StoredActionTurn(player, 180.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", "Yoih, dunia ini gerak kalo lu gerak, it's synchronous baybee", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", "Nah sekarang coba lari deh", m_voicePack))));
        um.AddUIAction(() => { player.storedActions.Add(new StoredActionTurn(player, 0.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.MoveMod, LocalizationManager.TUTORIAL_RUN), 5.0f); um.NextAction(); }); 
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
    }

    [SerializeField] CinemachineVirtualCamera virtualCamera1;
    [SerializeField] CinemachineVirtualCamera virtualCamera2; 
    public void WinCheckpointEvent()
    {
        virtualCamera1.gameObject.SetActive(true);

        // ganti kamera
        // kasih wajah di laptop
        // peek a boo
        // player kaget
        // teleport
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "?"))));
        um.AddUIAction(() => { 
            virtualCamera1.gameObject.SetActive(false); 
            virtualCamera2.gameObject.SetActive(true);
            player.animator.SetInteger("expression", 1);
            um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => {
            player.animator.SetInteger("expression", 4);
            um.NextAction();
        });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "!"))));
        um.AddUIAction(() => { em.triggerCheckpointsParent.GetChild(1).GetComponent<TriggerCheckpoint>().teleportArea.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
        um.AddUIAction(() => { 
            virtualCamera2.gameObject.SetActive(false); 
            player.animator.gameObject.SetActive(false); 
            um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene("Level 1"));
    }
}
