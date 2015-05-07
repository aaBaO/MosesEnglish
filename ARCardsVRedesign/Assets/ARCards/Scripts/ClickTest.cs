using UnityEngine;
using System.Collections;

public class ClickTest : MonoBehaviour
{
	public string name = "";

	public Animator mmm;
	void Start()
	{
		StartCoroutine(testreturn());
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit))
			{
				Debug.Log(hit.transform.name);
				if(hit.transform.name.Equals(CardInfoCatcher.GetEnglishname(name)))
				{
					Animator animator = GameObject.Find(name).GetComponent<Animator>();
					animator.SetTrigger("TouchRun");
				}
			}
		}
	}

	IEnumerator testreturn()
	{
		while(mmm == null)
		{
			mmm = this.gameObject.GetComponent<Animator>();
			yield return new WaitForEndOfFrame();
		}

		Debug.Log("get");
	}
}
