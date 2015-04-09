using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

enum ActivateError
{
	errorLength = 0,
	errorCode,
	errorOutofrange,
	errorTimeout,
	errorFail,
}

enum ActivateSuccess
{
	acOne = 0,
	acTwo,
	acThree,
	acFour,
	acFive,
}


public class IndexManager : MonoBehaviour 
{
	public GameObject ActButton;
	public GameObject EntButton;
	public GameObject FreeButton;
	public GameObject toActButon;
	public GameObject BackButton;
	public GameObject InputObj;
	public GameObject NotificationBox;
	public UILabel NotificaionLabel;

	private ActivateError aeCode;
	private ActivateSuccess asCode;
	private string ActivateCodestr = "";
	private UIInput uiInput;
	private Config mConfig;
	private FileInfo fileinfo;
	private string IOSUDID;

	void Awake()
	{
		uiInput = InputObj.GetComponent<UIInput>();
		mConfig = new Config();
		fileinfo = new FileInfo(Path.Combine(Application.persistentDataPath, "config.xml"));
		if(fileinfo.Exists)
		{
			ActivateCodestr = mConfig.ReadACConfig();
			if(ActivateCodestr.Equals(PlayerPrefs.GetString("ACCODE")) && PlayerPrefs.HasKey("ACCODE"))
			{
				mConfig.GetToturial();
				
				ActButton.SetActive(false);
				InputObj.SetActive(false);
				EntButton.SetActive(true);
				BackButton.SetActive(false);
				FreeButton.SetActive(false);
				toActButon.SetActive(false);
			}
			else
			{
				NotificationBoxIn(ActivateError.errorFail);
			}
		}
	}

	public void OnFinshInput()
	{
		ActivateCodestr = uiInput.value;
		Debug.Log("Finsh Input:" + ActivateCodestr);
	}

	public void OnChangeInput()
	{
		ActivateCodestr = uiInput.value;
	}

	public void OnActivate()
	{
		if(ActivateCodestr.Length != 10)
		{
			NotificationBoxIn(ActivateError.errorLength);
		}
		else
		{
			Debug.Log("try to Activate");
			StartCoroutine(DoActivate(ActivateCodestr));
		}

	}

	public void OnEntergame()
	{
		Debug.Log("on enter game");
		Application.LoadLevel("Loading");
	}

	public void OnFreegame()
	{
		Debug.Log("on the free game");
		Application.LoadLevel("FreeLoading");
	}

	public void OntoActButton()
	{
		toActButon.SetActive(false);
		InputObj.SetActive(true);
		ActButton.SetActive(true);
		BackButton.SetActive(true);
		FreeButton.SetActive(false);
	}

	public void OnBackButton()
	{
		BackButton.SetActive(false);
		toActButon.SetActive(true);
		FreeButton.SetActive(true);
		InputObj.SetActive(false);
		ActButton.SetActive(false);
	}

	public void OnBoxButton()
	{
		NotificationBox.SetActive(false);
	}

	IEnumerator DoActivate(string code)
	{
		Debug.Log(code);
		WWWForm wwwfrom = new WWWForm();
		wwwfrom.AddField("xuliehao", code);
		string value = "";
		#if UNITY_ANDROID
		value = SystemInfo.deviceUniqueIdentifier;
		#elif UNITY_IOS
		Send();
		yield return new WaitForEndOfFrame();
		Debug.Log("ios" + IOSUDID);
		value = IOSUDID;
		#elif UNITY_EDITOR
		value = SystemInfo.deviceUniqueIdentifier;
		#endif
		wwwfrom.AddField("jiqima", value);
		WWW www = new WWW("http://121.199.35.173:8080/xihuan22dcloud/services/Kapianservice/serviceYanzhengxuliehao", wwwfrom);
		yield return www;

		if(DoSplitRequest(www.text.ToString()))
		{
//			Debug.Log("SUCC");
			PlayerPrefs.SetString("ACCODE", code);
			if(!fileinfo.Exists)
			{
				mConfig.xmlInit();
				mConfig.SaveConfig(code, "true");
				mConfig.GetToturial();
			}
			else
			{
				mConfig.UpdateACConfig(code);
				mConfig.GetToturial();
			}

//			yield return new WaitForSeconds(1.0f);
			ActButton.SetActive(false);
			InputObj.SetActive(false);
			EntButton.SetActive(true);
			BackButton.SetActive(false);
			FreeButton.SetActive(false);
			toActButon.SetActive(false);
		}
	}

	private void NotificationBoxIn(ActivateError _aecode)
	{
		string value = "";
		switch(_aecode)
		{
		case ActivateError.errorCode:
			value =	@"[f41b23]验证失败![-]" + "\n" + "序列号错误!";
			break;
		case ActivateError.errorLength:
			value = @"[f41b23]验证失败![-]" + "\n" + "序列号长度错误!";
			break;
		case ActivateError.errorOutofrange:
			value = @"[f41b23]验证失败![-]" + "\n" + "序列号激活次数达到上限!";
			break;
		case ActivateError.errorTimeout:
			value = @"[f41b23]验证失败![-]" + "\n" + "连接超时!";
			break;
		case ActivateError.errorFail:
			value = @"[f41b23]验证失败![-]" + "\n" + "需要重新验证!";
			break;
		}
		NotificaionLabel.text = value;
		NotificationBox.SetActive(true);
	}

	private void NotificationBoxIn(ActivateSuccess _ascode)
	{
		string value = "";
		switch(_ascode)
		{
		case ActivateSuccess.acOne:
			value = @"激活成功！" + "\n" + "已激活[f41b23]1[-]台设备" + "\n" + "还有[66FA33]4[-]台可以激活";
			break;
		case ActivateSuccess.acTwo:
			value = @"激活成功！" + "\n" + "已激活[f41b23]2[-]台设备" + "\n" + "还有[66FA33]3[-]台可以激活";
			break;
		case ActivateSuccess.acThree:
			value = @"激活成功！" + "\n" + "已激活[f41b23]3[-]台设备" + "\n" + "还有[66FA33]2[-]台可以激活";
			break;
		case ActivateSuccess.acFour:
			value = @"激活成功！" + "\n" + "已激活[f41b23]4[-]台设备" + "\n" + "还有[66FA33]1[-]台可以激活";
			break;
		case ActivateSuccess.acFive:
			value = @"激活成功！" + "\n" + "已激活[f41b23]5[-]台设备" + "\n" + "还有[66FA33]0[-]台可以激活";
			break;
		}

		NotificaionLabel.text = value;
		NotificationBox.SetActive(true);
	}

#if UNITY_IOS
	[DllImport ("__Internal")]
	private static extern void _send ();
	
	public static void Send()
	{
		_send();
	}

	public void getIOSID(string udid)
	{
		IOSUDID = udid;
	}
#endif
	
	
	private bool DoSplitRequest(string str)
	{
		string tmpstr = "";
		XmlDocument xmld = new XmlDocument();
		Debug.Log(str);

		xmld.Load(new StringReader(str));

		XmlNodeList nodeList = xmld.GetElementsByTagName("ns:return");

		//遍历所有子节点
		foreach (XmlElement element in nodeList)
		{
			if (element.Name.Equals("ns:return"))
			{
				tmpstr = element.InnerText;
			}
		}

		switch(tmpstr)
		{
			case "error":
			NotificationBoxIn(ActivateError.errorCode);
			return false;
			break;

			case "1":
			NotificationBoxIn(ActivateSuccess.acOne);
			return true;
			break;

			case "2":
			NotificationBoxIn(ActivateSuccess.acTwo);
			return true;
			break;

			case "3":
			NotificationBoxIn(ActivateSuccess.acThree);
			return true;
			break;

			case "4":
			NotificationBoxIn(ActivateSuccess.acFour);
			return true;
			break;

			case "5":
			NotificationBoxIn(ActivateSuccess.acFive);
			return true;
			break;

			case "yibangman":
			NotificationBoxIn(ActivateError.errorOutofrange);
			return false;
			break;

			default:
			NotificationBoxIn(ActivateError.errorTimeout);
			return false;
			break;
		}
	}
}
