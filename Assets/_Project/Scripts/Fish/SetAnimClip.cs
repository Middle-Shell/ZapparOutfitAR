using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimClip : MonoBehaviour
{
    private Animator m_animator;

    [SerializeField] private int m_frame;

    [SerializeField] private AnimationClip m_animationClip;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        PlayClipFromFrame(m_animationClip);
        
    }

    private void PlayClipFromFrame(AnimationClip animationClip)
    {
        m_frame = UnityEngine.Random.Range(0, 120);
        float frameTime = animationClip.length / animationClip.frameRate;
        m_animator.Play(animationClip.name, 0, frameTime * m_frame);
    }
}
