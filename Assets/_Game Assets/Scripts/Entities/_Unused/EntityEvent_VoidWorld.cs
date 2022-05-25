using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityEvent_VoidWorld : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        _BasicOnLoadLevel();
        AddBasicStatusEffectOnStartingEvent();

        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));

        string currentScene = PlayerPrefs.GetString(ProfileManager.PLAYERPREFS_CURRENTSCENE, "Void World");
        if (currentScene != "Void World")
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", "Agent Violet. Agent Violet. Bangun oy, lu masih di "+ currentScene + " kan?", m_voicePack),
                    new DialogueChoice[2] {
                        new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_LOADGAME_0_1), () => _LoadGameButton(currentScene)),
                        new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_LOADGAME_0_2), () => _ClearSaveGameButton())
                    })));
            return;
        }

        um.AddUIAction(() => { player.animator.SetInteger("expression", 2); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("???", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_0), m_voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_0_1), () => { 
                        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_LANGUAGEID, (int)LocalizationLanguage.English); 
                    }),
                    new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_0_2), () => { 
                        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_LANGUAGEID, (int)LocalizationLanguage.Indonesia); 
                    })
                })));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("???", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_1), m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_2), m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_3), m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_4), m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => { player.animator.SetInteger("expression", 0); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_5), m_voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_5_1), () => { 
                        _NewGameButton(); 
                    }),
                    //new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_5_2), () => { }),
                    new DialogueChoice(LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_5_3), () => { 
                        _QuitButton(); 
                    })
                })));
    }

    private void _NewGameButton()
    {
        um.AddUIAction(() => { player.animator.SetInteger("expression", 1); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_6), m_voicePack))));
        um.AddUIAction(() => { player.animator.SetInteger("expression", 3); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Developer", LocalizationManager.Translate(LocalizationManager.VW_ONLOAD_7), m_voicePack))));
        um.AddUIAction(() => { em.triggerCheckpointsParent.GetChild(0).GetComponent<TriggerCheckpoint>().teleportArea.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
        um.AddUIAction(() => { player.animator.gameObject.SetActive(false); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene("Level 0"));
    }

    private void _LoadGameButton(string sceneName)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => SceneManager.LoadScene(sceneName));
    }

    private void _ClearSaveGameButton()
    {
        GlobalGameManager.Instance.profileManager.ClearProfile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // tanyain are you sure

        //um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Pak Pres", "Agent Violet. Agent Violet. Bangun oy, lu masih di " + currentScene + " kan?"),
        //        new DialogueChoice[2] {
        //            new DialogueChoice("Eiya sori, barusan bengong wkwk (Load Game)", () => { SceneManager.LoadScene(currentScene); }), // kayaknya butuh tunggu sedetik
        //            new DialogueChoice("Hah? Engga ko, ngablu kali lu (New Game)", () => { _ClearSaveGameButton(); })
        //        })));
    }
}
