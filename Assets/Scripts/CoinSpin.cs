using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour {
	/// <summary>
	/// 回転速度
	/// </summary>
	private const float m_fSpinSpeed = 3;

	// Use this for initialization
	void Start()
	{
		//回転を開始する角度を設定
		transform.Rotate(0, Random.Range(0, 360), 0);
	}

	// Update is called once per frame
	void Update()
	{
		//回転
		transform.Rotate(0, m_fSpinSpeed, 0);
	}
}
