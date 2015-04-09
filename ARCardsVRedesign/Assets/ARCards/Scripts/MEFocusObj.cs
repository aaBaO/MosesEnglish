using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MEFocusObj 
{
	public string Name;

	public MEWordAudio WordAudio;
	public MEAnimation Animation;
	public MEParticle Particle;

	public MEFocusObj(string name)
	{
		Name = name;
		WordAudio = new MEWordAudio(name);
		Animation = new MEAnimation(name);
		Particle = new MEParticle(name);
	}


}
