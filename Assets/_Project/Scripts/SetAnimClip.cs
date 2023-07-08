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
        PlayClipFromFrame(m_animationClip, m_frame);
    }

    private void PlayClipFromFrame(AnimationClip animationClip, int frame)
    {
        float frameTime = animationClip.length / animationClip.frameRate;
        m_animator.Play(animationClip.name, 0, frameTime * frame);
    }
}
