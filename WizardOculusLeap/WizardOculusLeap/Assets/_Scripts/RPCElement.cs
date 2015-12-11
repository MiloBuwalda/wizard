using UnityEngine;
using System.Collections;

public class RPCElement : MonoBehaviour {


	public float Speed;

	public float LifeTime;

	// Important for RPC
	double m_CreationTime;
	Vector3 m_StartPosition;
	// NEEDS SOLUTION: Ship m_Owner;
	int m_Owner;
	int m_ProjectileId;

	public int ProjectileId
	{
		get
		{
			return m_ProjectileId;
		}
	}

	public void SetStartPosition( Vector3 position )
	{
		m_StartPosition = position;
	}
	
	public void SetCreationTime( double time )
	{
		m_CreationTime = time;
	}
	
//	public void SetOwner( Ship owner )
//	{
//		m_Owner = owner;
//	}

	public void SetOwner (int owner)
	{
		m_Owner = owner;
	}

	public void SetProjectileId( int id )
	{
		m_ProjectileId = id;
	}
	
	// Update is called once per frame
	void Update () {
		float timePassed = (float)(PhotonNetwork.time - m_CreationTime);
		transform.position = m_StartPosition + transform.forward * Speed * timePassed;

		if (timePassed > LifeTime)
		{
			Destroy(gameObject);
		}
		if (transform.position.y < -1.76f)
		{
			Destroy (gameObject);
		}


	}

	public void OnProjectileHit()
	{
		Destroy( gameObject );
		// Do something special like an effect ^^
	}


	void OnCollisionEnter( Collision collision)
	{
//		if (collision.collider.tag == "Obstacle") {
//			OnProjectileHit();
//		} else if (collision.collider.tag == "Shield") {
//			ShieldElement shieldElement = collision.collider.GetComponent<ShieldElement>();
//
//			if (shieldElement.team != m_owner && 
//			OnProjectileHit();
//		} else if (collision.collider.tag == "Player") {
//
//		}

	}
}
