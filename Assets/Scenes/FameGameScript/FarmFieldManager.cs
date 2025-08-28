using System.Collections;
using UnityEngine;
using static FarmGameManager;


public class FarmFieldManager : MonoBehaviour
{
    public enum FieldState { None, Seed, Harvest }
    public FieldState fieldState;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2 fieldSize = new Vector2(8, 8);
    [SerializeField] private float tileSize = 2f;

    [SerializeField] private int currentPlantIndex;
    [SerializeField] private GameObject[] plants;

        [SerializeField] private GameObject[] crops;

    private GameObject[,] tileArray;
    private Camera mainCamera;
    [SerializeField] private LayerMask fieldLayerMask;

    void Awake()
    {
        mainCamera = Camera.main;
        tileArray = new GameObject[(int)fieldSize.x, (int)fieldSize.y];

        CreateField();
    }

    void Update()
    {
        if (fieldState != FieldState.None)
        {
            switch (fieldState)
            {
                case FieldState.Seed:
                    OnSeed();
                    break;
                case FieldState.Harvest:
                    OnHarvest();
                    break;
            }
        }
    }

    private void CreateField()
    {
        float offsetX = (fieldSize.x - 1) * tileSize / 2;
        float offsetY = (fieldSize.y - 1) * tileSize / 2;

        for (int i = 0; i < fieldSize.x; i++)
        {
            for (int j = 0; j < fieldSize.y; j++)
            {
                float posX = transform.position.x + i * tileSize - offsetX;
                float posZ = transform.position.z + j * tileSize - offsetY;

                GameObject tileObj = Instantiate(tilePrefab, transform.GetChild(0));

                tileObj.name = $"Tile_{i}_{j}";
                tileObj.transform.position = new Vector3(posX, 0.3f, posZ);
                // tileArray[i, j] = tileObj;

                tileObj.GetComponent<FarmTile>().arrayPos = new Vector2Int(i, j);
            }
        }
    }

    private void OnSeed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f, fieldLayerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 0.1f);

                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                int tileX = tile.arrayPos.x;
                int tileY = tile.arrayPos.y;

                //Debug.Log("hit! " + tileX + " / "+ tileY);
                if (tileArray[tileX, tileY] == null )
                {
                    GameObject plant = Instantiate(plants[currentPlantIndex], transform.GetChild(1));
                    plant.transform.position = hit.transform.position;

                    plant.GetComponent<Plant>().plantIndex = currentPlantIndex;

                    tileArray[tileX, tileY] = plant;
                }

            }
        }
    }

    private void OnHarvest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, fieldLayerMask))
            {
                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                int tileX = tile.arrayPos.x;
                int tileY = tile.arrayPos.y;

                if (tileArray[tileX, tileY] != null)
                {
                    Plant plant = tileArray[tileX, tileY].GetComponent<Plant>();

                    if (plant.isHarvest)
                    {
                        plant.gameObject.SetActive(false);
                        tileArray[tileX, tileY] = null;

                        StartCoroutine(HarvestRoutine(plant.plantIndex, tile.transform.position));
                    }
                }

            }
        }
    }



    IEnumerator HarvestRoutine(int index, Vector3 pos)
    {
        int ranAmount = Random.Range(1, 4); // 1 ~ 3개의 작물 개수 설정

        for (int i = 0; i < ranAmount; i++)
        {
            GameObject crop = Instantiate(crops[index]);
            crop.transform.position = pos + Vector3.up * 0.5f;
            Rigidbody cropRb = crop.GetComponent<Rigidbody>();

            float ranX = Random.Range(-2f, 2f);
            float ranZ = Random.Range(-2f, 2f);
            var forceDir = new Vector3(ranX, 5f, ranZ);

            cropRb.AddForce(forceDir, ForceMode.Impulse);

            yield return new WaitForSeconds(0.15f);
        }
    }
    public void SetPlant(int index)
    {
        currentPlantIndex = index;
    }

    public void SetState(FieldState newState)
    {
        if (fieldState != newState)
        {
            fieldState = newState;
        }
    }
}
