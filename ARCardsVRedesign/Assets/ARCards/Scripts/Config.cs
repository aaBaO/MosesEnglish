using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class Config
{
	private string filename = @"/config.xml";
	private string path = "";

	private XmlDocument xd = new XmlDocument();
	private XmlElement rootEle;

	public void xmlInit()
	{
		path = Application.persistentDataPath + filename;
		//******Root Element<config>
		rootEle = xd.CreateElement("config");
		XmlDeclaration xdc = xd.CreateXmlDeclaration("1.0", "UTF-8", null);

		xd.AppendChild(xdc);
	}

	public void SaveConfig(string accode, string ttstr)
	{
		//******node1 Element1<activatecode>
		XmlElement ele = xd.CreateElement("activatecode");
		ele.InnerText = accode;
		rootEle.AppendChild(ele);

		//******node1 Element2<toturial>
		ele = xd.CreateElement("toturial");
		ele.InnerText = ttstr;
		rootEle.AppendChild(ele);

		xd.AppendChild(rootEle);
		Debug.Log(path);
		xd.Save(path);
	}

	public string ReadACConfig()
	{
		path = Application.persistentDataPath + filename;
		string tmpstr = "";
		xd.Load(path);

		XmlNodeList nodeList = xd.SelectSingleNode("config").ChildNodes;
		foreach(XmlElement xe in nodeList)
		{
			if(xe.Name.Equals("activatecode"))
				tmpstr = xe.InnerText;
		}
//		Debug.Log("acconfig:" + tmpstr);
		return tmpstr;
	}
	
	public void UpdateACConfig(string accode)
	{
		path = Application.persistentDataPath + filename;
		xd.Load(path);
		XmlNodeList nodelist = xd.SelectSingleNode("config").ChildNodes;
		foreach(XmlElement xe in nodelist)
		{
			if(xe.Name.Equals("activatecode"))
				xe.InnerText = accode;
		}
		xd.Save(path);
	}

	public string ReadTTConfig()
	{
		path = Application.persistentDataPath + filename;
		string tmpstr = "";
		xd.Load(path);
		
		XmlNodeList nodeList = xd.SelectSingleNode("config").ChildNodes;
		foreach(XmlElement xe in nodeList)
		{
			if(xe.Name.Equals("toturial"))
				tmpstr = xe.InnerText;
		}
//		Debug.Log("ttconfig:" + tmpstr);
		return tmpstr;
	}

	public void UpdateTTConfig(string tt)
	{
		path = Application.persistentDataPath + filename;
		xd.Load(path);
		XmlNodeList nodelist = xd.SelectSingleNode("config").ChildNodes;
		foreach(XmlElement xe in nodelist)
		{
			if(xe.Name.Equals("toturial"))
				xe.InnerText = tt;
		}
		xd.Save(path);
	}

	public void GetToturial()
	{
		if(this.ReadTTConfig().Equals("true"))
		{
			MosesEnglishData.Toturial = true;
//			Debug.Log("MosesEnglishData.Toturial:" + MosesEnglishData.Toturial);
		}
		else if(this.ReadTTConfig().Equals("false"))
		{
			MosesEnglishData.Toturial = false;
//			Debug.Log("MosesEnglishData.Toturial:" + MosesEnglishData.Toturial);
		}
	}
}

