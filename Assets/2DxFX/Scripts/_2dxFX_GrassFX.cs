﻿//////////////////////////////////////////////
/// 2DxFX - 2D SPRITE FX - by VETASOFT 2015 //
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu ("2DxFX/Standard/GrassFX")]
[System.Serializable]
public class _2dxFX_GrassFX : MonoBehaviour
{
	[HideInInspector] public Material ForceMaterial;
	[HideInInspector] public bool ActiveChange=true;
	private string shader = "2DxFX/Standard/GrassFX";
	[HideInInspector] [Range(0, 1)] public float _Alpha = 1f;

	[HideInInspector] [Range(0.0f, 4f)] public float Heat = 1.0f;
	[HideInInspector] [Range(0.0f, 4f)] public float Speed = 1.0f;

	private AnimationCurve Wind;
	private float WindTime1=0;

	[HideInInspector] public int ShaderChange=0;
	Material tempMaterial;
	Material defaultMaterial;
	Image CanvasImage;

	
	void Awake()
	{
		if (this.gameObject.GetComponent<Image> () != null) 
		{
			CanvasImage = this.gameObject.GetComponent<Image> ();
		}
	}
	void Start ()
	{  
		ShaderChange = 0;

		// VS AnimationCurve To C# for Wind
		// Put this code on 'Start' or 'Awake' fonction
		
		Wind= new AnimationCurve();
		Wind.AddKey(0,0);
		Wind.keys[0].tangentMode = 0;
		Wind.keys[0].inTangent = 0f;
		Wind.keys[0].outTangent = 0f;
		
		Wind.AddKey(0.1004994f,0.06637689f);
		Wind.keys[1].tangentMode = 0;
		Wind.keys[1].inTangent = 0f;
		Wind.keys[1].outTangent = 0f;
		
		Wind.AddKey(0.2430963f,-0.06465532f);
		Wind.keys[2].tangentMode = 0;
		Wind.keys[2].inTangent = -0.07599592f;
		Wind.keys[2].outTangent = -0.07599592f;
		
		Wind.AddKey(0.3425266f,0.02290122f);
		Wind.keys[3].tangentMode = 0;
		Wind.keys[3].inTangent = 0.03580004f;
		Wind.keys[3].outTangent = 0.03580004f;
		
		Wind.AddKey(0.4246872f,-0.02232522f);
		Wind.keys[4].tangentMode = 0;
		Wind.keys[4].inTangent = -0.006025657f;
		Wind.keys[4].outTangent = -0.006025657f;
		
		Wind.AddKey(0.5104106f,0.1647801f);
		Wind.keys[5].tangentMode = 0;
		Wind.keys[5].inTangent = 0.02981164f;
		Wind.keys[5].outTangent = 0.02981164f;
		
		Wind.AddKey(0.6082056f,-0.04679203f);
		Wind.keys[6].tangentMode = 0;
		Wind.keys[6].inTangent = -0.3176928f;
		Wind.keys[6].outTangent = -0.3176928f;
		
		Wind.AddKey(0.7794942f,0.2234365f);
		Wind.keys[7].tangentMode = 0;
		Wind.keys[7].inTangent = 0.2063811f;
		Wind.keys[7].outTangent = 0.2063811f;
		
		Wind.AddKey(0.8546611f,-0.003165513f);
		Wind.keys[8].tangentMode = 0;
		Wind.keys[8].inTangent = 0.02264977f;
		Wind.keys[8].outTangent = 0.02264977f;
		
		Wind.AddKey(1.022495f,-0.07358052f);
		Wind.keys[9].tangentMode = 0;
		Wind.keys[9].inTangent = 2.450916f;
		Wind.keys[9].outTangent = 2.450916f;
		
		Wind.AddKey(1.250894f,-0.1813075f);
		Wind.keys[10].tangentMode = 0;
		Wind.keys[10].inTangent = 0.02214685f;
		Wind.keys[10].outTangent = 0.02214685f;
		
		Wind.AddKey(1.369877f,-0.06861454f);
		Wind.keys[11].tangentMode = 0;
		Wind.keys[11].inTangent = -1.860534f;
		Wind.keys[11].outTangent = -1.860534f;
		
		Wind.AddKey(1.484951f,-0.1543293f);
		Wind.keys[12].tangentMode = 0;
		Wind.keys[12].inTangent = 0.0602752f;
		Wind.keys[12].outTangent = 0.0602752f;
		
		Wind.AddKey(1.583562f,0.100938f);
		Wind.keys[13].tangentMode = 0;
		Wind.keys[13].inTangent = 0.08665025f;
		Wind.keys[13].outTangent = 0.08665025f;
		
		Wind.AddKey(1.687307f,-0.100769f);
		Wind.keys[14].tangentMode = 0;
		Wind.keys[14].inTangent = 0.01110137f;
		Wind.keys[14].outTangent = 0.01110137f;
		
		Wind.AddKey(1.797593f,0.04921142f);
		Wind.keys[15].tangentMode = 0;
		Wind.keys[15].inTangent = 3.407104f;
		Wind.keys[15].outTangent = 3.407104f;
		
		Wind.AddKey(1.927248f,-0.1877219f);
		Wind.keys[16].tangentMode = 0;
		Wind.keys[16].inTangent = -0.001117587f;
		Wind.keys[16].outTangent = -0.001117587f;
		
		Wind.AddKey(2.067694f,0.2742145f);
		Wind.keys[17].tangentMode = 0;
		Wind.keys[17].inTangent = 4.736587f;
		Wind.keys[17].outTangent = 4.736587f;
		
		Wind.AddKey(2.184602f,-0.06127208f);
		Wind.keys[18].tangentMode = 0;
		Wind.keys[18].inTangent = -0.1308322f;
		Wind.keys[18].outTangent = -0.1308322f;
		
		Wind.AddKey(2.305948f,0.1891117f);
		Wind.keys[19].tangentMode = 0;
		Wind.keys[19].inTangent = 0.04030764f;
		Wind.keys[19].outTangent = 0.04030764f;
		
		Wind.AddKey(2.428946f,-0.1695723f);
		Wind.keys[20].tangentMode = 0;
		Wind.keys[20].inTangent = -0.2463162f;
		Wind.keys[20].outTangent = -0.2463162f;
		
		Wind.AddKey(2.55922f,0.0359862f);
		Wind.keys[21].tangentMode = 0;
		Wind.keys[21].inTangent = 0.3967434f;
		Wind.keys[21].outTangent = 0.3967434f;
		
		Wind.AddKey(2.785119f,-0.08398628f);
		Wind.keys[22].tangentMode = 0;
		Wind.keys[22].inTangent = -0.2388284f;
		Wind.keys[22].outTangent = -0.2388284f;
		
		Wind.AddKey(3f,0f);
		Wind.keys[23].tangentMode = 0;
		Wind.keys[23].inTangent = 0f;
		Wind.keys[23].outTangent = 0f;

		Wind.postWrapMode= WrapMode.Loop;
		Wind.preWrapMode= WrapMode.Loop;


	}

 	public void CallUpdate()
	{
		Update ();
	}

	void Update()
	{
		if (this.gameObject.GetComponent<Image> () != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		}		
		if ((ShaderChange == 0) && (ForceMaterial != null)) 
		{
			ShaderChange=1;
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = ForceMaterial;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = ForceMaterial;
			}
			ForceMaterial.hideFlags = HideFlags.None;
			ForceMaterial.shader=Shader.Find(shader);
			
		}
		if ((ForceMaterial == null) && (ShaderChange==1))
		{
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.None;
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = tempMaterial;
			}
			ShaderChange=0;
		}
		
		#if UNITY_EDITOR
		string dfname = "";
		if(this.gameObject.GetComponent<SpriteRenderer>() != null) dfname=this.GetComponent<Renderer>().sharedMaterial.shader.name;
		if(this.gameObject.GetComponent<Image>() != null) 
		{
			Image img = this.gameObject.GetComponent<Image>();
			if (img.material==null)	dfname="Sprites/Default";
		}
		if (dfname == "Sprites/Default")
		{
			ForceMaterial.shader=Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.None;
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = ForceMaterial;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				Image img = this.gameObject.GetComponent<Image>();
				if (img.material==null)
				{
				CanvasImage.material = ForceMaterial;
				}
			}
		}
		#endif
		if (ActiveChange)
		{
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
			this.GetComponent<Renderer>().sharedMaterial.SetFloat("_Alpha", 1-_Alpha);
			this.GetComponent<Renderer>().sharedMaterial.SetFloat("_Distortion", Heat);
			if (Wind!=null) this.GetComponent<Renderer>().sharedMaterial.SetFloat("_Wind", Wind.Evaluate(WindTime1));
			this.GetComponent<Renderer>().sharedMaterial.SetFloat("_Speed", Speed);
			WindTime1 += (Time.deltaTime/8)*Speed;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
			CanvasImage.material.SetFloat("_Alpha", 1-_Alpha);
			CanvasImage.material.SetFloat("_Distortion", Heat);
			if (Wind!=null) CanvasImage.material.SetFloat("_Wind", Wind.Evaluate(WindTime1));
			CanvasImage.material.SetFloat("_Speed", Speed);
			WindTime1 += (Time.deltaTime/8)*Speed;
			}
			
		}
		
	}
	
	void OnDestroy()
	{
	if (this.gameObject.GetComponent<Image> () != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		}
		if ((Application.isPlaying == false) && (Application.isEditor == true)) {
			
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			
			if (gameObject.activeSelf && defaultMaterial!=null) {
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
				this.GetComponent<Renderer>().sharedMaterial.hideFlags = HideFlags.None;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = defaultMaterial;
				CanvasImage.material.hideFlags = HideFlags.None;
			}
		}	
		}
	}
	void OnDisable()
	{ 
	if (this.gameObject.GetComponent<Image> () != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		} 
		if (gameObject.activeSelf && defaultMaterial!=null) {
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
				this.GetComponent<Renderer>().sharedMaterial.hideFlags = HideFlags.None;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = defaultMaterial;
				CanvasImage.material.hideFlags = HideFlags.None;
			}
		}		
	}
	
	void OnEnable()
	{
		if (this.gameObject.GetComponent<Image> () != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		} 
		WindTime1 = 0;
	
		if (defaultMaterial == null) {
			defaultMaterial = new Material(Shader.Find("Sprites/Default"));
			 
			
		}
		if (ForceMaterial==null)
		{
			ActiveChange=true;
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.None;
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = tempMaterial;
			}
		}
		else
		{
			ForceMaterial.shader=Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.None;
			if(this.gameObject.GetComponent<SpriteRenderer>() != null)
			{
				this.GetComponent<Renderer>().sharedMaterial = ForceMaterial;
			}
			else if(this.gameObject.GetComponent<Image>() != null)
			{
				CanvasImage.material = ForceMaterial;
			}
		}
		
	}
}




#if UNITY_EDITOR
[CustomEditor(typeof(_2dxFX_GrassFX)),CanEditMultipleObjects]
public class _2dxFX_GrassFX_Editor : Editor
{
	private SerializedObject m_object;
	
	public void OnEnable()
	{
		 
		m_object = new SerializedObject(targets);
	}
	
	public override void OnInspectorGUI()
	{
		m_object.Update();
		DrawDefaultInspector();
		
		_2dxFX_GrassFX _2dxScript = (_2dxFX_GrassFX)target;
	
		Texture2D icon = Resources.Load ("2dxfxinspector-anim") as Texture2D;
		if (icon)
		{
			Rect r;
			float ih=icon.height;
			float iw=icon.width;
			float result=ih/iw;
			float w=Screen.width;
			result=result*w;
			r = GUILayoutUtility.GetRect(ih, result);
			EditorGUI.DrawTextureTransparent(r,icon);
		}

		EditorGUILayout.PropertyField(m_object.FindProperty("ForceMaterial"), new GUIContent("Shared Material", "Use a unique material, reduce drastically the use of draw call"));
		
		if (_2dxScript.ForceMaterial == null)
		{
			_2dxScript.ActiveChange = true;
		}
		else
		{
			if(GUILayout.Button("Remove Shared Material"))
			{
				_2dxScript.ForceMaterial= null;
				_2dxScript.ShaderChange = 1;
				_2dxScript.ActiveChange = true;
				_2dxScript.CallUpdate();
			}
		
			EditorGUILayout.PropertyField (m_object.FindProperty ("ActiveChange"), new GUIContent ("Change Material Property", "Change The Material Property"));
		}

		if (_2dxScript.ActiveChange)
		{

			EditorGUILayout.BeginVertical("Box");

			Texture2D icone = Resources.Load ("2dxfx-icon-distortion") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("Heat"), new GUIContent("Heat Distortion", icone, "Change the distortion of the heat"));
			icone = Resources.Load ("2dxfx-icon-time") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("Speed"), new GUIContent("Time Speed", icone, "Change the time speed"));
		
			EditorGUILayout.BeginVertical("Box");



			icone = Resources.Load ("2dxfx-icon-fade") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Alpha"), new GUIContent("Fading", icone, "Fade from nothing to showing"));

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
	

		}
		
		m_object.ApplyModifiedProperties();
		
	}
}
#endif