using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : ChiMonoBehaviour
{

    private static ManagerGame instance;
    public static ManagerGame Instance => instance;
    public int rows = 9;
    public int cols = 16;
    [SerializeField] List<int> values;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected Transform blockPrefab;

    [SerializeField] public Sprite[] sprites;

    [SerializeField] public Transform blocks;
    protected override void Awake()
    {
        base.Awake();
        if (ManagerGame.instance != null) Debug.LogError("Only 1 ManagerBlock allow to exist");
        ManagerGame.instance = this;
        sprites = Resources.LoadAll<Sprite>("Images");
        InitializeBlock();
        
      
    }

    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBlockPrefab();
        this.LoadCanvas();
        this.LoadBlocks();
        
    }

    protected void LoadBlocks()
    {
        if (this.blocks != null) return;
        this.blocks = this.canvas.transform.Find("Blocks");
        Debug.LogWarning(transform.name + " LoadBlocks", gameObject);
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = ManagerGame.FindObjectOfType<Canvas>();
        Debug.LogWarning(transform.name + " LoadCanvas", gameObject);
    }


    protected void LoadBlockPrefab()
    {
        if (this.blockPrefab != null) return;
        this.blockPrefab = transform.Find("Block");
        Debug.LogWarning(transform.name + " LoadBlockPrefab", gameObject);
    }

    protected override void Start()
    {
        base.Start();

    }

    public void InitializeBlock()
    {


        values = new List<int>();
        int blockCount = 4;
        
        for (int i = 0; i < (rows*cols)/4; i++)
        {
            for(int j = 0; j < blockCount; j++)
            {
                values.Add(i);
            }
        }

        ShuffleList(values);


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {    
                Transform card = Instantiate(blockPrefab, new Vector3(j, i, 0), Quaternion.identity);
                card.GetComponent<BlockItem>().SetValue(values[i * cols + j]);
                Image buttonImage = this.blockPrefab.GetComponent<Image>();
                int indexImage = values[i * cols + j];  
                buttonImage.sprite = sprites[indexImage];
                card.gameObject.SetActive(true);
                card.SetParent(this.blocks);
            }
        }
        return;
    }


    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count - 1;
        while (n > 0)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }


    }

}
