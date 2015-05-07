using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreeLoadingManager : MonoBehaviour 
{
	public UIProgressBar mprohressbar;
	public UILabel mlabel;
	private AsyncOperation async;

	private List<string> Tips = new List<string> {"还不过瘾?购买完整版更多精彩!", "好玩的东西要和大家分享哦!"};

	void Start () 
	{
		RandomaTip();
		StartCoroutine(LoadScene("ME_Redesign"));
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
		int i = UnityEngine.Random.Range(0, 2);
		mlabel.text = Tips[i];
	}
}
