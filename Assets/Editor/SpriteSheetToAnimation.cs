using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SpriteSheetToAnimation : MonoBehaviour
{
    [MenuItem("Tools/Create Animation From Sprite Sheet %#d")]
    public static void CreateAnimationFromSpriteSheet()
    {
        Object obj = Selection.activeObject;
        string path = AssetDatabase.GetAssetPath(obj);

        if (!path.EndsWith(".png") && !path.EndsWith(".jpg"))
        {
            Debug.LogError("������ ������ �̹����� �ƴմϴ�.");
            return;
        }

        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;

        if (ti == null || !ti.spriteImportMode.Equals(SpriteImportMode.Multiple))
        {
            Debug.LogError("�� ��������Ʈ�� 'Multiple' Ÿ������ �����Ǿ�� �մϴ�.");
            return;
        }

        // ��������Ʈ ����Ʈ ����
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
        List<Sprite> sprites = new List<Sprite>();

        foreach (var asset in assets)
        {
            if (asset is Sprite sprite)
                sprites.Add(sprite);
        }

        if (sprites.Count == 0)
        {
            Debug.LogError("��������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        sprites.Sort((a, b) => a.name.CompareTo(b.name)); // �̸��� ����

        // Animation Clip ����
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 12f; // ������ �ӵ� ����

        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[sprites.Count];
        for (int i = 0; i < sprites.Count; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe
            {
                time = i / clip.frameRate,
                value = sprites[i]
            };
        }

        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, keyFrames);

        // Animation ����
        string animPath = Path.ChangeExtension(path, ".anim");
        AssetDatabase.CreateAsset(clip, animPath);
        AssetDatabase.SaveAssets();

        Debug.Log("�ִϸ��̼� ���� �Ϸ�: " + animPath);
    }
}
