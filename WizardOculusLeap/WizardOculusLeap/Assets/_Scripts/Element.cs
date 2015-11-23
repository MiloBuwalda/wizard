using UnityEngine;
using System.Collections;

public enum elementType {Fire, Air, Water, Earth, AirFire, AirWater, EarthFire, EarthWater, FireAir, FireEarth, WaterAir, WaterEarth};

public abstract class Element : MonoBehaviour, ICollidable
{
	#region ICollidable implementation

	public void CollideWithElement (Element l1, Element l2)
	{
		throw new System.NotImplementedException ();
	}

	#endregion



}
