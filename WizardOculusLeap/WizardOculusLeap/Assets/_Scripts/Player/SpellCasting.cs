using UnityEngine;
using System.Collections;

public class SpellCasting : Photon.MonoBehaviour {

	/// <summary>
	/// What is the minimal time that should pass between to lasers fired
	/// </summary>
	public float ShootDelay;

	float m_LastShootTime;
	int m_LastProjectileId;


	public Transform m_player;
	Quaternion m_CastingSpawnRotation;

	public bool IsShooting
	{
		get;
		set;
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateIsShooting ();
		UpdateShooting ();
	}

	void UpdateIsShooting()
	{
		IsShooting = Input.GetButton("FireKeyboard") || Input.GetAxisRaw("FireTrigger") >0.1f;
	}

	void UpdateShooting(){
		if (photonView.isMine == false)
			return;
		if (IsShooting == false)
			return;
		//Make sure we wait between shooting lasers
		if( Time.realtimeSinceStartup - m_LastShootTime < ShootDelay )
		{
			return;
		}

		m_LastProjectileId++;

		
		photonView.RPC ("OnShoot", PhotonTargets.All, new object[]{
			GetCastingSpawnPosition (),
			GetCastingSpawnOrientation (),
			this.m_LastProjectileId
		});

	}

	Vector3 GetCastingSpawnPosition ()
	{
		return transform.position + this.m_player.forward * (100 * PhotonNetwork.GetPing () * 0.001f);
	}

	Quaternion GetCastingSpawnOrientation ()
	{
		return this.m_player.rotation;
	}

	public void CreateProjectile(Vector3 position, Quaternion rotation, double createTime, int projectileId)
	{
		m_LastShootTime = Time.realtimeSinceStartup;

		GameObject newCastingObject = (GameObject)Instantiate(Resources.Load<GameObject>("Elements/Fire"), new Vector3(0,-100,0), rotation);
		newCastingObject.name = "ZZZ_" + newCastingObject.name;

		// Now projectile management has to take place. 
		// Keep track of the projectile:
		// creationTime, StartPosition, ProjectileID and Owner!
	}

	[PunRPC]
	public void OnShoot(Vector3 position, Quaternion rotation, int projectileId, PhotonMessageInfo info)
	{
		double timestamp = PhotonNetwork.time;

		if (info != null) {
			timestamp = info.timestamp;
		}
		CreateProjectile (position, rotation, timestamp, projectileId);
	}

	public void SendProjectileHit( int projectileId)
	{
		// Here we can check if we are in offline mode
		photonView.RPC ("OnProjectileHit", PhotonTargets.Others, new object[] {projectileId});

	}

    [PunRPC]
    public void OnProjectileHit( int projectileId )
    {
		//When we receive a projectile hit, it means that the projectile was destroyed on another client and we have to destroy it to
		//So we try to find the appropriate projectile through its ID and then destroy it as well
		
//		m_Projectiles.RemoveAll( item => item == null );
//		
//		ProjectileBase projectile = m_Projectiles.Find( item => item.ProjectileId == projectileId );
//		
//		if( projectile != null )
//		{
//			projectile.OnProjectileHit();
//			m_Projectiles.Remove( projectile );
//		}
	}
}
