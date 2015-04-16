using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

public class CardInfoCatcher 
{
#if UNITY_EDITOR_OSX
	private static string xmlfilepath = "./Assets/StreamingAssets/MosesEnglish/mCardInfo.xml";
#elif UNITY_IOS
	private static string xmlfilepath = Application.streamingAssetsPath + "/MosesEnglish/mCardInfo.xml";
#endif
	private static XElement mCardinfoXML;
	private static IEnumerable<XElement> XELcardinfo;

	/// <summary>
	/// Loads the card infofile.
	/// </summary>
	/// <param name="filepath">Filepath.</param>
	private static void LoadCardInfofile(string filepath)
	{
		mCardinfoXML = XElement.Load(xmlfilepath);
		XELcardinfo = from el in mCardinfoXML.Elements("card")
			select el;
	}

	/// <summary>
	/// Gets the englishname from XML
	/// </summary>
	/// <returns>The englishname.</returns>
	/// <param name="focusname">Focusname.</param>
	public static string GetEnglishname(string focusname)
	{
		LoadCardInfofile(xmlfilepath);
		
		string tmpstr = "empty";

		foreach(XElement xel in XELcardinfo)
		{
			if(xel.Attribute("name").Value == focusname)
			{
				tmpstr = xel.Element("englishname").Value.ToString();
			}
		}
		return tmpstr;
	}

	/// <summary>
	/// Gets the chinesename from XML
	/// </summary>
	/// <returns>The chinesename.</returns>
	/// <param name="focusname">Focusname.</param>
	public static string GetChinesename(string focusname)
	{
		LoadCardInfofile(xmlfilepath);
		
		string tmpstr = "empty";
		
		foreach(XElement xel in XELcardinfo)
		{
			if(xel.Attribute("name").Value == focusname)
			{
				tmpstr = xel.Element("chinesename").Value.ToString();
			}
		}
		return tmpstr;
	}

	/// <summary>
	/// Gets the defaultscale from XML
	/// </summary>
	/// <returns>The defaultscale.</returns>
	/// <param name="focusname">Focusname.</param>
	public static float GetDefaultscale(string focusname)
	{
		LoadCardInfofile(xmlfilepath);
		
		float tmpf = 0;
		
		foreach(XElement xel in XELcardinfo)
		{
			if(xel.Attribute("name").Value == focusname)
			{
				tmpf = Convert.ToSingle(xel.Element("localscale").Value);
			}
		}
		return tmpf;
	}

	/// <summary>
	/// Gets result if is animal from XML
	/// </summary>
	/// <returns>if is animal.</returns>
	/// <param name="focusname">Focusname.</param>
	public static int GetIsAnimal(string focusname)
	{
		LoadCardInfofile(xmlfilepath);
		
		int tmpb = 1;
		
		foreach(XElement xel in XELcardinfo)
		{
			if(xel.Attribute("name").Value == focusname)
			{
				tmpb = Convert.ToInt16(xel.Element("isanimal").Value);
			}
		}
		return tmpb;
	}

	public static string GetLetterReferenceModel(string focusname)
	{
		LoadCardInfofile(xmlfilepath);
		
		string tmpstr = "empty";
		
		foreach(XElement xel in XELcardinfo)
		{
			if(xel.Attribute("name").Value == focusname)
			{
				tmpstr = xel.Element("letterreferencemodel").Value.ToString();
			}
		}
		return tmpstr;
	}
}