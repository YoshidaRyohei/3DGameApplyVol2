using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour {
	#region プレハブ
	[SerializeField]
	private GameObject m_CoinObject;

	[SerializeField]
	private GameObject m_CarObject;

	[SerializeField]
	private GameObject m_CornObject;
	#endregion

	#region プロパティ
	/// <summary>
	/// 生成オブジェクトの親
	/// </summary>
	[SerializeField]
	private Transform m_ParentObject;

	[SerializeField]
	private Transform m_UnityChanTransform;

	/// <summary>
	/// スタートZ
	/// </summary>
	private const float m_fStartPos = -160.0f;

	/// <summary>
	/// 生成視野範囲
	/// </summary>
	private const float m_fCreateCrip = 50.0f;

	/// <summary>
	/// 生成間隔
	/// </summary>
	private const int m_nCreateZRange = 15;

	/// <summary>
	/// 生成範囲（X)
	/// </summary>
	private const float m_fCreateXRange = 3.4f;

	/// <summary>
	/// 前回の生成場所
	/// </summary>
	private float m_vPrevCreatePosZ = -400.0f;
	#endregion

	private void Update()
	{
		if (m_UnityChanTransform.position.z < m_fStartPos - m_fCreateCrip)
			return;

		if (m_UnityChanTransform.position.z < m_vPrevCreatePosZ)
			return;
		
		float fStartPos = m_UnityChanTransform.position.z + m_fCreateCrip;
		m_vPrevCreatePosZ = fStartPos;

		//一定の距離ごとにアイテムを生成
		for (int i = (int)fStartPos; i < (int)fStartPos + m_fCreateCrip; i += m_nCreateZRange)
		{
			//どのアイテムを出すのかをランダムに設定
			if (Random.Range(0, 10) <= 1)
			{
				//コーンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f)
				{
					GameObject cone = Instantiate(m_CornObject) as GameObject;
					cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
					cone.transform.parent = m_ParentObject;
				}
			}
			else
			{
				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++)
				{
					//アイテムの種類を決める
					int item = Random.Range(1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range(-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6)
					{
						//コインを生成
						GameObject coin = Instantiate(m_CoinObject) as GameObject;
						coin.transform.position = new Vector3(m_fCreateXRange * j, coin.transform.position.y, i + offsetZ);
						coin.transform.parent = m_ParentObject;
					}
					else if (7 <= item && item <= 9)
					{
						//車を生成
						GameObject car = Instantiate(m_CarObject) as GameObject;
						car.transform.position = new Vector3(m_fCreateXRange * j, car.transform.position.y, i + offsetZ);
						car.transform.parent = m_ParentObject;
					}
				}
			}
		}
	}
}
