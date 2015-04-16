using UnityEngine;
using System.Collections;

[AddComponentMenu("BaO/ModelAudio")]
public class ModelAudio : MonoBehaviour
{
	public AudioSource asModel;
	public AudioSource asWord;
	public AudioClip[] acModel;

//	public void OnTouchRun () 
//	{
//		if(asWord != null)
//		{
//			if(acModel[MosesEnglishData.Language] != null)
//				asWord.PlayOneShot(acModel[MosesEnglishData.Language]);
//		}
//
//		if(asModel != null)
//		{
//			if(acModel[3] != null)
//			{
//				asModel.loop = false;
//				asModel.PlayOneShot(acModel[3]);
//			}
//		}
//	}

	public void OninTarget()
	{
		asModel.enabled = true;
		asWord.enabled = true;
		if(acModel[2] != null)
		{
			asModel.loop = true;
			asModel.clip = acModel[2];
			asModel.Play();
		}
	}

	public void OndisTarget()
	{
		if(acModel[2] != null)
		{
			asModel.Stop();
			asModel.loop = false;
			asModel.clip = null;
		}
		asModel.enabled = false;
		asWord.enabled = false;
	}
	
	public void PlayModelAudio()
	{
		asModel.loop = true;
		asModel.Play();
	}

	public void SetAudioSource(bool tf)
	{
		asModel.enabled = tf;
		asWord.enabled = tf;
	}
	
}
