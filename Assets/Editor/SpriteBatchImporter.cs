using UnityEngine;
using UnityEditor;

public class SpriteBatchImporter : EditorWindow
{

    [MenuItem("Tools/Sprite Batch Importer")]
    static void ApplySettings()
    {
        string[] allPaths = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/FreeKnight_v1" });

        int modifiedCount = 0;

        foreach (string guid in allPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null && importer.textureType == TextureImporterType.Default)
            {
                bool changed = false;

                if (importer.textureType != TextureImporterType.Sprite)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    changed = true;
                }

                // 픽셀 퍼 유닛
                if (importer.spritePixelsPerUnit != 24f)
                {
                    importer.spritePixelsPerUnit = 24f;
                    changed = true;
                }

                // 필터 모드
                if (importer.filterMode != FilterMode.Point)
                {
                    importer.filterMode = FilterMode.Point;
                    changed = true;
                }

                // 압축 설정 (선택사항)
                TextureImporterPlatformSettings platformSettings = importer.GetDefaultPlatformTextureSettings();
                if (platformSettings.format != TextureImporterFormat.RGBA32)
                {
                    platformSettings.format = TextureImporterFormat.RGBA32;
                    importer.SetPlatformTextureSettings(platformSettings);
                    changed = true;
                }

                if (changed)
                {
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    modifiedCount++;
                }
            }
        }

        Debug.Log($"✅ 스프라이트 {modifiedCount}개 설정 완료!");
    }
}
