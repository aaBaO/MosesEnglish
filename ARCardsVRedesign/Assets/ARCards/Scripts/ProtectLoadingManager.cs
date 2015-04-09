using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProtectLoadingManager : MonoBehaviour 
{
	public UILabel mlabel;
	private AsyncOperation async;

	private List<string> Tips = new List<string> {"小朋友要注意保护眼睛哦,先休息5分钟吧~~~"};

	void Start () 
	{
		RandomaTip();
		StartCoroutine(LoadScene("Index"));
	}

	IEnumerator LoadScene(string scenename)
	{
		int count = 0;
		while(count < 300)
		{
			yield return new WaitForSeconds(1f);
			count += 1;
		}
		Application.LoadLevelAsync(scenename);
	}

	private void RandomaTip()
	{
		int i = UnityEngine.Random.Range(0, 1);
		mlabel.text = Tips[i];
	}
}
