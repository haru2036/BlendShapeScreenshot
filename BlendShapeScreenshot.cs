/**
Copyright (c) 2020 haru2036

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;


public class BlendShapeScreenshot : MonoBehaviour
{
    private Camera camera;
    private float range;

    public void startCapture(Camera camera, float range){
        this.camera = camera;
        this.range = range;
        StartCoroutine("captureAllBlendShapes");

    }

    public IEnumerator captureAllBlendShapes(){
        float[] prevValues = dumpCurrentBlendShapeValues();

        var SkinnedMeshRenderer = gameObject.GetComponent(typeof (SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        Mesh mesh = SkinnedMeshRenderer.sharedMesh;
        var names = new string[mesh.blendShapeCount];
        foreach(int i in Enumerable.Range(0, mesh.blendShapeCount - 1)){
            SkinnedMeshRenderer.SetBlendShapeWeight(i, 0.0f);
            names[i] = mesh.GetBlendShapeName(i);
            Debug.Log(mesh.GetBlendShapeName(i));
        }

        foreach(int i in Enumerable.Range(0, mesh.blendShapeCount - 1)){
            SkinnedMeshRenderer.SetBlendShapeWeight(i, range);
            yield return null;
            RenderImage(camera, names[i]);
            SkinnedMeshRenderer.SetBlendShapeWeight(i, 0.0f);
        }

        restoreBlendShapeValues(prevValues);
    }

    private float[] dumpCurrentBlendShapeValues(){
        var SkinnedMeshRenderer = gameObject.GetComponent(typeof (SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        Mesh mesh = SkinnedMeshRenderer.sharedMesh;
        float[] prevValues = new float[mesh.blendShapeCount];
        foreach(int i in Enumerable.Range(0, mesh.blendShapeCount - 1)){
            prevValues[i] = SkinnedMeshRenderer.GetBlendShapeWeight(i);
        }
        return prevValues;
    }

    private void restoreBlendShapeValues(float[] values){
        var SkinnedMeshRenderer = gameObject.GetComponent(typeof (SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        Mesh mesh = SkinnedMeshRenderer.sharedMesh;
        foreach(int i in Enumerable.Range(0, mesh.blendShapeCount - 1)){
            SkinnedMeshRenderer.SetBlendShapeWeight(i, values[i]);
        }
    }



    public void RenderImage(Camera camera, String shapeName)
    {
        Texture2D texture = new Texture2D (camera.targetTexture.width, camera.targetTexture.height,
                            TextureFormat.RGB24, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;
        camera.Render();
        texture.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = currentRT;
        byte[] bytes =  texture.EncodeToPNG ();
        File.WriteAllBytes(Application.dataPath + "/BlendShapeCaptures/" + shapeName + ".png", bytes);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BlendShapeScreenshot))]
public class BlendShapeScreenshotEditor: Editor
{
    Camera camera;
    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        camera = (Camera)EditorGUILayout.ObjectField(camera , typeof(UnityEngine.Camera), true);

        Draw();
        serializedObject.ApplyModifiedProperties ();
    }


    void Draw()
        {

            if (GUILayout.Button("Take Screenshot"))
            {
                var tc = target as BlendShapeScreenshot;
                tc.startCapture(camera, 100.0f);
            }
        }
        

}
#endif