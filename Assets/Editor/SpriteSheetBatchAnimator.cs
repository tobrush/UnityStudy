using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SpriteSheetBatchAnimator
{
    [MenuItem("Tools/Create Animations From Selected Folder %#f")] // Ctrl + Shift + F
    public static void CreateAnimationsFromSelectedFolder()
    {
        Object obj = Selection.activeObject;
        string folderPath = AssetDatabase.GetAssetPath(obj);

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError("선택한 항목이 폴더가 아닙니다. Project 창에서 폴더를 선택해주세요.");
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

        foreach (string filePath in files)
        {
            string assetPath = filePath.Replace("\\", "/"); // 윈도우 대비
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer == null || importer.spriteImportMode != SpriteImportMode.Multiple)
            {
                Debug.LogWarning($"[스킵] {assetPath} 는 Multiple 스프라이트 시트가 아닙니다.");
                continue;
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

            Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            List<Sprite> sprites = new List<Sprite>();
            foreach (var asset in allAssets)
            {
                if (asset is Sprite sprite)
                    sprites.Add(sprite);
            }

            if (sprites.Count == 0)
            {
                Debug.LogWarning($"[스킵] {assetPath} 에서 스프라이트를 찾지 못했습니다.");
                continue;
            }

            sprites.Sort((a, b) => a.name.CompareTo(b.name));

            AnimationClip clip = new AnimationClip();
            clip.frameRate = 12f;

            EditorCurveBinding binding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Count];
            for (int i = 0; i < sprites.Count; i++)
            {
                keyframes[i] = new ObjectReferenceKeyframe
                {
                    time = i / clip.frameRate,
                    value = sprites[i]
                };
            }

            AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);

            string animPath = Path.ChangeExtension(assetPath, ".anim");
            AssetDatabase.CreateAsset(clip, animPath);
            Debug.Log($"✅ 애니메이션 생성 완료: {animPath}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("🎉 모든 애니메이션 생성 완료!");
    }
}
