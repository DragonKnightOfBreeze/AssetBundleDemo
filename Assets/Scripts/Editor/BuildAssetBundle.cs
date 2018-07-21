/*******
 * ［标题］
 * 项目：AssetBundleDemo
 * 作者：微风的龙骑士 风游迩
 * 
 * 
 * 
 * ［标题］
 * 
 * 
 * ［功能］
 * 
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
using UnityEngine;
using UnityEditor;		//引入Unity编辑器命名空间

namespace AssetBundleDemo {
	/// <summary>
	/// 
	/// </summary>
	public class BuildAssetBundle {

		/// <summary>
		/// 打包生成所有的AssetBundle包
		/// （特性：在菜单中增加一个标签卡）
		/// </summary>
		[MenuItem("AssetBundleTools/BuildAllAssetBundles")]
		public static void BuildAllAB(){
			//打包AB输出路径
			string abOutPathDir = string.Empty;
			//获取StreamingAssets路径
			abOutPathDir = Application.streamingAssetsPath;
			//如果不存在StreamingAssets目录，则自动生成
			if (!Directory.Exists(abOutPathDir))
				Directory.CreateDirectory(abOutPathDir);
			
			//打包生成
			BuildPipeline.BuildAssetBundles(abOutPathDir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
		}

	}
}