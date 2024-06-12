using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using StarterAssetsInput;

public class Minimap : MonoBehaviour
{
	private Transform _player;

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void LateUpdate()
	{
		Vector3 newPosition = _player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;
	}

}
