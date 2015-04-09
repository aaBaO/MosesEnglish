using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FocusManager :MonoBehaviour
{
	public GameObject FocusTargetPrefab;
	public delegate void FocusTarget(string focusname);
	public event FocusTarget focustargetEvent;

	private static bool FocusnameListChanged;
	private static List<string> FoundnameList = new List<string>();
	
	private static List<MEFocusObj> FocusObjList = new List<MEFocusObj>();
	private static bool TodoGetMEFocusObj;

	private static int LoadCount = 0;
	private static int LoadIndex = 0;

	private int MEFocusTargetIndexID = -1;

	private GameObject FocusTargetParticle;
	
	private void Awake()
	{
		MosesEnglishData.FocusTargetname = "empty";
//		Debug.Log(CardInfoCatcher.GetEnglishname("rabbit"));
//		Debug.Log(CardInfoCatcher.GetChinesename("rabbit"));
//		Debug.Log(CardInfoCatcher.GetDefaultscale("rabbit"));
//		Debug.Log(CardInfoCatcher.GetIsAnimal("rabbit"));
		this.focustargetEvent += ModelAnimator.SetFocusTarget;
		this.focustargetEvent += ModelControl.SetFocusTarget;
//		this.focustargetEvent += ModelAudio.SetFocusTarget;
//		this.focustargetEvent += ModelParticle.SetFocusTarget;
	}

	private void Update()
	{
		//Get the focustarget via Click 
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				string tmphitname = hit.transform.name;
				if(focustargetEvent != null && !tmphitname.Equals("Shadow") && !tmphitname.Equals(MosesEnglishData.FocusTargetname))
				{
					MosesEnglishData.FocusTargetname = hit.transform.name;
//					SetFocusName(MosesEnglishData.FocusTargetname);		
					FocusnameListChanged = true;
				}
				else if(focustargetEvent != null && !tmphitname.Equals("Shadow"))
				{
					//When had a target in focus ,play animation , particles, audio

					MEFocusTargetIndexID = FocusTargetIndexID(tmphitname);
					if(MEFocusTargetIndexID >= 0)
					{
						FocusObjList[MEFocusTargetIndexID].WordAudio.Source = GameObject.Find("Audio Source_Word").GetComponent<AudioSource>();
						FocusObjList[MEFocusTargetIndexID].WordAudio.PlayWord(MEAudioType.Chinese);
						FocusObjList[MEFocusTargetIndexID].Animation.Play("TouchRun");
						FocusObjList[MEFocusTargetIndexID].Particle.Play();
					}
				}
			}
		}

		//one FocusObj
		if(TodoGetMEFocusObj)
		{
			TodoGetMEFocusObj = false;
			StartCoroutine(DogetFocusObj(LoadIndex));
//			if(LoadCount > 1)
//				StartCoroutine(DogetFocusObj(LoadIndex + 1));
		}


		//Set focustarget when add a new target at the same time
		if(FocusnameListChanged)
		{
			FocusnameListChanged = false;
			SetFocusName(MosesEnglishData.FocusTargetname);
			SetFocusParticle();
		}
	}
	
	/// <summary>
	/// Adds the name of the target to FoundnameList
	/// </summary>
	/// <param name="findtarget">Focus target name</param>
	public static void AddTarget(string findtarget)
	{
		FoundnameList.Add(findtarget);
		MosesEnglishData.FocusTargetname = findtarget;

		LoadCount = 0;

		//FocusObj,one model,two models(letter have a model)
		if(findtarget.Contains("letter"))
		{
			LoadCount = 2;
			FocusObjList.Add(new MEFocusObj(MosesEnglishData.FocusTargetname));
			FocusObjList.Add(new MEFocusObj(CardInfoCatcher.GetLetterReferenceModel(findtarget)));

			if(LoadIndex < FocusObjList.Count - 2)
				LoadIndex++;
			TodoGetMEFocusObj = true;
		}
		else
		{
			LoadCount = 1;
			FocusObjList.Add(new MEFocusObj(MosesEnglishData.FocusTargetname));

			if(LoadIndex < FocusObjList.Count - 1)
				LoadIndex++;
			TodoGetMEFocusObj = true;
		}

		FocusnameListChanged = true;
	}

	/// <summary>
	/// Removes the name of the target from FoundnameList
	/// </summary>
	/// <param name="findtarget">Remove target's name</param>
	public static void RemoveTarget(string findtarget)
	{
		FoundnameList.Remove(findtarget);

		//Remove target from audio objects list,and set object null
		if(findtarget.Contains("letter"))
		{
			//remove two objects
			int tempindex_a = -1;
			int tempindex_b = -1;
			foreach(MEFocusObj mefo in FocusObjList)
			{
				if(mefo.Name.Equals(findtarget))
				{
					tempindex_a = FocusObjList.IndexOf(mefo);
				}
				else if(mefo.Name.Equals(CardInfoCatcher.GetLetterReferenceModel(findtarget)))
				{
					tempindex_b = FocusObjList.IndexOf(mefo);
				}
			}

			if(tempindex_a > tempindex_b)
			{
				FocusObjList[tempindex_a] = null;
				FocusObjList.Remove(FocusObjList[tempindex_a]);

				FocusObjList[tempindex_b] = null;
				FocusObjList.Remove(FocusObjList[tempindex_b]);

				LoadIndex -= 2;
			}
			else if(tempindex_a < tempindex_b)
			{
				FocusObjList[tempindex_b] = null;
				FocusObjList.Remove(FocusObjList[tempindex_b]);
				
				FocusObjList[tempindex_a] = null;
				FocusObjList.Remove(FocusObjList[tempindex_a]);
				
				LoadIndex -= 2;
			}
		}
		else
		{
			//remove one object
			foreach(MEFocusObj mefo in FocusObjList)
			{
				if(mefo.Name.Equals(findtarget))
				{
					int tmpindex = FocusObjList.IndexOf(mefo);

					FocusObjList[tmpindex] = null;
					FocusObjList.Remove(FocusObjList[tmpindex]);

					LoadIndex -= 1;
				}
			}

		}


		if(FoundnameList.Count == 0)
		{
			MosesEnglishData.FocusTargetname = "empty";
			LoadIndex = 0;
		}
		else
		{
			MosesEnglishData.FocusTargetname = FoundnameList[0];
		}
		FocusnameListChanged = true;
	}

	/// <summary>
	/// Trigger the event
	/// </summary>
	/// <param name="focusname">Focusname.</param>
	private void SetFocusName(string focusname)
	{
		focustargetEvent(focusname);
	}

	/// <summary>
	/// Dogets the focus object.
	/// </summary>
	/// <returns>The focus object.</returns>
	/// <param name="index">Index.</param>
	IEnumerator DogetFocusObj(int index)
	{
		while(true)
		{
			if(FocusObjList.Count == 0)
			{
				Debug.Log("FocusObjlist is null");
//				continue;
			}
			else
			{
				for(int i = 0; i < LoadCount; i++)
				{
					index = index + i;
					if(FocusObjList[index] == null)
					{
						break;
					}
					else
					{
						string objname = FocusObjList[index].Name;
						
						AudioClip enac = Resources.Load<AudioClip>("MosesEnglish/WordAudios/" + objname + "_en");
						if(enac != null)
							FocusObjList[index].WordAudio.MEWordAudioClips[enac] = MEAudioType.English;
						
						AudioClip cnac = Resources.Load<AudioClip>("MosesEnglish/WordAudios/" + objname + "_cn");
						if(cnac != null)
							FocusObjList[index].WordAudio.MEWordAudioClips[cnac] = MEAudioType.Chinese;
						
						GameObject[] gameobjs;
						gameobjs = GameObject.FindGameObjectsWithTag("Model");
						foreach(GameObject go in gameobjs)
						{
							if(go.name.Equals(objname))
							{
								FocusObjList[index].Animation.Animator = go.GetComponent<Animator>();
								FocusObjList[index].Particle.MEParticleSystems = go.GetComponentsInChildren<ParticleSystem>(true);
								if(FocusObjList[index].Particle.MEParticleSystems.Length > 0 && FocusObjList[index].Particle.MEParticleSystems[0] != null)
									FocusObjList[index].Particle.SetParticleParamter();
							}
						}

					}
				}
				break;
			}
			yield return 0;
		}
	}

	public int FocusTargetIndexID(string focusname)
	{
		int tmpindex = -1;
		//all indexid in lists are same
		foreach(MEFocusObj mefo in FocusObjList )
		{
			if(mefo.Name.Equals(focusname))
			{
				tmpindex = FocusObjList.IndexOf(mefo);
			}
		}

		return tmpindex;
	}

	public ScaleInterface GetParticleSystemInterface(string focusname)
	{
		int tmpindex;

		tmpindex = FocusTargetIndexID(focusname);

		return FocusObjList[tmpindex].Particle;
	}

	public void SetFocusParticle()
	{
		if(FocusTargetParticle == null)
		{
			FocusTargetParticle = Instantiate(FocusTargetPrefab) as GameObject;
		}
		GameObject[] gameobjs;
		gameobjs = GameObject.FindGameObjectsWithTag("Model");
		foreach(GameObject go in gameobjs)
		{
			if(go.name.Equals(MosesEnglishData.FocusTargetname))
			{
				FocusTargetParticle.transform.parent = go.transform;
				FocusTargetParticle.transform.localPosition = new Vector3(0, 0, 0);
				FocusTargetParticle.transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
}
