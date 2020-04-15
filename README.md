# ブレンドシェイプごとのスクリーンショットを撮影するやつ
Unity上でSkinnedMeshRendererの持つブレンドシェイプそれぞれに対してスクリーンショットを撮影するスクリプトです。

# 使い方
念の為使用前にプロジェクトのバックアップを取ってください。
1. AssetsにBlendShapeScreenshot.csを配置します。
2. RenderTextureを用意します。
3. Sceneに撮影したいアングルに設定されたCameraを配置します。
4. CameraのTargetTextureにRenderTextureを指定します。[^1]
5. 撮影したいブレンドシェイプが含まれているSkinnedMeshRendererが含まれているGameObjectにBlendShapeScreenshot.csを追加します。
6. Blend Shape Screenshotに3のCameraを指定します。[^2]
7. "Take Screenshot"を押して、終了するまで待ちます。このとき、Unity Editor以外のウインドウにフォーカスを当てないでください。
8. \Assets\BlendShapeCapturesにBlendShapeと同じ名前の画像が保存されます。

[^1]: ![カメラの設定](readme-images/camera_setting.png)
[^2]: ![スクリプトのの設定](readme-images/take.png)

# LICENSE
このスクリプトはMITライセンスで公開されています。
https://opensource.org/licenses/mit-license.php