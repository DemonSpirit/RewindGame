﻿using UnityEngine;
using System.Collections;

public class Table : MouseInteract {
	public Color Color = new Color(0.733f, 0.447f, 0.224f);
	public Color Glow = new Color(1.000f, 0.847f, 0.733f);
	public float MinGlow = 4.6f;
	public float GlowDifference = 2.0f; 

	void Start () {
		IdleColor = Color;
		ActiveColor = Glow;
		IdleGlow = MinGlow;
		Intensity = GlowDifference;

		shader = GetComponent<Renderer>();
	}
	
	void Update () {
	   UpdateMaterial();
        Intensity = GlowDifference;
	}
    
    void UpdateMaterial (){
		shader.material.SetColor("_RimColor", true ? ActiveColor : IdleColor);
		shader.material.SetFloat("_RimPower", true ? IdleGlow - Intensity : IdleGlow);
	}
}
