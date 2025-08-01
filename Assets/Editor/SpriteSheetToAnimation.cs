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
            Debug.LogError("선택한 파일이 이미지가 아닙니다.");
            return;
        }

        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;

        if (ti == null || !ti.spriteImportMode.Equals(SpriteImportMode.Multiple))
        {
            Debug.LogError("이 스프라이트는 'Multiple' 타입으로 설정되어야 합니다.");
            return;
        }

        // 스프라이트 리스트 추출
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
        List<Sprite> sprites = new List<Sprite>();

        foreach (var asset in assets)
        {
            if (asset is Sprite sprite)
                sprites.Add(sprite);
        }

        if (sprites.Count == 0)
        {
            Debug.LogError("스프라이트를 찾을 수 없습니다.");
            return;
        }

        sprites.Sort((a, b) => a.name.CompareTo(b.name)); // 이름순 정렬

        // Animation Clip 생성
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 12f; // 프레임 속도 조절

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

        // Animation 저장
        string animPath = Path.ChangeExtension(path, ".anim");
        AssetDatabase.CreateAsset(clip, animPath);
        AssetDatabase.SaveAssets();

        Debug.Log("애니메이션 생성 완료: " + animPath);
    }
}
