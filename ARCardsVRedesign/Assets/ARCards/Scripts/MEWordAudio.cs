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

	private AudioSource clickSource;
	
	public AudioSource ClickSource
	{
		get
		{
			if(clickSource == null)
				throw new ArgumentNullException("clickSource", "AudioSource:ClickSource is null");
			else 
				return clickSource;
		}
		
		set
		{
			clickSource = value;
		}
	}

	public MEWordAudio(string name)
	{
		Name = name;
		MEWordAudioClips = new Dictionary<AudioClip, MEAudioType>();
	}

	public void PlayWord(MEAudioType mel)
	{
		foreach(KeyValuePair<AudioClip, MEAudioType> pair in MEWordAudioClips)
		{
			if(pair.Value.Equals(mel))
			{
				Source.PlayOneShot(pair.Key);
			}
		}
	}


//	public void PlaySong(string name)
//	{
//		AudioClip ac = Resources.Load<AudioClip>("MosesEnglish/SongAudios/" + name + "_song");
//		Source.PlayOneShot(ac);
//	}

	public void PlayStateClip(MEAudioType met)
	{
		foreach(KeyValuePair<AudioClip, MEAudioType> pair in MEWordAudioClips)
		{
			if(pair.Value.Equals(met))
			{
				clickSource.Stop();
				clickSource.loop = false;
				clickSource.clip = null;
				clickSource.PlayOneShot(pair.Key);
			}
		}
	}

	public void PlayLoopStateClip(MEAudioType met)
	{
		foreach(KeyValuePair<AudioClip, MEAudioType> pair in MEWordAudioClips)
		{
			if(pair.Value.Equals(met))
			{
				clickSource.clip = pair.Key;
				clickSource.loop = true;
				clickSource.Play();
			}
		}
	}
}
