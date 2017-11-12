using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanControl : MonoBehaviour
{
	#region プロパティ
	/// <summary>
	///	Unityちゃんアニメーションマネージャ
	/// </summary>
	private UnityChanAnimationManager m_AnimationManager;

	/// <summary>
	/// 物理制御
	/// </summary>
	private Rigidbody m_RigidBody;

	/// <summary>
	/// コイン取得時のパーティクル
	/// </summary>
	private ParticleSystem m_ParticleSystem;

	/// <summary>
	/// ゲームリザルトテキスト
	/// </summary>
	[SerializeField]
	private Text m_ResultText;

	/// <summary>
	/// スコアテキスト
	/// </summary>
	[SerializeField]
	private Text m_ScoreText;

	/// <summary>
	/// スコア
	/// </summary>
	private int m_nScore;

	/// <summary>
	/// 前進力
	/// </summary>
	private float m_fForwardForce;

	/// <summary>
	/// 横移動力
	/// </summary>
	private float m_fTurnForce;

	/// <summary>
	/// 最大横移動距離
	/// </summary>
	private const float m_fMoveRange = 3.4f;

	/// <summary>
	/// ジャンプ力
	/// </summary>
	private float m_fJumpForce;

	/// <summary>
	/// ジャンプ可能範囲
	/// </summary>
	private const float m_fToJumpRange = 0.5f;

	/// <summary>
	/// 減速力
	/// </summary>
	private float m_fStoppingForce = 0.95f;

	/// <summary>
	/// 終了判定
	/// </summary>
	private bool m_bEnd;
	#endregion

	private void Start()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		m_AnimationManager = GetComponent<UnityChanAnimationManager>();
		m_ParticleSystem = GetComponent<ParticleSystem>();
		m_AnimationManager.Init();
		m_bEnd = false;
		m_nScore = 0;
		ForceSet();
	}

	private void ForceSet()
	{
		m_fForwardForce = 800.0f;
		m_fTurnForce = 500.0f;
		m_fJumpForce = 500.0f;
		m_AnimationManager.AnimationResetSpeed();
	}

	private void Update()
	{
		if (m_bEnd)
		{
			m_fForwardForce *= m_fStoppingForce;
			m_fTurnForce *= m_fStoppingForce;
			m_fJumpForce *= m_fStoppingForce;
			m_AnimationManager.AnimationDecaySpeed(m_fStoppingForce);
		}

		// 前移動
		m_RigidBody.AddForce(transform.forward * m_fForwardForce);

		// 横移動
		if (Input.GetKey(KeyCode.LeftArrow))
			LeftMove();
		if (Input.GetKey(KeyCode.RightArrow))
			RightMove();

		// ジャンプ
		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}

	public void RightMove()
	{
		// 範囲確認
		if (transform.position.x < m_fMoveRange)
		{
			m_RigidBody.AddForce(m_fTurnForce, 0, 0);
		}
	}

	public void LeftMove()
	{
		// 範囲確認
		if (-m_fMoveRange < transform.position.x)
		{
			m_RigidBody.AddForce(-m_fTurnForce, 0, 0);
		}
	}

	public void Jump()
	{
		if (transform.position.y < m_fToJumpRange)
		{
			m_AnimationManager.Jump();
			m_RigidBody.AddForce(transform.up * m_fJumpForce);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
		{
			m_ResultText.text = "GAME OVER...";
			m_bEnd = true;
		}

		//コイン取得
		if (other.gameObject.tag == "CoinTag")
		{
			m_nScore += 10;
			m_ScoreText.text = "Score : " + m_nScore.ToString() + "pt";
			m_ParticleSystem.Play();
			Destroy(other.gameObject);
		}

		//ゴール地点に到達した場合
		if (other.gameObject.tag == "GoalTag")
		{
			m_ResultText.text = "GAME CLEAR!!";
			m_bEnd = true;
		}
	}
}
