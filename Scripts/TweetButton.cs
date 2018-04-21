using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweetButton : MonoBehaviour {

	private Button _button;
	private GameObject rotationTextObj;
	private int rotationCount;
	private string tweetText1 = "ハンドスピナーを";
	private string tweetText2 = "回まわした。アプリはこちらから→ https://play.google.com/store/apps/details?id=com.teraokaakihiro.handspinner";

	void Start ()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnButtonClick);

		rotationTextObj = GameObject.FindGameObjectWithTag("RotationText");
	}
	
	void Update ()
	{
		
	}

	void OnButtonClick()
	{
		string text = tweetText1 + rotationTextObj.GetComponent<Text>().text + tweetText2;
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(text));
	}
}
