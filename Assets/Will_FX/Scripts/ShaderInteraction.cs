using UnityEngine;
using System.Collections;

public class ShaderInteraction : MonoBehaviour {
	protected bool Selected = false;

	protected Renderer shader;
	protected Color IdleColor;
	protected Color ActiveColor;
	protected float IdleGlow;
	protected float Intensity;

	void Start () {
//		shader = GetComponent<Renderer>();
	}
	
	void Update () {
		//test with button press
		if (Input.GetKeyDown (KeyCode.Space))
			Selected = !Selected;
		//Change to mouse hover function

		//UpdateMaterial ();
	}
}
