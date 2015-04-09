using UnityEngine;
using System.Collections;

[AddComponentMenu("BaO/ModelAnimator")]
public class ModelAnimator : MonoBehaviour 
{
	public bool PlayParticle;

	private Animator mAnimator;
	private ModelAudio mAudio;
	private ScaleInterface sInterface;
	private bool hasParticles;
	private bool tmpMosesEnglishVolume = true;

	private static string FocusTargetname;
	// Use this for initialization
	void Awake () 
	{
		if(gameObject.GetComponent<ModelParticle>())
		{
			this.sInterface = gameObject.GetComponent<ModelParticle>();
			hasParticles = true;
		}
		else
		{
			hasParticles = false;
		}

		if(mAnimator == null)
		{
			mAnimator = this.GetComponent<Animator>(); 
			//Debug.Log(mAnimator.name);
		}

		if(mAudio == null)
		{
			mAudio = this.GetComponent<ModelAudio>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			//创建一个射线，该射线从主摄像机中发出，而发出点是鼠标点击的位置
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//创建一个射线信息集
			RaycastHit hit;
			//如果碰到什么，则····
			if (Physics.Raycast(ray, out hit))
			{
				//此时hit就是你点击的物体，你可以为所欲为了，当然hit.point可以显示此时你点击的坐标值
				//print(hit.point);
				//print(hit.transform.name);
				if(hit.transform.name.Equals(this.gameObject.name))
				{
					if(hasParticles && PlayParticle)
					{
						if(mAnimator != null)
							mAnimator.SetTrigger("TouchRun");
						sInterface.OnplayClick();
						mAudio.OnTouchRun();
						StartCoroutine("DisableCollider");
					}
					else
					{
						if(mAnimator != null)
							mAnimator.SetTrigger("TouchRun");
						mAudio.OnTouchRun();
						StartCoroutine("DisableCollider");
					}
				}


			}
		}

		if(!tmpMosesEnglishVolume.Equals(MosesEnglishData.Volume))
		{
			tmpMosesEnglishVolume = MosesEnglishData.Volume;
		}
	}

	IEnumerator DisableCollider()
	{
		gameObject.collider.enabled = false;
		float anilen = mAnimator.GetCurrentAnimationClipState(0).Length;
		//Debug.Log(anilen);
		yield return new WaitForSeconds(2);
		gameObject.collider.enabled = true;
		mAudio.PlayModelAudio();
	}


	//Delegate from FocusManager,intreseted in focuscardname
	public static void SetFocusTarget(string focusname)
	{
		FocusTargetname = focusname;
		Debug.Log("==In ModelAnimator==" + focusname);
	}
}
