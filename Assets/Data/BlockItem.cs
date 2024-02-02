using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    public int value;
    public int Value => value;

    [SerializeField] private ManagerGame managerGame;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
    }

    public void SetValue(int value)
    {
        this.value = value;
    }
}
