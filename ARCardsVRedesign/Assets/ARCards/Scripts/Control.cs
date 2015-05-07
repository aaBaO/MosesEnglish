using UnityEngine;
using System.Collections;


[AddComponentMenu("BaO/Control")]
public class Control : MonoBehaviour
{
//	public Camera mCam;
	public GameObject Model;

	private ScaleInterface sInterface;
	private float MaxScaleLimit = 3;
	private float MinScaleLimit = 1.0f;
	private float ScaleOffset = 1;
	private float PerscaleStep;
	private Vector2 tempPosition0;
	private Vector2 tempPosition1;
	private Vector2 oldPosition0;
	private Vector2 oldPosition1;

	private float myScale;
//	private float mCamFiledofView = 60;
	private float mLocalScale;
	private float yR;

	void Start () 
	{
		this.mLocalScale = this.gameObject.transform.localScale.x;
		this.ScaleOffset = 1 / this.gameObject.transform.localScale.x;
		Input.multiTouchEnabled = true;
		this.sInterface = gameObject.GetComponent<ModelParticle>();
		this.MaxScaleLimit = this.MaxScaleLimit / this.ScaleOffset;
		this.MinScaleLimit = this.MinScaleLimit / this.ScaleOffset;
		this.PerscaleStep = (this.MaxScaleLimit - this.MinScaleLimit) / 6;
//		mCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Model == null)
			this.enabled = false;

		if(Input.touchCount > 1)
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
//					Debug.Log("Enlarge!");
//					if(mCamFiledofView > MaxScaleLimit)
//						mCamFiledofView -= myScale;
//					else 
//						mCamFiledofView = MaxScaleLimit;
						
					this.mLocalScale += GetmyScale();
					if(this.mLocalScale < this.MaxScaleLimit)
					{
						if(this.sInterface != null)
						{
							this.sInterface.OnEnlarge();
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
//					Debug.Log("Zoom!");
//					if(mCamFiledofView < MinScaleLimit)
//						mCamFiledofView += myScale;
//					else
//						mCamFiledofView = MinScaleLimit;	

					this.mLocalScale -= GetmyScale();
					if(this.mLocalScale > this.MinScaleLimit)
					{
						if(this.sInterface != null)
						{
							this.sInterface.OnZoom();
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

//	void LateUpdate()
//	{
//		mCam.fieldOfView = mCamFiledofView;
//	}

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

	public void OnLocalEnlarge()
	{  
		Debug.Log(this.gameObject.name+":onlocaenlarge");

		this.mLocalScale += this.PerscaleStep;
		if(this.mLocalScale < this.MaxScaleLimit)
		{
			if(this.sInterface != null)
			{
				this.sInterface.OnEnlarge();
			}
			Model.transform.localScale += new Vector3(this.PerscaleStep, this.PerscaleStep, this.PerscaleStep);
		}
		else if(this.mLocalScale >= this.MaxScaleLimit)
		{
			this.mLocalScale = this.MaxScaleLimit;
			Model.transform.localScale = new Vector3(MaxScaleLimit, MaxScaleLimit, MaxScaleLimit);
		}
	}
	
	public void OnLocalZoom()
	{
		Debug.Log(this.gameObject.name+":onlocalzoom");

		this.mLocalScale -= this.PerscaleStep;
		if(this.mLocalScale > this.MinScaleLimit)
		{
			if(this.sInterface != null)
			{
				this.sInterface.OnZoom();
			}
			Model.transform.localScale -= new Vector3(this.PerscaleStep, this.PerscaleStep, this.PerscaleStep);
		}
		else if(this.mLocalScale <= this.MinScaleLimit)
		{
			this.mLocalScale = this.MinScaleLimit;
			Model.transform.localScale = new Vector3(MinScaleLimit, MinScaleLimit, MinScaleLimit);
		}
	}

	public void OnLocalRotate(float slidervalue)
	{
		yR = slidervalue * 360;
		Model.transform.localRotation = Quaternion.Euler(0, yR, 0);
	}
}
