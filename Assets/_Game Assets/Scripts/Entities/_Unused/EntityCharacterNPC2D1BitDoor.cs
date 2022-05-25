using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordChoice
{
    public int trueAnswer { get; private set; }
    public string[] choiceStrings { get; private set; }
    public Dialogue hintDialogue { get; private set; }

    public PasswordChoice(int trueAnswer, string[] choiceStrings, Dialogue hintDialogue)
    {
        this.trueAnswer = trueAnswer;
        this.choiceStrings = choiceStrings;
        this.hintDialogue = hintDialogue;
    }
}

public class EntityCharacterNPC2D1BitDoor : EntityCharacterNPC
{
    public bool currentIsClosed = true;

    [SerializeField] EntityCharacterNPC2D1BitDoor[] m_connectedDoors;

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

    public void SetDoorIsClosed(bool isClosed, bool callConnectedDoorToo = true)
    {
        currentIsClosed = isClosed;
        
        // masukin ke stored action kah? ada fitur animasi cuma pas process input
        animator.SetBool("currentIsClosed", currentIsClosed);

        if(!isClosed)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<TagEntityUnpassable>().enabled = false;
            GetComponent<TagEntityInteractable>().enabled = false;
        }

        if (isClosed)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<TagEntityUnpassable>().enabled = true;
            GetComponent<TagEntityInteractable>().enabled = true;
        }

        if(callConnectedDoorToo)
        {
            foreach (EntityCharacterNPC2D1BitDoor door in m_connectedDoors)
            {
                door.SetDoorIsClosed(isClosed, false);
            }
        }
    }

    // check player have password 

    [HideInInspector] int[] m_playerAnswer;
    [HideInInspector] int m_currentPasswordIndex;
    public void EnterPassword(PasswordChoice[] passwordChoices, Action trueAction, Action wrongAction)
    {
        m_playerAnswer = new int[passwordChoices.Length];
        m_currentPasswordIndex = 0;
        _EnterPasswordRecursive(passwordChoices, trueAction, wrongAction);
    }

    protected override void _AssignComponent()
    {
        base._AssignComponent();
        SetDoorIsClosed(currentIsClosed, false);
    }

    private void _EnterPasswordRecursive(PasswordChoice[] passwordChoices, Action trueAction, Action wrongAction)
    {
        UIManager um = GameManager.Instance.uiManager;
        PasswordChoice cPC = passwordChoices[m_currentPasswordIndex];

        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(cPC.hintDialogue.nameStr, cPC.hintDialogue.dialogueStr, cPC.hintDialogue.voice),
        new DialogueChoice[3] {
                new DialogueChoice(cPC.choiceStrings[0], () => {
                    m_playerAnswer[m_currentPasswordIndex] = 0;
                    if(m_currentPasswordIndex+1 >= passwordChoices.Length)
                    {
                        _CheckPassword(passwordChoices, trueAction, wrongAction);
                    } else
                    {
                        m_currentPasswordIndex++;
                        _EnterPasswordRecursive(passwordChoices, trueAction, wrongAction);
                    }
                }),
                new DialogueChoice(cPC.choiceStrings[1], () => {
                    m_playerAnswer[m_currentPasswordIndex] = 1;
                    if(m_currentPasswordIndex+1 >= passwordChoices.Length)
                    {
                        _CheckPassword(passwordChoices, trueAction, wrongAction);
                    } else
                    {
                        m_currentPasswordIndex++;
                        _EnterPasswordRecursive(passwordChoices, trueAction, wrongAction);
                    }
                }),
                new DialogueChoice(cPC.choiceStrings[2], () => {
                    m_playerAnswer[m_currentPasswordIndex] = 2;
                    if(m_currentPasswordIndex+1 >= passwordChoices.Length)
                    {
                        _CheckPassword(passwordChoices, trueAction, wrongAction);
                    } else
                    {
                        m_currentPasswordIndex++;
                        _EnterPasswordRecursive(passwordChoices, trueAction, wrongAction);
                    }
                }),
            })));
    }

    private void _CheckPassword(PasswordChoice[] passwordChoices, Action trueAction, Action wrongAction)
    {
        bool answerIsTrue = true;
        for (int j = 0; j < passwordChoices.Length; j++)
        {
            if (m_playerAnswer[j] != passwordChoices[j].trueAnswer)
            {
                answerIsTrue = false;
                break;
            }
        }

        if (answerIsTrue) trueAction.Invoke();
        else wrongAction.Invoke();
    }
}
