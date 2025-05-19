using System;
using UnityEditor;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [CustomEditor(typeof(ColorCorrectionLookup))]
    class ColorCorrectionLookupEditor : Editor
    {
        SerializedObject serObj;
        private Texture2D tempClutTex2D;

        void OnEnable()
        {
            serObj = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            serObj.Update();

            EditorGUILayout.LabelField("Converts textures into color lookup volumes (for grading)", EditorStyles.miniLabel);

            ColorCorrectionLookup effect = (ColorCorrectionLookup)target;

            tempClutTex2D = EditorGUILayout.ObjectField(" Based on", tempClutTex2D, typeof(Texture2D), false) as Texture2D;
            if (tempClutTex2D == null && !string.IsNullOrEmpty(effect.basedOnTempTex))
            {
                tempClutTex2D = AssetDatabase.LoadAssetAtPath<Texture2D>(effect.basedOnTempTex);
            }

            if (tempClutTex2D != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(tempClutTex2D);

                if (effect.basedOnTempTex != assetPath)
                {
                    EditorGUILayout.Space();
                    if (!effect.ValidDimensions(tempClutTex2D))
                    {
                        EditorGUILayout.HelpBox("Invalid texture dimensions!\nExpected: 256x16, 512x16, etc.", MessageType.Warning);
                    }
                    else if (GUILayout.Button("Convert and Apply"))
                    {
                        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                        if (importer != null)
                        {
                            bool needsReimport = false;

                            if (!importer.isReadable)
                            {
                                importer.isReadable = true;
                                needsReimport = true;
                            }

                            if (importer.mipmapEnabled)
                            {
                                importer.mipmapEnabled = false;
                                needsReimport = true;
                            }

                            if (importer.textureCompression != TextureImporterCompression.Uncompressed)
                            {
                                importer.textureCompression = TextureImporterCompression.Uncompressed;
                                needsReimport = true;
                            }

                            if (needsReimport)
                            {
                                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                            }
                        }

                        effect.Convert(tempClutTex2D, assetPath);
                    }
                }
            }

            if (!string.IsNullOrEmpty(effect.basedOnTempTex))
            {
                EditorGUILayout.HelpBox("Using " + effect.basedOnTempTex, MessageType.Info);

                Texture2D previewTex = AssetDatabase.LoadAssetAtPath<Texture2D>(effect.basedOnTempTex);
                if (previewTex)
                {
                    Rect r = GUILayoutUtility.GetRect(128, 20);
                    GUI.DrawTexture(r, previewTex, ScaleMode.ScaleToFit);
                    GUILayout.Space(4);
                }
            }

            serObj.ApplyModifiedProperties();
        }
    }
}
