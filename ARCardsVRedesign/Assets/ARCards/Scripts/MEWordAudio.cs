using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MEWordAudio:MEAudio
{
	public Dictionary<AudioClip, MEAudioType> MEWordAudioClips;
	
	//May not idleloopsource,it will change when have many actions in the feature
	private AudioSource idleloopsource;

	public AudioSource IdleLoopSource
	{
		get
		{
			if(idleloopsource == null)
				throw new ArgumentNullException("idleloopsource", "AudioSource:IdleLoopSource is null");
			else 
				return idleloopsource;
		}

		set
		{
			idleloopsource = value;
		}
	}
	
	public MEWordAudio(string name)
	{
		Name = name;
		MEWordAudioClips = new Dictionary<AudioClip, MEAudioType>();
	}

	public void PlayWord(MEAudioType mel)
	{
//		switch(mel)
//		{
//		case MEAudioType.Chinese:
//			//Find chinese audioclip '_cn'
//			Source.PlayOneShot(MEAudioClips[1]);
//			break;
//		case MEAudioType.English:
//			//Find English audioclip '_en'
//			Source.PlayOneShot(MEAudioClips[0]);
//			break;
//		}
		foreach(KeyValuePair<AudioClip, MEAudioType> pair in MEWordAudioClips)
		{
			if(pair.Value.Equals(mel))
			{
				Source.PlayOneShot(pair.Key);
			}
		}
//		Source.PlayOneShot(MEWordAudioClips[mel]);
	}

}
