using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private float textSpeed;
    [SerializeField] private float textBoxMovingDuration;
    [SerializeField] private float heightOffset;
    [SerializeField] private RectTransform textBoxTransform;
    [SerializeField] private string pathTemplate;
    [SerializeField] private Sequence currentSequence = null;
    [SerializeField] private int blockIndex = 0;
    [SerializeField] private TextMeshProUGUI currentTitle;
    [SerializeField] private TextMeshProUGUI currentText;
    private float _blockTextCutOff;
    private int _blockTextMax;
    private bool _showing;
    private bool _clickedNext;
    private int _blockCount;
    private TempPlayer _currentPlayer;

    public static TextManager Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _currentPlayer = null;
            _showing = false;
            textBoxTransform.position =
                new Vector3(textBoxTransform.position.x, -heightOffset, textBoxTransform.position.y);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Show(bool shouldShow)
    {
        if (!_showing && shouldShow)
        {
            Debug.Log("Subo");
            textBoxTransform.DOMoveY(50, textBoxMovingDuration, true);
            _showing = true;
        }

        if (_showing && !shouldShow)
        {
            Debug.Log("Bajo");
            textBoxTransform.DOMoveY(-heightOffset, textBoxMovingDuration, true);
            _showing = false;
            
            if (_currentPlayer != null) 
                _currentPlayer.AlowInteracting();
        }
    }

    private void Update()
    {
        //Behold la cascada de caca
        //si hay una secuencia y el bloque actual es menor que el total de bloques
        if (currentSequence != null && blockIndex <= _blockCount)
        {
            //Si el texto que voy mostrando del bloque es menor al total del texto del bloque y el usuario no clickeo
            if (_blockTextCutOff <= _blockTextMax && !_clickedNext)
            {
                //Voy alargando el string a mostrar en funcion al tiempo
                _blockTextCutOff += Time.deltaTime * textSpeed;
                currentText.text = currentSequence.blocks[blockIndex].text
                    .Substring(0, Mathf.Min(Mathf.FloorToInt(_blockTextCutOff), _blockTextMax));
            }
            else
            {
                if (_clickedNext && _blockTextCutOff <= _blockTextMax)
                {
                    //El chabon clickeo antes de tiempo, le llenamos la pantalla y le reseteo el click
                    _blockTextCutOff = _blockTextMax + 0.1f;
                    currentText.text = currentSequence.blocks[blockIndex].text;
                    _clickedNext = false;
                }
                else
                {
                    //Terminamos de mostrar el texto y estamos esperando que el usuario aprete click para cargar el siguiente bloque
                    if (_clickedNext)
                    {
                        _clickedNext = false;
                        LoadNextBlock();
                    }
                    else
                    {
                        AudioManager.Instance.StopSpecialFX();
                    }
                }
            }
        }
    }

    public void LoadSequence(TempPlayer currentPlayer,string sequenceName)
    {
        var filePath = string.Format(pathTemplate, sequenceName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Algo malio sal y no encontre el archivo en el path {filePath}");
            return;
        }
        
        _currentPlayer = currentPlayer;
        
        var contents = File.ReadAllText(filePath);
        Debug.Log(contents);
        currentSequence = Sequence.FromJson(contents);
        _blockCount = currentSequence?.blocks?.Length ?? 0;
        LoadBlock(0);
    }

    private void LoadBlock(int i)
    {
        Debug.Log($"Loading block number {i} o {_blockCount}");
        if (i < _blockCount)
        {
            AudioManager.Instance.PlaySpecialFX(Constants.AudioClips.TYPEWRITER);
            blockIndex = i;
            currentText.text = string.Empty;
            _blockTextCutOff = 0;
            _blockTextMax = currentSequence.blocks[blockIndex].text.Length;
            currentTitle.text = currentSequence.blocks[blockIndex].title;
            ResolveImage(currentSequence.blocks[blockIndex].image);
            Show(true);
        }
        else
        {
            Debug.Log("No hay mas :c");
            Show(false);
            currentSequence = null;
        }
    }

    private void ResolveImage(string image)
    {
        //throw new System.NotImplementedException();
    }

    private void LoadNextBlock()
    {
        blockIndex += 1;
        LoadBlock(blockIndex);
    }


    public void DialogPressed()
    {
        Debug.Log("Auch!");
        _clickedNext = true;
    }

    [System.Serializable]
    public class Sequence
    {
        public Block[] blocks;

        public static Sequence FromJson(string json) => JsonConvert.DeserializeObject<Sequence>(json,
            new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
                },
            });
    }

    [System.Serializable]
    public class Block
    {
        public string title;

        public string text;

        public string image;
    }
}