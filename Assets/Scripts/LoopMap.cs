using UnityEngine;

public class LoopMap : MonoBehaviour
{

    
    public float moveSpeed = 3.0f;

    public float EndZone = -30f;
    public float StartZone = 30f;

    MeshRenderer m_Renderer;

    public float offsetSpeed = 0.1f;

    public bool TransformToMaterial = false;



    private void Start()
    {
        if(TransformToMaterial)
        {
            m_Renderer = GetComponent<MeshRenderer>();

        }
    }

    private void Update()
    {

        if(TransformToMaterial)
        {
            Vector2 offset = Vector2.right * offsetSpeed * Time.deltaTime;

            m_Renderer.material.SetTextureOffset("_BaseMap", m_Renderer.material.mainTextureOffset + offset);
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.x <= EndZone)
            {
                transform.position = new Vector3(StartZone, 3, 0);
            }
        }
    }
}
