using UnityEngine;
using System.Collections;

public class AutoDestructParticle : MonoBehaviour 
{
	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}
	
	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!this.GetComponent<ParticleSystem>().IsAlive(true))
			{
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}
