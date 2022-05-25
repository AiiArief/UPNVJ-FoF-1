using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLevelFX : Entity
{
    [SerializeField] Transform m_levelFXParent;
    [SerializeField] Transform m_stepSoundParent;

    [SerializeField] float m_skyboxSpeed = 0.0f;
    float m_currentSkyboxRotation = 0.0f;

    [Header("Wind Effect")]
    [SerializeField] bool m_hasWindEffect;
    [SerializeField] AudioSource m_windAmbienceSoundEffect;
    [SerializeField] GameObject m_playerSpringBoneParent; // belum disetting buat semua entity, probably berat lol

    public void FXOnLoadLevel()
    {
        HandleSkyBox();
    }

    public override void WaitInput()
    {
        HandleFX(false);

        storedActions.Add(new StoredActionLevelFX(this));
    }

    public void HandleSkyBox()
    {
        if(m_skyboxSpeed > 0.0f)
        {

        }
    }

    public void HandleWindEffect()
    {
        if(m_hasWindEffect)
        {

        }
    }

    public void HandleFX(bool play = true)
    {
        /*var pss = m_stepSoundParent.GetComponentsInChildren<ParticleSystem>(); // boros betulll
        foreach(ParticleSystem ps in pss)
        {
            if (play && ps.isPaused)
                ps.Play();
            else
                ps.Pause();
        }
        */
    }

    public void ChangeWindEffect(bool hasWindEffect)
    {
        m_hasWindEffect = hasWindEffect;
        StartCoroutine(_AmbiencePitchLerp(hasWindEffect));
        if (!m_hasWindEffect)
        {

        }
    }

    private IEnumerator _AmbiencePitchLerp(bool windEffect)
    {
        if(windEffect)
        {
            while(m_windAmbienceSoundEffect.pitch < 0.3f)
            {
                m_windAmbienceSoundEffect.pitch += 0.2f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        } else
        {
            while (m_windAmbienceSoundEffect.pitch > 0.1f)
            {
                m_windAmbienceSoundEffect.pitch -= 0.2f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
