﻿using UnityEngine;
using System;
using System.Collections;

public enum MEAudioType
{
	Chinese = 0,
	English
}

public class MEAudio
{
	private AudioSource source;

	public AudioSource Source
	{
		get
		{
			if(source == null)
				throw new ArgumentNullException("source", "AudioSource:source is null");
			else	
				return source;
		}
		
		set
		{ 
			source = value;
		}
	}

	public string Name;
	
}
