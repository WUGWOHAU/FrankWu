using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popEnemy : MonoBehaviour {

	[SerializeField] private GameObject _enemy = null;
	[SerializeField] private int MAX_CREATE_NUM = 10;
	[SerializeField] private float CREATE_TIME = 3.0f;
    [SerializeField] private float OFFSET_TIME = 0.0f;
	private int _create_num;
	private float _create_time;
	// Use this for initialization
	void Start () {
		_create_num = 0;
		_create_time = -OFFSET_TIME;		
	}
	
	// Update is called once per frame
	void Update () {
		_create_time += Time.deltaTime;

		if ( _create_time > CREATE_TIME ) {
			Instantiate( _enemy, transform.position, Quaternion.identity );
			_create_num++;
			_create_time = 0.0f;
		}

		if ( MAX_CREATE_NUM > 0 &&
			 _create_num > MAX_CREATE_NUM ) {
			Destroy( gameObject );
		}
	}

	public void init( GameObject enemy, int max_create_num, float create_time ) {
		_enemy = enemy;
		MAX_CREATE_NUM = max_create_num;
		CREATE_TIME = create_time;
	}
}
