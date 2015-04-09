using UnityEngine;
using System.Collections;

public class ProtectAudio : MonoBehaviour 
{
	public AudioSource audiosource;
	public AudioClip[] audioclips;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine("ProtectAudiomanager");

	}

	IEnumerator ProtectAudiomanager()
	{
		while(true)
		{
			int randomi = UnityEngine.Random.Range(0, 3);
			audiosource.clip = audioclips[randomi];
			audiosource.Play();
			Debug.Log(audioclips[randomi].length);
			yield return new WaitForSeconds(audioclips[randomi].length);
		}
	}
}
