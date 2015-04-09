using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("BaO/ModelParticle")]
public class ModelParticle : MonoBehaviour, ScaleInterface
{
	public ParticleSystem[] ParSystem;
	public float DelayTime;
	public float WaitColldierTime;
	private float LimitScale = 3;
	private bool Played;

	private float pmaxStartSize;
	private float pmaxStartSpeed;
	private float pminStartSize;
	private float pminStartSpeed;
	private int Parlen;
	void Awake()
	{
		ParSystem = this.gameObject.GetComponentsInChildren<ParticleSystem>(true);
		Parlen = ParSystem.Length;
		pmaxStartSize = ParSystem[0].startSize * LimitScale;
		pmaxStartSpeed = ParSystem[0].startSpeed * LimitScale;

		pminStartSize = ParSystem[0].startSize;
		pminStartSpeed = ParSystem[0].startSpeed;
	}

	public void OnEnlarge()
	{
		for(int i = 0; i < Parlen; i ++)
		{
			//需要限制缩放系数
			if(ParSystem[i].startSize * 1.05f < pmaxStartSize)
			{
				ParSystem[i].startSize = ParSystem[i].startSize * 1.05f;
				ParSystem[i].startSpeed = ParSystem[i].startSpeed * 1.05f;
			}
			else
			{
				ParSystem[i].startSize = pmaxStartSize;
				ParSystem[i].startSpeed = pmaxStartSpeed;
			}
		}
	}

	public void OnZoom()
	{
		for(int i = 0; i < Parlen; i ++)
		{
			//需要限制缩放系数
			if(ParSystem[i].startSize * 0.95f > pminStartSize)
			{
				ParSystem[i].startSize = ParSystem[i].startSize * 0.95f;
				ParSystem[i].startSpeed = ParSystem[i].startSpeed * 0.95f;
			}
			else
			{
				ParSystem[i].startSize = pminStartSize;
				ParSystem[i].startSpeed = pminStartSpeed;
			}
		}
	}

	public void OnplayClick()
	{
		if(!Played)
		{
			Played = true;
			StartCoroutine(PlayoneParticle());
		}
	}

	IEnumerator PlayoneParticle()
	{
		yield return new WaitForSeconds(DelayTime);
		for(int i = 0; i < Parlen; i ++)
		{
			ParSystem[i].Play();
		}
		yield return new WaitForSeconds(WaitColldierTime);
		Played = false;
	}
}

