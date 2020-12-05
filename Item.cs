using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Texture2D _dirt;
    private Toggle Toggle => FindObjectOfType<Toggle>();
    private void Start()
    {
        //CreateDirtTexture();
    }

    private void Update()
    {
        RotateItem();
    }

    public float CkeckCleaning(Texture2D Dirt, Color CleanColor)
    {
        Color[] Colors = Dirt.GetPixels(0, 0, Dirt.width, Dirt.height);

        float DirtyPixels = 0;
        float CleanPixels = 0;
        float AllPixels = Dirt.width * Dirt.height;

        for (int i = 0; i < Colors.Length; i++)
        {
            if (Colors[i] == CleanColor)
            {
                CleanPixels++;
            }
            else
            {
                DirtyPixels++;
            }
        }

        return CleanPixels / AllPixels * 100;
    }

    private void CreateDirtTexture()
    {
        _dirt = new Texture2D(512, 512);

        for(int i = 0; i < _dirt.width; i++)
        {
            for(int y = 0; y < _dirt.height; y++)
            {
                _dirt.SetPixel(i, y, new Color(0, 0, 0));
            }
        }

        _dirt.Apply();
        _dirt.Compress(false);

        GetComponent<MeshRenderer>().material.mainTexture = _dirt;
    }

    private void RotateItem()
    {
        if (Toggle.isOn == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(Vector3.down, touch.deltaPosition.x);
                }
            }
        }
    }
}
