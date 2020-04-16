# ブレンドシェイプごとのスクリーンショットを撮影するやつ
Unity上でSkinnedMeshRendererの持つブレンドシェイプそれぞれに対してスクリーンショットを撮影するスクリプトです。

# 動作イメージ
![example](https://raw.githubusercontent.com/haru2036/BlendShapeScreenshot/c5f47681ce22e33e766b08d07559d369d0fffbf5/readme-images/example-reeva.gif)

ここでは、⊿S.I.N 様の【3Dモデル】Leeme -リーメ- & Reeva -リーバ-に対して本スクリプトを使用しています。

https://tomori-hikage.booth.pm/items/972559


# 使い方
念の為使用前にプロジェクトのバックアップを取ってください。
1. AssetsにBlendShapeScreenshot.csを配置します。
2. RenderTextureを用意します。
3. Sceneに撮影したいアングルに設定されたCameraを配置します。
4. CameraのTargetTextureにRenderTextureを指定します。
![カメラの設定](https://raw.githubusercontent.com/haru2036/BlendShapeScreenshot/master/readme-images/camera_setting.png)
5. 撮影したいブレンドシェイプが含まれているSkinnedMeshRendererが含まれているGameObjectにBlendShapeScreenshot.csを追加します。
6. Blend Shape Screenshotに3のCameraを指定します。
![スクリプトの設定](https://raw.githubusercontent.com/haru2036/BlendShapeScreenshot/master/readme-images/take.png)
7. "Take Screenshot"を押して、終了するまで待ちます。このとき、Unity Editor以外のウインドウにフォーカスを当てないでください。
8. \Assets\BlendShapeCapturesにBlendShapeと同じ名前の画像が保存されます。


# LICENSE
このスクリプトはMITライセンスで公開されています。
https://opensource.org/licenses/mit-license.php