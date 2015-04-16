using UnityEngine;
using System.Collections;

public class ModelControl : MonoBehaviour
{
	private FocusManager mFocusManager;
	private GameObject Model;

	private float MaxScaleLimit;
	private float MinScaleLimit;
	private float ScaleOffset;
	private float PerscaleStep;
	private Vector2 tempPosition0;
	private Vector2 tempPosition1;
	private Vector2 oldPosition0;
	private Vector2 oldPosition1;
	
	private float myScale;
	private float mLocalScale;
	private float yRotation;

	private ScaleInterface ParticleSystemScale;
	/// <summary>
	/// The mfocusname.
	/// </summary>
	private static string mFocusname;
	/// <summary>
	/// The getmodelflag.to get a new model
	/// </summary>
	private static bool getmodelflag;

	void Start () 
	{
		mFocusname = "empty";
		getmodelflag = false;
		Input.multiTouchEnabled = true;
		mFocusManager = GameObject.Find("Managers").GetComponent<FocusManager>();
	}

	void Update () 
	{		
		if(!mFocusname.Equals("empty") && getmodelflag)
		{
			getmodelflag = false;
			Model = GameObject.Find(MosesEnglishData.FocusTargetname);
			//should load from a document then save in local document at runtime
			this.mLocalScale = CardInfoCatcher.GetDefaultscale(MosesEnglishData.FocusTargetname);
			this.ScaleOffset = 1 / this.mLocalScale;
			this.MaxScaleLimit = 3.0f / this.ScaleOffset;
			this.MinScaleLimit = 1.0f / this.ScaleOffset;
			this.PerscaleStep = (this.MaxScaleLimit - this.MinScaleLimit) / 6;

			this.yRotation = 180;
			//get particlesysteminterface
			ParticleSystemScale = mFocusManager.GetParticleSystemInterface(MosesEnglishData.FocusTargetname);
		}
		else if(mFocusname.Equals("empty"))
		{
			Model = null;
		}

		if(Input.touchCount == 1 && Model != null)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				yRotation -= Input.GetTouch(0).deltaPosition.x * 0.5f;
				Model.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
			}
		}
		else if(Input.touchCount > 1 && Model != null)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
			{
				oldPosition0 = Input.GetTouch(0).position;
				oldPosition1 = Input.GetTouch(1).position;
			}
			else  if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				tempPosition0 = Input.GetTouch(0).position;
				tempPosition1 = Input.GetTouch(1).position;
				
				
				if(isEnlarge(oldPosition0, oldPosition1, tempPosition0, tempPosition1) == 2)
				{
					this.mLocalScale += GetmyScale();
					if(this.mLocalScale < this.MaxScaleLimit)
					{
						if(this.ParticleSystemScale != null)
						{
							this.ParticleSystemScale.OnEnlarge();
						}
						Model.transform.localScale += new Vector3(GetmyScale(), GetmyScale(), GetmyScale());
					}
					else if(this.mLocalScale >= this.MaxScaleLimit)
					{
						this.mLocalScale = this.MaxScaleLimit;
						Model.transform.localScale = new Vector3(MaxScaleLimit, MaxScaleLimit, MaxScaleLimit);
					}
				}
				else if(isEnlarge(oldPosition0, oldPosition1, tempPosition0, tempPosition1) == 0)
				{
					this.mLocalScale -= GetmyScale();
					if(this.mLocalScale > this.MinScaleLimit)
					{
						if(this.ParticleSystemScale != null)
						{
							this.ParticleSystemScale.OnZoom();
						}
						Model.transform.localScale -= new Vector3(GetmyScale(), GetmyScale(), GetmyScale());
					}
					else if(this.mLocalScale <= this.MinScaleLimit)
					{
						this.mLocalScale = this.MinScaleLimit;
						Model.transform.localScale = new Vector3(MinScaleLimit, MinScaleLimit, MinScaleLimit);
					}
				}
				
				oldPosition0 = tempPosition0;
				oldPosition1 = tempPosition1;
			}
		}
	}

	private int isEnlarge(Vector2 op0, Vector2 op1, Vector2 tp0, Vector2 tp1)
	{
		//oldposition
		//float leng0 = Mathf.Sqrt((op0.x - op1.x)*(op0.x - op1.x) + (op0.y - op1.y)*(op0.y - op1.y));
		float leng0 = Vector2.Distance(op0, op1);
		
		//tempposition
		//float leng1 = Mathf.Sqrt((tp0.x - tp1.x)*(tp0.x - tp0.x) + (tp0.y - tp1.y)*(tp0.y - tp1.y));
		float leng1 = Vector2.Distance(tp0, tp1);
		//Debug.Log("leng1 - leng0:" + (leng1 - leng0));
		
		if(leng0 > leng1)
		{
			return 0;
		}
		else if(leng0 < leng1)
		{
			return 2;
		}
		else
		{
			return 1;
		}
	}

	private float GetmyScale()
	{
		myScale = Mathf.Sqrt(Mathf.Pow(Input.GetTouch(0).deltaPosition.x, 2) + Mathf.Pow(Input.GetTouch(1).deltaPosition.y, 2)) * 0.01f;
		return myScale;
	}

	public void OnUIEnlarge()
	{  
		if(Model != null)
		{
			Debug.Log(Model.name + ":onlocaenlarge");
		
			this.mLocalScale += this.PerscaleStep;
			if(this.mLocalScale < this.MaxScaleLimit)
			{
				if(this.ParticleSystemScale != null)
				{
					this.ParticleSystemScale.OnEnlarge();
				}
				Model.transform.localScale += new Vector3(this.PerscaleStep, this.PerscaleStep, this.PerscaleStep);
			}
			else if(this.mLocalScale >= this.MaxScaleLimit)
			{
				this.mLocalScale = this.MaxScaleLimit;
				Model.transform.localScale = new Vector3(MaxScaleLimit, MaxScaleLimit, MaxScaleLimit);
			}
		}
	}
	
	public void OnUIZoom()
	{
		if(Model != null)
		{
			Debug.Log(Model.name + ":onlocalzoom");
			
			this.mLocalScale -= this.PerscaleStep;
			if(this.mLocalScale > this.MinScaleLimit)
			{
				if(this.ParticleSystemScale != null)
				{
					this.ParticleSystemScale.OnZoom();
				}
				Model.transform.localScale -= new Vector3(this.PerscaleStep, this.PerscaleStep, this.PerscaleStep);
			}
			else if(this.mLocalScale <= this.MinScaleLimit)
			{
				this.mLocalScale = this.MinScaleLimit;
				Model.transform.localScale = new Vector3(MinScaleLimit, MinScaleLimit, MinScaleLimit);
			}
		}
	}


	/// <summary>
	/// Delegate from FocusManager,interested in focuscardname
	/// </summary>
	/// <param name="focusname">Focusname.</param>
	public static void SetFocusTarget(string focusname)
	{
		if(!focusname.Equals("empty"))
		{
			mFocusname = focusname;
			getmodelflag = true;
		}
		else
			mFocusname = "empty";
	}

}
