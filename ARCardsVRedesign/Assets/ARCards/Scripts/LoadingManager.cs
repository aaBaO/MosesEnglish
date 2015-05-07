using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingManager : MonoBehaviour 
{
	public UIProgressBar mprohressbar;
	public UILabel mlabel;
	private AsyncOperation async;

	private List<string> Tips = new List<string> {"Tips:小朋友要注意保护眼睛哦", "Tips:以后还有新的教程推出哦", "Tips:好玩的东西要和大家分享哦"};
	

	void Start () 
	{
		RandomaTip();
		StartCoroutine(LoadScene("ME_Redesign_main"));
	}

	IEnumerator LoadScene(string scenename)
	{
		float displayProgress = 0;
		float toProgress = 100;
		
		while(displayProgress < toProgress)
		{
			displayProgress += 1;
			SetProgress(displayProgress);
			yield return new WaitForSeconds(0.02f);
		}
		async = Application.LoadLevelAsync(scenename);
	}

	private void SetProgress(float dis)
	{
		mprohressbar.value = dis / 100;
	}

	private void RandomaTip()
	{
		int i = UnityEngine.Random.Range(0, 3);
		mlabel.text = Tips[i];
	}
}
