using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityCharacterPlayer : EntityCharacter
{
    public int playerId { get { return transform.GetSiblingIndex(); } }

    public override void SetupWaitInput()
    {
        base.SetupWaitInput();
    }

    public override void WaitInput()
    {
        base.WaitInput();

        if (_CheckIsOnEvent())
            return;

        float moveH = Input.GetAxisRaw("Horizontal" + " #" + playerId);
        float moveV = Input.GetAxisRaw("Vertical" + " #" + playerId);
        bool isAttacking = Mathf.Abs(moveH) > 0.0f || Mathf.Abs(moveV) > 0.0f; // ganti ke swipe
        bool moveMod = Input.GetButton("Move Modifier" + " #" + playerId);
        bool skipTurn = _CheckDoubleInput("Move Modifier" + " #" + playerId, 0.5f);

        // send to tutorial here

        if (skipTurn)
        {
            StoredActionSkipTurn();
            return;
        }

        if (isAttacking)
        {
            int moveRange = moveMod ? 2 : 1;
            Vector3 moveDir = (Mathf.Abs(_FCInput(moveH) + _FCInput(moveV)) != 1.0f) ? new Vector3(0.0f, 0.0f, _FCInput(moveV)) : new Vector3(_FCInput(moveH), 0.0f, _FCInput(moveV));
            float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

            storedActions.Add(new StoredActionAttack(this, moveDir));
            return;
        }
    }

    public override void SetupProcessInput()
    {
        base.SetupProcessInput();

        _ResetAllInputButtonVariable();
    }

    public override void AfterInput()
    {
        base.AfterInput();

        afterActionHasDone = true;
    }

    public void Hit()
    {
        animator.SetTrigger("hit");
        storedStatusEffects.Add(new StoredStatusEffectCustom(() => { }, () => { Time.timeScale = 0; }));
    }

    private float _FCInput(float input)
    {
        return (input < 0.0f) ? Mathf.Floor(input) : Mathf.Ceil(input);
    }

    [HideInInspector] float m_buttonDownCount = 0;
    [HideInInspector] float m_buttonDownTime = 0;
    private bool _CheckDoubleInput(string inputName, float buttonDownDelay)
    {
        if (Input.GetButtonDown(inputName))
        {
            m_buttonDownCount++;
            if (m_buttonDownCount == 1) m_buttonDownTime = Time.time;
        }
        if (m_buttonDownCount > 1 && Time.time - m_buttonDownTime < buttonDownDelay)
        {
            m_buttonDownCount = 0;
            m_buttonDownTime = 0;
            return true;
        }
        else if (m_buttonDownCount > 2 || Time.time - m_buttonDownTime > 1) m_buttonDownCount = 0;
        return false;
    }

    [HideInInspector] float m_buttonHoldTime = 0;
    private bool _CheckHoldInput(string inputName, float holdTime)
    {
        if (Input.GetButtonDown(inputName))
            m_buttonHoldTime = 0;

        if (Input.GetButton(inputName))
        {
            m_buttonHoldTime += Time.deltaTime;
        }

        return m_buttonHoldTime >= holdTime;
    }

    private void _ResetAllInputButtonVariable()
    {
        m_buttonDownCount = 0;
        m_buttonDownTime = 0;
        m_buttonHoldTime = 0;
    }

    private bool _CheckIsOnEvent()
    {
        var disableMoveInputList = storedStatusEffects.OfType<StoredStatusEffectEventControl>();
        return disableMoveInputList.Count() > 0;
    }
}
