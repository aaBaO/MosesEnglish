using UnityEngine;
using System;
using System.Collections;

public class MEAnimation 
{
	private Animator animator;

	public Animator Animator
	{
		get
		{
			if(animator != null)
				return animator;
			else
				throw new ArgumentNullException("animator", "Animator:Animator is null");
		}

		set
		{
			animator = value;
		}
	}

	public string Name;

	public MEAnimation(string name)
	{
		Name = name;
	}

	public void Play(string triggername)
	{
		animator.SetTrigger(triggername);
	}

	public float GetClipTime()
	{
		return animator.GetCurrentAnimatorStateInfo(0).length;
	}

	public bool IsAnimatorState(string statename)
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName(statename);
	}

	public bool Isloop(string statename)
	{
		bool result = false;
//		AnimationInfo[] ais = animator.GetCurrentAnimationClipState(0);
//		if(ais[0].clip != null && ais[0].clip.name.Equals(statename) && ais[0].clip.isLooping)
//			result = true;
		if(animator.GetCurrentAnimatorStateInfo(0).loop && animator.GetCurrentAnimatorStateInfo(0).IsName(statename))
			result = true;
		return result;
	}
}
