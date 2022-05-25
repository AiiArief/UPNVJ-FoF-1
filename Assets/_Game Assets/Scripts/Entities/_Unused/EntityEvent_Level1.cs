using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityEvent_Level1 : EntityEvent
{
    //public override void EventOnLoadLevel()
    //{
    //    base.EventOnLoadLevel();
    // pasang story pas awal masuk level
    //}

    public void DoorNoSwitchEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "There's no switch to open this door ..."))));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
    }

    public void OpenDoorEvent_1(EntityCharacterNPC2D1BitDoor door)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Do you want to get rid of me?"),
                new DialogueChoice[2] {
                    new DialogueChoice("Yes", () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "F-Fine!"))));
                        um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    new DialogueChoice("Er... Nah", () => {
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                })));
    }

    public void OpenDoorEvent_Timing_1(EntityCharacterNPC2D1BitSwitch doorSwitch)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Juh? Do you want to open me? But make it quick, it'll closed automatically."),
                new DialogueChoice[2] {
                    new DialogueChoice("Alright", () => {
                        um.AddUIAction(() => { doorSwitch.UseSwitch(25); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    new DialogueChoice("Why are you here?", () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Hmm ... Idk lol"))));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                })));
    }

    public void OpenDoorEvent_Password_1(EntityCharacterNPC2D1BitDoor door)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Need password bruh"),
                new DialogueChoice[3] {
                    new DialogueChoice("Enter password", () => {
                        door.EnterPassword(new PasswordChoice[] { 
                            new PasswordChoice(0, new string[] { "Rejected" , "Loved" , "...ed" } , new Dialogue("The Door Door", "One more god ...")),  
                            new PasswordChoice(1, new string[] { "Heart" , "Bug" , "Meat" } , new Dialogue("The Door Door", "I'm the ... inside you")),  
                            new PasswordChoice(2, new string[] { "Angel" , "Maiden" , "Judgement" } , new Dialogue("The Door Door", "... has come to you")),
                        },
                        () => {
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "It's ... true?"))));
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                        },
                        () => {
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Wrong password stupid"))));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                        });
                    }),
                    new DialogueChoice("Use Password", () => {
                        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-1";
                        if(PlayerPrefs.GetString(key, false.ToString()) == true.ToString())
                        {
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "It's ... true?"))));
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                        } else
                        {
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "You don't have password for me ..."))));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                        }
                    }),
                    new DialogueChoice("Er... Nah", () => {
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                })));
    }

    public void PasswordTriggerEvent_1()
    {
        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-1";

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Crack in Time", "The password for the door is ..."))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Crack in Time", "\"One more god rejected\""))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Crack in Time", "\"I'm the bug inside you\""))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Crack in Time", "\"Judgement has come to you\""))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("Crack in Time", "Play a real Shin Megami Tensei please"))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        if(PlayerPrefs.GetString(key, false.ToString()) == false.ToString())
        {
            PlayerPrefs.SetString(ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-1", true.ToString());
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "You magically remembered the password!"))));
            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
        } else
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "You're already remembered the password ..."))));
            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
        }
    }

    public void CloseDoorEvent_1(EntityCharacterNPC2D1BitSwitch doorSwitch)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));

        if(!doorSwitch.switchForDoors[0].currentIsClosed)
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Do you want to close me so the robot can't see you?"),
                    new DialogueChoice[2] {
                    new DialogueChoice("Yes", () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Well, they are stupid anyway, they won't notice it lol"))));
                        um.AddUIAction(() => { doorSwitch.UseSwitch(); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    new DialogueChoice("Er... Nah", () => {
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    })));
        } else
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Do you want to open me?"),
                    new DialogueChoice[2] {
                    new DialogueChoice("Yes", () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("The Door Door", "Whatev"))));
                        um.AddUIAction(() => { doorSwitch.UseSwitch(); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    new DialogueChoice("Er... Nah", () => {
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
                    }),
                    })));
        }
    }
}
