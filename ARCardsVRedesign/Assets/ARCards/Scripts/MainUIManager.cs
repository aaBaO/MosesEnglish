using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class MainUIManager : MonoBehaviour
{
#region WordDictionary
	public static Dictionary<string, string> Cardsname = new Dictionary<string, string> (56)
	{
		{
			"rabbit",
			"兔子"
		},
		{
			"bus",
			"公交车"
		},		
		{
			"potato",
			"土豆"
		},		
		{
			"hair",
			"头发"
		},		
		{
			"boat",
			"小船"
		},	
//		1
		{
			"chick",
			"小鸡"
		},		
		{
			"cap",
			"帽子"
		},		
		{
			"dinosaur",
			"恐龙"
		},		
		{
			"hand",
			"手"
		},		
		{
			"cup",
			"杯子"
		},		
//		2
		{
			"orange",
			"橙子"
		},		
		{
			"balloon",
			"气球"
		},		
		{
			"firetruck",
			"消防车"
		},		
		{
			"train",
			"火车"
		},		
		{
			"panda",
			"熊猫"
		},	
//		3
		{
			"teeth",
			"牙齿"
		},		
		{
			"dog",
			"狗"
		},		
		{
			"lion",
			"狮子"
		},		
		{
			"pig",
			"猪"
		},		
		{
			"monkey",
			"猴子"
		},	
//		4
		{
			"doll",
			"玩具娃娃"
		},	
		{
			"ball",
			"球"
		},		
		{
			"plate",
			"盘子"
		},		
		{
			"eye",
			"眼睛"
		},		
		{
			"bowl",
			"碗"
		},	
//		5
		{
			"candy",
			"糖果"
		},		
		{
			"paper",
			"纸"
		},	
		{
			"tiger",
			"老虎"
		},		
		{
			"ear",
			"耳朵"
		},		
		{
			"meat",
			"肉"
		},
//		6
		{
			"tummy",
			"肚子"
		},
		{
			"foot",
			"脚"
		},
		{
			"face",
			"脸"
		},
		{
			"bike",
			"自行车"
		},
		{
			"apple",
			"苹果"
		},
//		7
		{
			"carrot",
			"胡萝卜"
		},
		{
			"cake",
			"蛋糕"
		},
		{
			"kangaroo",
			"袋鼠"
		},
		{
			"socks",
			"袜子"
		},
		{
			"pants",
			"裤子"
		},
//		8
		{
			"tomato",
			"西红柿"
		},
		{
			"elephant",
			"象"
		},
		{
			"ship",
			"轮船"
		},
		{
			"dress",
			"连衣裙"
		},
		{
			"key",
			"钥匙"
		},
//		9
		{
			"umbrella",
			"雨伞"
		},
		{
			"frog",
			"青蛙"
		},
		{
			"plane",
			"飞机"
		},
		{
			"horse",
			"马"
		},
		{
			"fish",
			"鱼"
		},
//		10
		{
			"fish2",
			"鱼肉"
		},
		{
			"bird",
			"鸟"
		},
		{
			"egg",
			"鸡蛋"
		},
		{
			"duck",
			"鸭子"
		},
		{
			"nose",
			"鼻子"
		},
//		11
		{
			"truck",
			"货车"
		},
	};
#endregion
	
	public GameObject MenubuttonsPanel;
	public GameObject HelpWindow;
	public UISlider sliderRotate;
	public GameObject BtnLanguage;
	public GameObject BtnShoworHide;
	public GameObject BtnVolume;
	public UILabel Word;
	public GameObject WordPanel;
	public AudioListener AduListener;
	public UISprite ScanWindow;
	private Animator MenuAni;
	private Animator HelpAni;
	private GameObject FoundObj;
	private Control FoundObjControl;
	private Config mconfig;
	private ModelAudio FoundObjAudio;
	private FileInfo fileinfo;
	void Awake()
	{
		MosesEnglishData.Volume = true;
		MenuAni = MenubuttonsPanel.GetComponent<Animator>();
		HelpAni = HelpWindow.GetComponent<Animator>();
		mconfig = new Config();
		fileinfo = new FileInfo(Path.Combine(Application.persistentDataPath, "config.xml"));
	}

	void Start()
	{
		StartCoroutine("EyePro");
	}

	void Update()
	{
		if(FoundObj == null && !MosesEnglishData.FocusTargetname.Equals("empty"))
		{
			FoundObj = GameObject.Find(MosesEnglishData.FocusTargetname);
		}
		else if(FoundObj!= null && FoundObjAudio == null)
		{
			ScanWindow.enabled = false;
			FoundObjAudio = FoundObj.GetComponent<ModelAudio>();
			FoundObjAudio.OninTarget();
		}
		
		if(MosesEnglishData.FocusTargetname.Equals("empty"))	
		{
			ScanWindow.enabled = true;
			FoundObj = null;
			if(FoundObjControl != null)
			{
				FoundObjControl = null;
				
			}
			Word.text = "";
			if(FoundObjAudio != null)
			{
				FoundObjAudio.OndisTarget();
				FoundObjAudio = null;
			}
		}

		SetWordLabel();
		SetLanguageButtonSprite();
		SetHideorShowSprite();
		SetVolumeSprite();
	}

	public void OnMenuButton()
	{
		MenuAni.SetTrigger("TouchMenu");
		if(MosesEnglishData.Toturial == true)
		{
			HelpAni.SetTrigger("TouchHelp");
		}
	}

	public void OnCloseMenuButton()
	{
		MenuAni.SetTrigger("TouchMenu");
	}

	public void OnEnlarge()
	{
		if(FoundObj != null)
		{
			if(FoundObjControl == null && FoundObj.GetComponent<Control>())
			{
				FoundObjControl = FoundObj.GetComponent<Control>();
			}

			FoundObjControl.OnLocalEnlarge();
		}
	}

	public void OnZoom()
	{
		if(FoundObj != null)
		{
			if(FoundObjControl == null && FoundObj.GetComponent<Control>())
			{
				FoundObjControl = FoundObj.GetComponent<Control>();
			}

			FoundObjControl.OnLocalZoom();
		}
	}

	public void OnSliderRotation()
	{
		if(FoundObj != null)
		{
			if(FoundObjControl == null && FoundObj.GetComponent<Control>())
			{
				FoundObjControl = FoundObj.GetComponent<Control>();
			}
			
			FoundObjControl.OnLocalRotate(sliderRotate.value);
		}
	}

	public void OnHelpButton()
	{
		HelpAni.SetTrigger("TouchHelp");
	}

	public void OnCloseHelpButton()
	{
		HelpAni.SetTrigger("TouchHelp");
		if(fileinfo.Exists)
			mconfig.UpdateTTConfig("false");
		MosesEnglishData.Toturial = false;
	}

	public void OnLanguageButton()
	{
		if(MosesEnglishData.Language == 1)
			MosesEnglishData.Language = 0;
		else
			MosesEnglishData.Language = 1;
	}

	public void OnHideorShowButton()
	{
		MosesEnglishData.Hideword = !MosesEnglishData.Hideword;
		if(MosesEnglishData.Hideword)
			WordPanel.SetActive(false);
		else
			WordPanel.SetActive(true);
	}

	public void OnVolumeButton()
	{
		MosesEnglishData.Volume = !MosesEnglishData.Volume;
	}

	public void OnBackButton()
	{
		Application.LoadLevelAsync("Index");
	}

	private void SetLanguageButtonSprite()
	{
		if(MosesEnglishData.Language == 0)
			BtnLanguage.GetComponentInChildren<UISprite>().spriteName = "english";
		else
			BtnLanguage.GetComponentInChildren<UISprite>().spriteName = "chinese";

	}

	private void SetWordLabel()
	{
		if(MosesEnglishData.FocusTargetname.Length != 0)
		{
			if(!MosesEnglishData.FocusTargetname.Equals("empty"))
			{
				foreach (KeyValuePair<string, string> pairvalue in Cardsname)  
				{    	
					if(pairvalue.Key.Equals(MosesEnglishData.FocusTargetname))
					{
						if(MosesEnglishData.Language == 0)
						{
							if(pairvalue.Key.Equals("fish2"))
								Word.text = "fish";
							else 
								Word.text = pairvalue.Key;
						}
						else
							Word.text = pairvalue.Value;
	
//						Debug.Log("TargetID:" + pairvalue.Key + "--Value:" + pairvalue.Value);  
					}
				}  
			}
		}
	}

	private void SetHideorShowSprite()
	{
		if(MosesEnglishData.Hideword)
			BtnShoworHide.GetComponentInChildren<UISprite>().spriteName = "hide";
		else
			BtnShoworHide.GetComponentInChildren<UISprite>().spriteName = "show";
	}

	private void SetVolumeSprite()
	{
		if(MosesEnglishData.Volume)
			BtnVolume.GetComponentInChildren<UISprite>().spriteName = "volume";
		else
			BtnVolume.GetComponentInChildren<UISprite>().spriteName = "volumeno";
	}

	IEnumerator EyePro()
	{
		int count = 0;
		while(count < 900)
		{
			yield return new WaitForSeconds(1);
			count += 1;
		}
		Application.LoadLevelAsync("ProtectLoading");
	}
}
