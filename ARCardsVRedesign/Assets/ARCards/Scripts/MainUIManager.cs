using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class MainUIManager : MonoBehaviour
{
	public GameObject MenuAnimationOBJ;
	private Animator MenuAni;

	public GameObject MenuButton;
	public GameObject EnlargeButton;
	public GameObject ZoomButton;
	public GameObject HideButton;
	public GameObject VolumeButton;
	public GameObject HelpButton;
	public GameObject CloseButton;
	public GameObject HomeButton;
	public GameObject LanguageButton;

	public GameObject WordPanel;
	public UILabel WordText;

	private ModelControl mModelControl;

	private bool mClickedLanguage;
	private static bool mGetFocusTarget;

	private Config mConfig;
	private FileInfo mFileinfo;

	public GameObject HelpCenterOBJ;
	private Animator HelpCenterAni;

	public GameObject HelpLeftBottonOBJ;
	private Animator HelpLeftBottomAni;

	public GameObject HelpRightBottomOBJ;
	private Animator HelpRightBottomAni;

	private AudioListener mAudioListener;
//	public UISlider sliderRotate;
//	public GameObject BtnLanguage;
//	public GameObject BtnShoworHide;
//	public GameObject BtnVolume;
//	public UILabel Word;
//	public GameObject WordPanel;
//	public AudioListener AduListener;
//	public UISprite ScanWindow;
//	private GameObject FoundObj;
//	private Control FoundObjControl;
//	private ModelAudio FoundObjAudio;
	void Awake()
	{
		mConfig = new Config();
		mFileinfo = new FileInfo(Path.Combine(Application.persistentDataPath, "config.xml"));

		MenuAni = MenuAnimationOBJ.GetComponent<Animator>();
		mModelControl = this.gameObject.GetComponent<ModelControl>();

		MosesEnglishData.Language = MEAudioType.English;
		MosesEnglishData.Volume = true;

		mAudioListener = this.gameObject.GetComponent<AudioListener>();

		UIEventListener.Get(MenuButton).onPress = MenuButtonPress;
		UIEventListener.Get(EnlargeButton).onPress = EnlargeButtonPress;
		UIEventListener.Get(ZoomButton).onPress = ZoomButtonPress;
		UIEventListener.Get(HideButton).onPress = HideButtonPress;
		UIEventListener.Get(VolumeButton).onPress = VolumeButtonPress;
		UIEventListener.Get(HelpButton).onPress = HelpButtonPress;
		UIEventListener.Get(CloseButton).onPress = CloseButtonPress;
		UIEventListener.Get(HomeButton).onPress = HomeButtonPress;
		UIEventListener.Get(LanguageButton).onPress = LanguageButtonPress;

		HelpCenterAni = HelpCenterOBJ.GetComponent<Animator>();
		HelpLeftBottomAni = HelpLeftBottonOBJ.GetComponent<Animator>();
		HelpRightBottomAni = HelpRightBottomOBJ.GetComponent<Animator>();
	}

	void Start()
	{
		StartCoroutine("EyePro");
	
		if(mFileinfo.Exists)
		{
			if(MosesEnglishData.Toturial)
			{
				StartCoroutine(MosesToturial());
			}
		}
		else throw new FileNotFoundException();

//		StartCoroutine("MosesToturial");
	}

	void Update()
	{
//		if(FoundObj == null && !MosesEnglishData.FocusTargetname.Equals("empty"))
//		{
//			FoundObj = GameObject.Find(MosesEnglishData.FocusTargetname);
//		}
//		else if(FoundObj!= null && FoundObjAudio == null)
//		{
//			ScanWindow.enabled = false;
//			FoundObjAudio = FoundObj.GetComponent<ModelAudio>();
//			FoundObjAudio.OninTarget();
//		}
//		
		if(mClickedLanguage)	
		{
			mClickedLanguage = false;
			if(!MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = GetShowWord(MosesEnglishData.FocusTargetname ,MosesEnglishData.Language);
			else if(MosesEnglishData.Language.Equals(MEAudioType.English) && MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = "Moses English";
			else if(MosesEnglishData.Language.Equals(MEAudioType.Chinese) && MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = "摩西英语";
		}

		if(mGetFocusTarget)
		{
			mGetFocusTarget = false;
			if(!MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = GetShowWord(MosesEnglishData.FocusTargetname ,MosesEnglishData.Language);
			else if(MosesEnglishData.Language.Equals(MEAudioType.English) && MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = "Moses English";
			else if(MosesEnglishData.Language.Equals(MEAudioType.Chinese) && MosesEnglishData.FocusTargetname.Equals("empty"))
				WordText.text = "摩西英语";
		}
	}

	private void MenuButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			MenuAni.SetTrigger("Click");
			MenuAni.GetComponentInChildren<UISprite>().spriteName = "menu_d";
		}
		else
		{
			MenuAni.GetComponentInChildren<UISprite>().spriteName = "menu";
		}	
	}

	private void EnlargeButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			EnlargeButton.GetComponentInChildren<UISprite>().spriteName = "enlarge_d";
			if(mModelControl != null)
			{
				mModelControl.OnUIEnlarge();
			}
		}
		else
		{
			EnlargeButton.GetComponentInChildren<UISprite>().spriteName = "enlarge";
		}
	}

	private void ZoomButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			ZoomButton.GetComponentInChildren<UISprite>().spriteName = "zoom_d";
			if(mModelControl != null)
			{
				mModelControl.OnUIZoom();
			}
		}
		else
		{
			ZoomButton.GetComponentInChildren<UISprite>().spriteName = "zoom";
		}
	}

	private void HideButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			if(MosesEnglishData.Hideword)
				HideButton.GetComponentInChildren<UISprite>().spriteName = "hide_d";
			else
				HideButton.GetComponentInChildren<UISprite>().spriteName = "show_d";

			MosesEnglishData.Hideword = !MosesEnglishData.Hideword;
			if(MosesEnglishData.Hideword)
				WordPanel.SetActive(false);
			else
				WordPanel.SetActive(true);
		}
		else
		{
			if(MosesEnglishData.Hideword)
				HideButton.GetComponent<UIButton>().normalSprite = "hide";
			else
				HideButton.GetComponent<UIButton>().normalSprite = "show";
		}
	}

	private void VolumeButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			if(MosesEnglishData.Volume)
				VolumeButton.GetComponentInChildren<UISprite>().spriteName = "volumeno_d";
			else
				VolumeButton.GetComponentInChildren<UISprite>().spriteName = "volume_d";

			MosesEnglishData.Volume = !MosesEnglishData.Volume;
		}
		else
		{
			mAudioListener.enabled = MosesEnglishData.Volume;
			if(MosesEnglishData.Volume)
				VolumeButton.GetComponent<UIButton>().normalSprite = "volume";
			else
				VolumeButton.GetComponent<UIButton>().normalSprite = "volumeno";
		}
	}

	private void HelpButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			HelpButton.GetComponentInChildren<UISprite>().spriteName = "help_d";
			HelpCenterAni.SetTrigger("run");
			HelpLeftBottomAni.SetTrigger("run");
			HelpRightBottomAni.SetTrigger("run");
		}
		else
		{
			HelpButton.GetComponentInChildren<UISprite>().spriteName = "help";
		}
	}

	private void CloseButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			CloseButton.GetComponentInChildren<UISprite>().spriteName = "close_d";
			MenuAni.SetTrigger("Click");
		}
		else
		{
			CloseButton.GetComponentInChildren<UISprite>().spriteName = "close";
		}
	}

	private void HomeButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			HomeButton.GetComponentInChildren<UISprite>().spriteName = "backhome_d";
		}
		else
		{

			HomeButton.GetComponentInChildren<UISprite>().spriteName = "backhome";
			Application.LoadLevelAsync("Index");
		}
	}

	private void LanguageButtonPress(GameObject button, bool isPress)
	{
		if(isPress)
		{
			mClickedLanguage = true;	
			
			if(MosesEnglishData.Language.Equals(MEAudioType.Chinese))
			{
				LanguageButton.GetComponentInChildren<UISprite>().spriteName = "chinese_d";
				MosesEnglishData.Language = MEAudioType.English;
			}
			else
			{
				LanguageButton.GetComponentInChildren<UISprite>().spriteName = "english_d";
				MosesEnglishData.Language = MEAudioType.Chinese;
			}
		}
		else
		{
			if(MosesEnglishData.Language.Equals(MEAudioType.Chinese))
			{
				LanguageButton.GetComponent<UIButton>().normalSprite = "chinese";
			}
			else
			{
				LanguageButton.GetComponent<UIButton>().normalSprite = "english";
			}
		}
	}


	private string GetShowWord(string focusname, MEAudioType mel)
	{
		string tmpstring = "";
		if(focusname.Substring(0,1).Equals("L"))
			focusname = focusname.Substring(1);

		if(mel.Equals(MEAudioType.Chinese))
			tmpstring = CardInfoCatcher.GetChinesename(focusname);
		else if(mel.Equals(MEAudioType.English))
			tmpstring = CardInfoCatcher.GetEnglishname(focusname);

		return tmpstring;
	}

	
	/// <summary>
	/// Delegate from FocusManager,interested in focuscardname
	/// </summary>
	/// <param name="focusname">Focusname.</param>
	public static void SetFocusTarget(string focusname)
	{
		mGetFocusTarget = true;
	}


	IEnumerator MosesToturial()
	{	
		HelpCenterAni.SetTrigger("run");
		HelpLeftBottomAni.SetTrigger("run");
		HelpRightBottomAni.SetTrigger("run");
		MenuButton.GetComponent<UIButton>().normalSprite = "menu_d";
		yield return new WaitForSeconds(3.0f);
		MenuButton.GetComponent<UIButton>().normalSprite = "menu";
		HelpCenterAni.SetTrigger("run");
		HelpLeftBottomAni.SetTrigger("run");
		HelpRightBottomAni.SetTrigger("run");
	}
//	public void OnHelpButton()
//	{
//		HelpAni.SetTrigger("TouchHelp");
//	}
//
//	public void OnCloseHelpButton()
//	{
//		HelpAni.SetTrigger("TouchHelp");
//		if(fileinfo.Exists)
//			mconfig.UpdateTTConfig("false");
//		MosesEnglishData.Toturial = false;
//	}
//
//	public void OnLanguageButton()
//	{
//		if(MosesEnglishData.Language == 1)
//			MosesEnglishData.Language = 0;
//		else
//			MosesEnglishData.Language = 1;
//	}
//
//	public void OnHideorShowButton()
//	{
//		MosesEnglishData.Hideword = !MosesEnglishData.Hideword;
//		if(MosesEnglishData.Hideword)
//			WordPanel.SetActive(false);
//		else
//			WordPanel.SetActive(true);
//	}
//
//	public void OnVolumeButton()
//	{
//		MosesEnglishData.Volume = !MosesEnglishData.Volume;
//	}
//
//	public void OnBackButton()
//	{
//		Application.LoadLevelAsync("Index");
//	}
//
//	private void SetLanguageButtonSprite()
//	{
//		if(MosesEnglishData.Language == 0)
//			BtnLanguage.GetComponentInChildren<UISprite>().spriteName = "english";
//		else
//			BtnLanguage.GetComponentInChildren<UISprite>().spriteName = "chinese";
//
//	}
//
//	private void SetWordLabel()
//	{
//
//	}
//
//	private void SetHideorShowSprite()
//	{
//		if(MosesEnglishData.Hideword)
//			BtnShoworHide.GetComponentInChildren<UISprite>().spriteName = "hide";
//		else
//			BtnShoworHide.GetComponentInChildren<UISprite>().spriteName = "show";
//	}
//
//	private void SetVolumeSprite()
//	{
//		if(MosesEnglishData.Volume)
//			BtnVolume.GetComponentInChildren<UISprite>().spriteName = "volume";
//		else
//			BtnVolume.GetComponentInChildren<UISprite>().spriteName = "volumeno";
//	}
//
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
