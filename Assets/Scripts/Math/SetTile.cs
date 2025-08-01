using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetTile : MonoBehaviour
{
    public GameObject tilePrefab;
    public int rows = 5, cols = 5;

    public Button[] buttons;

    public static int turretIndex;

    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => ChangeIndex(j));
        }
    }

    IEnumerator Start()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var pos = new Vector3(j, 0, i);

                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, this.gameObject.transform);
                Renderer renderer = tile.GetComponent<Renderer>();

                if ((i + j) % 2 == 0) // Â¦¼ö
                    renderer.material.color = Color.white;
                else // È¦¼ö
                    renderer.material.color = Color.black;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void ChangeIndex(int index)
    {
        turretIndex = index;

        for (int i = 0; i < buttons.Length; i++)
        {
            if(i != index)
            {
                buttons[i].GetComponent<Image>().color = Color.black;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}