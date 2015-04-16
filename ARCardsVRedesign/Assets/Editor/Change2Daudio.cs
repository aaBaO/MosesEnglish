using UnityEngine;
using System.Collections;
using UnityEditor;

public class Change2Daudio:EditorWindow
{
	private bool istwoDAudioclip;

	[@MenuItem("BaO/Change audio to 2D")]
	private static void Init()
	{
		Change2Daudio window = (Change2Daudio)GetWindow(typeof(Change2Daudio), true, "Change2DAudio");
		window.Show();
	}

	private void OnGUI()
	{
		GUILayout.BeginVertical();
//		istwoDAudioclip = EditorGUILayout.Toggle("Set 2DAudio?", true);
		istwoDAudioclip = false;
		GUILayout.EndVertical();

		if(GUILayout.Button("Set!!"))
		{
			SetAudio();
		}


	}

	private Object[] GetSelectedAudio()
	{
		return Selection.GetFiltered(typeof(AudioClip), SelectionMode.Unfiltered);
	}

	public AudioImporter GetAudioSettings(string path)
	{
		AudioImporter maudioImporter = AudioImporter.GetAtPath(path) as AudioImporter;
		return maudioImporter;
	}

	private void SetAudio()
	{
		Object[] objs = GetSelectedAudio();
		Selection.objects = new Object[0];
		foreach(AudioClip ac in objs)
		{
			string path = AssetDatabase.GetAssetPath(ac);
			AudioImporter audioImporter = GetAudioSettings(path);
			audioImporter.threeD = istwoDAudioclip;
			audioImporter.format = AudioImporterFormat.Compressed;
			AssetDatabase.ImportAsset(path);
		}
	}
}
