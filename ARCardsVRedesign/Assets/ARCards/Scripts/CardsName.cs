/*---------------------------
 * CopyRight 2014 Moses BaO
 --------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;

public class CardsName
{
	#region WordDictionary
	public static Dictionary<string, string> CardsnameDic = new Dictionary<string, string> (56)
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
	
	public string Find(string targetname)
	{	
		string returnstr = "";
		if(MosesEnglishData.Language == 0)
		{
			returnstr = CardInfoCatcher.GetEnglishname(targetname);
		}
		else
		{
			returnstr = CardInfoCatcher.GetChinesename(targetname);
		}
//		foreach (var pairvalue in CardsnameDic)  
//		{    	
//			if(pairvalue.Key.Equals(MosesEnglishData.FocusTargetname))
//			{
//				if(MosesEnglishData.Language == 0)
//				{
//					if(pairvalue.Key.Equals("fish2"))
//						returnstr = "fish";
//					else 
//						returnstr = pairvalue.Key;
//				}
//				else
//					returnstr = pairvalue.Value;
//			}
//		}  
		return returnstr;
	}
}