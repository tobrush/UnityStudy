using UnityEditor;
using UnityEngine;

public class SpriteSlicer : EditorWindow
{
    [MenuItem("Tools/Sprite Grid Slicer (Folder)")]
    public static void SliceAllSpritesInFolder()
    {
        // 여기에 원하는 폴더 경로를 쓰세요. 예: "Assets/SpritesToSlice"
        string targetFolder = "Assets/FreeKnight_v1";

        // Texture2D만 검색, 대상 폴더는 targetFolder
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { targetFolder });

        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer == null || importer.textureType != TextureImporterType.Sprite)
                continue;

            // Multiple 스프라이트 모드로 설정
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;

            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (texture == null) continue;

            int texWidth = texture.width;
            int texHeight = texture.height;

            int cellWidth = 120;
            int cellHeight = 80;

            int columns = texWidth / cellWidth;
            int rows = texHeight / cellHeight;

            SpriteMetaData[] metas = new SpriteMetaData[columns * rows];
            int i = 0;
            for (int y = rows - 1; y >= 0; y--)
            {
                for (int x = 0; x < columns; x++)
                {
                    SpriteMetaData meta = new SpriteMetaData();
                    meta.rect = new Rect(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    meta.name = $"{texture.name}_{i}";
                    meta.pivot = new Vector2(0.5f, 0.5f);
                    metas[i++] = meta;
                }
            }

            importer.spritesheet = metas;

            EditorUtility.SetDirty(importer);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            count++;
        }

        Debug.Log($"✅ \"{targetFolder}\" 폴더 내 {count}개 스프라이트 시트를 96×64로 슬라이스 완료했습니다.");
    }
}
