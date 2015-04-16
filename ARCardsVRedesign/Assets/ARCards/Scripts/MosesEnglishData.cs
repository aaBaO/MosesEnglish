using UnityEngine;
using System.Collections;

public enum MEAudioType
{
	English = 0,
	Chinese,
	State1,
	State2,
	State3
}

public class MosesEnglishData
{
	public static string FocusTargetname {set; get;}
	public static bool Toturial {set; get;}
	public static MEAudioType Language {set; get;}
	public static bool Hideword {set; get;}
	public static bool Volume {set; get;}
}

