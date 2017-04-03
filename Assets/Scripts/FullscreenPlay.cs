using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FullscreenPlay : MonoBehaviour {
	VideoPlayer videoPlayer;

	void Start()
	{
		// ローカルファイルを再生
		PlayVideo ("Assets/webmtest.webm", false);

		// Web上のファイルを再生
		//PlayVideo ("https://raw.githubusercontent.com/nakamura001/WebMAlphaSample/master/WebM/sample_vp8.webm", true);
	}

	void PlayVideo(string url, bool internet) {
		// ※ Main Camera には VideoPlayer コンポーネントを追加しておく
		GameObject camera = GameObject.Find("Main Camera");

		videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

		videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;

		//videoPlayer.targetCameraAlpha = 0.5F; // 動画全体のアルファ値設定

		// ファイルパス or URL の指定
		videoPlayer.url = url;

		// 再生開始のフレーム数
		//videoPlayer.frame = 100;

		// ループ設定
		videoPlayer.isLooping = true;

		// ループが行われるタイミングでのイベント。このサンプルでは再生速度を 1/10 にしている
		videoPlayer.loopPointReached += EndReached;

		if (internet) {
			// Web 上からダウンロードする場合には先読みをしておく。
			// 先読みしなくてもエラーは発生しません。再生ボタンを押した後にすぐに再生したい場合には先読みを行います
			Debug.Log ("Prepare");
			videoPlayer.prepareCompleted += PrepareCompleted;
			videoPlayer.Prepare ();
		} else {
			Debug.Log ("Play");
			videoPlayer.Play();
		}
	}

	void PrepareCompleted(VideoPlayer vp) {
		vp.prepareCompleted -= PrepareCompleted;
		Debug.Log ("Play");
		vp.Play();
	}

	void EndReached(VideoPlayer vp)
	{
		Debug.Log ("Loop");
		vp.playbackSpeed = vp.playbackSpeed / 10.0F;
	}
}
