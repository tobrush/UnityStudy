using UnityEngine;

public class HanoiBackgorund : MonoBehaviour
{
    public HanoiTower hanoiTower;

    private void OnMouseDown()
    {
        CancleAction();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancleAction();
        }
        
    }

    private void CancleAction()
    {
        if (hanoiTower.bars[hanoiTower.LastBar].barStack.Count >= 0 && HanoiTower.isSelected)
        {

            MeshRenderer meshRenderer = HanoiTower.selectedDonut.GetComponent<MeshRenderer>();

            meshRenderer.material.DisableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", Color.white);

            hanoiTower.bars[hanoiTower.LastBar].PushDonut(HanoiTower.selectedDonut);

            HanoiTower.isSelected = false;
            HanoiTower.selectedDonut = null;

            hanoiTower.SelectText.text = $"√Îº“µ ";
        }
    }

}
