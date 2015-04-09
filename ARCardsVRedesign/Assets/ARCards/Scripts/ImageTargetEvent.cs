/*============================================================================== 
 * Copyright (c) 2012-2014 Moses Bao.
 * ==============================================================================*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class ImageTargetEvent : MonoBehaviour,
								ITrackableEventHandler
{
	#region PUBLIC_MEMBER_VARIABLES
	public bool isBeingTracked;
	public GameObject StartPrefab;
	#endregion PUBLIC_MEMBER_VARIABLES
	
	#region PRIVATE_MEMBER_VARIABLES
	private TrackableBehaviour mTrackableBehaviour;
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	#region PUBLIC_METODS
	void Start()
	{
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	
	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region PRIVATE_METHODS
	private void OnTrackingFound()
	{
		isBeingTracked = true;

		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
//		MosesEnglishData.FocusTargetname = mTrackableBehaviour.TrackableName;
		FocusManager.AddTarget(mTrackableBehaviour.TrackableName);
		StartCoroutine("ShowfoundTarget");
	}
	
	
	private void OnTrackingLost()
	{
		isBeingTracked = false;

		ShowModel(false);

		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
//		MosesEnglishData.FocusTargetname = "empty";
		FocusManager.RemoveTarget(mTrackableBehaviour.TrackableName);
	}

	private void ShowstartParticle()
	{
		GameObject particleobj = Instantiate(StartPrefab) as GameObject;
		particleobj.transform.parent = this.gameObject.transform;
		particleobj.transform.localPosition = new Vector3(0, 0, 0);
		particleobj.transform.localScale = new Vector3(1, 1, 1);
	}
	
	private void ShowModel( bool tf)
	{
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
		
		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = tf;
		}
		
		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = tf;
		}
	}

	#endregion // PRIVATE_METHODS

	IEnumerator ShowfoundTarget()
	{
		ShowstartParticle();
		yield return new WaitForSeconds(0.1f);
		ShowModel(true);
	}
}
