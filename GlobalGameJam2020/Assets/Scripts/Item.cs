using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Sprite image;

    public Sprite Image { get { return image; } }
    public int Id { get { return id; } }
}
