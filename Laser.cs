using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [SerializeField] private int LaserWidth = 0;
    [SerializeField] private int LaserHeight = 0;

    public ParticleSystem PiecesRust;
    public GameObject ParticleObject;
    public Text CleaningPercent;
    public Texture2D DirtTexture;

    private Texture2D _dirt;
    private Item _item;
    private Toggle _toggle;
    private GameLoop GameLoop;

    void Update()
    {
        LaserInstrument();
    }

    public void SetFields()
    {
        _item = FindObjectOfType<Item>();
        _toggle = GetComponent<Toggle>();
        GameLoop = FindObjectOfType<GameLoop>();

        _dirt = Instantiate(DirtTexture);
        _item.GetComponent<MeshRenderer>().material.mainTexture = _dirt;
    }

    private void LaserInstrument()
    {
        if (_toggle.isOn)
        {
            if (Input.touchCount > 0)
            {
                Touch touchType = Input.GetTouch(0);

                if (touchType.phase == TouchPhase.Moved)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        Vector2 pixelUV = hit.textureCoord;
                        pixelUV.x *= _dirt.width;
                        pixelUV.y *= _dirt.height;

                        Color TransparentColor = new Color(1, 1, 1);

                        for (int x = 0; x < LaserWidth; x++)
                        {
                            _dirt.SetPixel((int)pixelUV.x + x, (int)pixelUV.y, TransparentColor);
                            _dirt.SetPixel((int)pixelUV.x - x, (int)pixelUV.y, TransparentColor);
                            
                            for(int y = 0; y < LaserHeight; y++)
                            {
                                _dirt.SetPixel((int)pixelUV.x - x, (int)pixelUV.y + y, TransparentColor);
                                _dirt.SetPixel((int)pixelUV.x - x, (int)pixelUV.y - y, TransparentColor);

                                _dirt.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, TransparentColor);
                                _dirt.SetPixel((int)pixelUV.x + x, (int)pixelUV.y - y, TransparentColor);
                            }
                        }

                        _dirt.Apply();

                        ParticleObject.transform.position = hit.point;

                        if(PiecesRust.isPlaying == false)
                        {
                            PiecesRust.Play();
                        }
                    }
                }
                else if(touchType.phase == TouchPhase.Ended)
                {
                    int f = (int)_item.CkeckCleaning(_dirt, new Color(1, 1, 1));
                    CleaningPercent.text = f + "%";

                    if(f >= 10)
                    {
                        Destroy(_item.gameObject);

                        GameLoop.InstantiateNewItem();

                        CleaningPercent.text = "0%";
                    }

                    PiecesRust.Stop();
                }
            }
        }
    }
}
