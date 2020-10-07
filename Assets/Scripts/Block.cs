using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public bool origin;
	public bool connected;
	private Vector3 localPosition;

	private void Awake()
	{
		localPosition = transform.localPosition;
		if (origin)
		{
			connected = true;
		}
	}

	public void DestroyBlock(bool refresh = true)
	{
		GameManager.player.blocks.Remove(localPosition);//playerdeki ilgili block siliniyor

		if (origin)
		{
			GameManager.gm.GameOver();
		}
		else
		{
			if (refresh)
			{
				GameManager.player.RefreshBlocks();
			}

			Destroy(gameObject);
		}
	}

	public void CheckConnection()//player çevre blockları kontorl ediliyor
	{
		connected = true;

		var rightBlock = GameManager.player.BlockByKey(localPosition + new Vector3(1, 0, 0));
		var leftBlock = GameManager.player.BlockByKey(localPosition + new Vector3(-1, 0, 0));
		var topBlock = GameManager.player.BlockByKey(localPosition + new Vector3(0, 0, 1));
		var bottomBlock = GameManager.player.BlockByKey(localPosition + new Vector3(0, 0, -1));

		if (rightBlock && !rightBlock.connected)
		{
			rightBlock.CheckConnection();
		}
		if (leftBlock && !leftBlock.connected)
		{
			leftBlock.CheckConnection();
		}
		if (topBlock && !topBlock.connected)
		{
			topBlock.CheckConnection();
		}
		if (bottomBlock && !bottomBlock.connected)
		{
			bottomBlock.CheckConnection();
		}
	}

}