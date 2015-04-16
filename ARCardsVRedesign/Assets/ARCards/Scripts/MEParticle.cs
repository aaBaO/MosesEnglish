using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MEParticle :ScaleInterface 
{
	public string Name;
	public ParticleSystem[] MEParticleSystems;

	private int ScaleLimit = 3;
	private int MEPSLength;
	private float pmaxStartSize;
	private float pmaxStartSpeed;
	private float pminStartSize;
	private float pminStartSpeed;
	
	public MEParticle(string name)
	{
		Name = name;
		MEPSLength = 0;
		pmaxStartSize = 0;
		pmaxStartSpeed = 0;
		pminStartSize = 0;
		pminStartSpeed = 0;
	}

	public void SetParticleParamter()
	{
		MEPSLength = MEParticleSystems.Length;
		pmaxStartSize = MEParticleSystems[0].startSize * ScaleLimit;
		pmaxStartSpeed = MEParticleSystems[0].startSpeed * ScaleLimit;

		pminStartSize = MEParticleSystems[0].startSize;
		pminStartSpeed = MEParticleSystems[0].startSpeed;
	}

	public void Play()
	{
		for(int i = 0; i < MEPSLength; i++)
		{
			if(!MEParticleSystems[i].playOnAwake)
				MEParticleSystems[i].Play();
		}
	}

	public void OnEnlarge()
	{
		for(int i = 0; i < MEPSLength; i ++)
		{
			//需要限制缩放系数
			if(MEParticleSystems[i].startSize * 1.05f < pmaxStartSize)
			{
				MEParticleSystems[i].startSize = MEParticleSystems[i].startSize * 1.05f;
				MEParticleSystems[i].startSpeed = MEParticleSystems[i].startSpeed * 1.05f;
			}
			else
			{
				MEParticleSystems[i].startSize = pmaxStartSize;
				MEParticleSystems[i].startSpeed = pmaxStartSpeed;
			}
		}
	}

	public void OnZoom()
	{
		for(int i = 0; i < MEPSLength; i ++)
		{
			//需要限制缩放系数
			if(MEParticleSystems[i].startSize * 0.95f > pminStartSize)
			{
				MEParticleSystems[i].startSize = MEParticleSystems[i].startSize * 0.95f;
				MEParticleSystems[i].startSpeed = MEParticleSystems[i].startSpeed * 0.95f;
			}
			else
			{
				MEParticleSystems[i].startSize = pminStartSize;
				MEParticleSystems[i].startSpeed = pminStartSpeed;
			}
		}
	}


	public void OnplayClick()
	{

	}
}
