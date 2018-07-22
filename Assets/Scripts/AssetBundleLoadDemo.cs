/*******
 * ［标题］
 * 项目：AssetBundleDemo
 * 作者：微风的龙骑士 风游迩
 * 
 * 
 * 
 * ［功能］
 * 演示AssetBundle的基本加载
 * 分为两种情况：
 * 1.加载非“对象预设”的方式（如：贴图、材质）
 * 2.加载“对象预设”的方式
 * 
 * ［思路］
 * 
 * 
 * ［用法］
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace AssetBundleDemo {
	/// <summary>
	/// 
	/// </summary>
	public class AssetBundleLoadDemo : MonoBehaviour {

		////测试对象，改变贴图
		//public GameObject goCube;
		////定义URL和资源名称
		//private string _Url1;
		//private string _AssetName1;

		//测试对象，方位
		public Transform TraShow;
		//定义URL和资源名称
		private string _Url2;
		private string _AssetName2;

		void Awake(){
			////AB包的下载地址
			//_Url1 = "file://" + Application.streamingAssetsPath + "/foxsworder";
			////资源的名称（在AB包内部）
			//_AssetName1 = "foxsworder_2";

			//AB包的下载地址
			_Url2 = "file://" + Application.streamingAssetsPath + "/foxsworder";
			//资源的名称（在AB包内部）
			_AssetName2 = "foxsworder_5";
		}

		void Start(){
			//StartCoroutine(LoadNonObjecFromAB(_Url1, goCube, _AssetName1));

			StartCoroutine(LoadPrefabFromAB(_Url2, _AssetName2, TraShow));
		}



		/// <summary>
		/// 加载非游戏对象的资源
		/// </summary>
		/// <param name="abUrl">AB包的Url</param>
		/// <param name="showGO">要操作和显示的对象</param>
		/// <param name="assetName">要加载的资源的名称</param>
		/// <returns></returns>
		IEnumerator LoadNonObjecFromAB(string abUrl, GameObject showGO, string assetName){
			//参数检查
			if (string.IsNullOrEmpty(abUrl) || showGO == null) {
				Debug.LogError("LoadNoeObjectFromAB()\t"+"输入的参数不合法！");
				yield break;
			}

			//加载WWW资源
			using (WWW www = new WWW(abUrl)) {
				//等待WWW资源加载完毕
				yield return www;
				AssetBundle ab = www.assetBundle;

				//非空检查
				if (ab == null) {
					Debug.LogError("LoadNoeObjectFromAB()\t" + "下载错误！");
					yield break;
				}

				//确定主要贴图
				if (assetName == "") {
					showGO.GetComponent<Renderer>().material.mainTexture = ab.mainAsset as Texture;
				}
				else {
					showGO.GetComponent<Renderer>().material.mainTexture = ab.LoadAsset(assetName) as Texture;
				}
				//卸载资源（只卸载AB包本身）
				ab.Unload(false);
			}
		}


		/// <summary>
		/// 加载预设的资源
		/// </summary>
		/// <param name="abUrl">AB包的Url</param>
		/// <param name="assetName">要加载的资源的名称</param>
		/// <param name="showTransform">要加载的方位</param>
		/// <returns></returns>
		IEnumerator LoadPrefabFromAB(string abUrl, string assetName,Transform showTransform) {
			//参数检查
			if (string.IsNullOrEmpty(abUrl)) {
				Debug.LogError("LoadNoeObjectFromAB()\t" + "输入的参数不合法！");
				yield break;
			}

			//加载WWW资源
			using (WWW www = new WWW(abUrl)) {
				//等待WWW资源加载完毕
				yield return www;
				AssetBundle ab = www.assetBundle;

				//非空检查
				if (ab == null) {
					Debug.LogError("LoadNoeObjectFromAB()\t" + "下载错误！");
					yield break;
				}

				if (assetName == "") {
					//加载主资源
					if (showTransform != null) {
						GameObject goClone = Instantiate(ab.mainAsset) as GameObject;
						//克隆对象的显示位置
						if (goClone != null)
							goClone.transform.position = showTransform.position;
					}
					else {
						//克隆加载的预设对象
						Instantiate(ab.mainAsset);
					}
				}
				else {
					//实例化指定的资源
					if (showTransform != null) {
						GameObject goClone = Instantiate(ab.LoadAsset(assetName)) as GameObject;
						//克隆对象的显示位置
						if (goClone != null)
							goClone.transform.position = showTransform.position;
					}
					else {
						//克隆加载的预设对象
						Instantiate(ab.LoadAsset(assetName));
					}

				}
				//卸载资源（只卸载AB包本身）
				ab.Unload(false);
			}
		}
	}
}