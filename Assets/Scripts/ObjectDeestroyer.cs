using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeestroyer : MonoBehaviour {
	/// <summary>
	/// 何かにあたったらオブジェクトを削除する
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag" ||
			other.gameObject.tag == "CoinTag")
		{
			Destroy(other.gameObject);
		}
	}
}
