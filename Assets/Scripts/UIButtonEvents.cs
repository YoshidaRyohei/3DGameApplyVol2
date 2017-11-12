using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEvents : MonoBehaviour {
	[SerializeField]
	private UnityChanControl m_UnityChanControl;

	private bool m_bLeftButtonDown;
	private bool m_bRightButtonDown;

	private void Start()
	{
		m_bLeftButtonDown = m_bRightButtonDown = false;
	}

	private void Update()
	{
		if (m_bLeftButtonDown)
			m_UnityChanControl.LeftMove();
		if (m_bRightButtonDown)
			m_UnityChanControl.RightMove();
	}

	public void LeftDown()
	{
		m_bLeftButtonDown = true;
	}

	public void LeftUp()
	{
		m_bLeftButtonDown = false;
	}

	public void RightDown()
	{
		m_bRightButtonDown = true;
	}

	public void RightUp()
	{
		m_bRightButtonDown = false;
	}
}
