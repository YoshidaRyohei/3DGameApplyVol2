using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimationManager : MonoBehaviour {
	//アニメーションするためのコンポーネントを入れる
	private Animator m_Animator;

	/// <summary>
	/// デフォルトアニメーション速度
	/// </summary>
	private float m_fDefaultAnimetionSpeed;

	public void Init()
	{
		//Animatorコンポーネントを取得
		m_Animator = GetComponent<Animator>();

		//走るアニメーションを開始
		m_Animator.SetFloat("Speed", 1);

		m_fDefaultAnimetionSpeed = m_Animator.speed;
	}

	void Update()
	{
		if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
		{// アニメーション変更フラグがboolなので疑似的にトリガー化
			m_Animator.SetBool("Jump", false);
		}
	}

	public void Jump()
	{
		m_Animator.SetBool("Jump", true);
	}

	/// <summary>
	/// アニメーション速度減衰
	/// </summary>
	/// <param name="DecayValue">減衰率</param>
	public void AnimationDecaySpeed(float DecayValue)
	{
		m_Animator.speed *= DecayValue;
	}

	/// <summary>
	/// アニメーション速度リセット
	/// </summary>
	public void AnimationResetSpeed()
	{
		m_Animator.speed = m_fDefaultAnimetionSpeed;
	}
}
