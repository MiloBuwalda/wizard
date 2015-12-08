using UnityEngine;
using System.Collections;

public class LightningboltShield : MonoBehaviour 
{
	#region Variable Declaration
	//variables that can be manipulated to change the effect
	public int numberOfBolts = 11;
	public float impactRadius = 20.0f;
	public float minimumSteps = 20.0f;
	public float arcVariation = 2.0f;
	public float inaccuracy = 1.0f;
	public float alphaFade = 0.1f;
	public GameObject lightningRenderer; //should be automated!
	public float boltSize = 0.1f; //does not do anything yet
	public int minimumBoltInterval = 25;
	public int interBoltInterval = 5;
	
	//other variables
	private Vector3 targetPoint;
	
	//arrays
	private int[] drawDelay;
	private LineRenderer[] LR;
	private GameObject[] lightRend;
	private int[] delayed;
	#endregion
	
	
	// Use this for initialization
	void Start () 
	{
		
		lightRend = new GameObject[numberOfBolts];
		LR = new LineRenderer[numberOfBolts];
		delayed = new int[numberOfBolts];
		drawDelay = new int[numberOfBolts];
		
		//we'll be sytematically creating LineRenderers here
		for (int i = 0; i < numberOfBolts; i++) 
		{
			lightRend[i] = (GameObject)Instantiate (lightningRenderer, transform.position, transform.rotation);
			LR[i] = lightRend[i].GetComponent<LineRenderer>();
			LR[i].SetWidth(boltSize, boltSize);
			delayed[i] = 0;
			if(i==0)
			{
				drawDelay[i] = minimumBoltInterval;
			}
			else
			{
				drawDelay[i] = drawDelay[i-1] + interBoltInterval;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//Cycle through all the LineRenderers
		for (int i = 0; i < numberOfBolts; i++)
		{
			
			//should we wait a bit more before drawing the next bolt?
			if (drawDelay[i] > delayed[i]) {
				delayed[i]++;
				
			} 
			else //we should draw a new bolt! :D
			{ 
				//let's get the starting position of the bolt
				Vector3 lastPoint = transform.position;
				int j = 1;
				
				LR[i].SetPosition(0, transform.position);
				//Create an impact point
				targetPoint = (Random.onUnitSphere * impactRadius) + transform.position;
				targetPoint.z = 0;
				targetPoint.Normalize();
				targetPoint *= impactRadius;
				targetPoint.z += transform.position.z;
				//are we there yet?
				float closeEnough = Vector3.Distance(targetPoint, lastPoint)/(minimumSteps/2);
				while (Vector3.Distance(targetPoint, lastPoint) > closeEnough)
				{
					LR[i].SetVertexCount(j+1);
					Vector3 fwd = targetPoint - lastPoint;
					fwd.Normalize();
					//change the overal direction
					fwd = Randomize(fwd, inaccuracy);
					//change the length
					fwd = fwd * Random.Range(closeEnough * arcVariation, closeEnough);
					fwd = fwd + lastPoint;
					LR[i].SetPosition(j, fwd);
					j++;
					lastPoint = fwd;
				}
				
				delayed[i] = 0;
			}
		}
	}
	
	private Vector3 Randomize(Vector3 v3, float inacc)
	{
		v3 = v3 + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f)) * inacc);
		v3.Normalize ();
		return v3;
	}
}
