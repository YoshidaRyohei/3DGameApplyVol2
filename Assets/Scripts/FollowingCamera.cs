using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
	/// <summary>
	/// Unityちゃんオブジェクト
	/// </summary>
	[SerializeField]
	private GameObject m_UnityChan;

	/// <summary>
	/// 距離
	/// </summary>
	private float difference;

	void Start()
	{
		//Unityちゃんとカメラの位置（z座標）の差を求める
		difference = m_UnityChan.transform.position.z - transform.position.z;
	}

	// Update is called once per frame
	void Update()
	{
		//Unityちゃんの位置に合わせてカメラの位置を移動
		transform.position = 
			new Vector3(0, transform.position.y, m_UnityChan.transform.position.z - difference);
	}
}