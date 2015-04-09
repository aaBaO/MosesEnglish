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
}
