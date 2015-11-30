using UnityEngine;
using System.Collections;

public class ScrollingUVs_Layers : MonoBehaviour 
{
	//public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0 );
	public string textureName = "_MainTex";
	private Renderer textureRenderer;

	private Vector2 uvOffset = Vector2.zero;

	void Start()
	{
		textureRenderer = GetComponent<Renderer> ();
	}


	void LateUpdate() 
	{
		uvOffset.x -= ( uvAnimationRate.x * Time.deltaTime );
		uvOffset.y -= (uvAnimationRate.y * Time.deltaTime);

		if( textureRenderer.enabled )
		{
			textureRenderer.sharedMaterial.SetTextureOffset( textureName, uvOffset);
		}
	}
}