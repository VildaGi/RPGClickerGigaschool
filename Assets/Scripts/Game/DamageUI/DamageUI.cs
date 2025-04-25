using System.Collections.Generic;
using Game.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Game.DamageUI
{
    public class DamageUI : MonoBehaviour
    {
        public static DamageUI Instance { get; set; }
        
        [SerializeField] private TextMeshProUGUI textPrefab;
        
        const int POOL_SIZE = 50;
        
        private Queue<TextMeshProUGUI> TextPool = new ();
        private List<ActiveText> ActiveTextPool = new();
        
        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            for (var i = 0; i < POOL_SIZE; i++)
            {
                TextMeshProUGUI temp = Instantiate(textPrefab, transform);
                temp.gameObject.SetActive(false);
                
                TextPool.Enqueue(temp);
            }
        }

        void FixedUpdate()
        {
            for (var i = 0; i < ActiveTextPool.Count; i++)
            {
                var at = ActiveTextPool[i];
                at.Timer -= Time.deltaTime;

                if (at.Timer <= 0)
                {
                    at.UIText.gameObject.SetActive(false);
                    TextPool.Enqueue(at.UIText);
                    ActiveTextPool.RemoveAt(i);
                    --i;
                }
                else
                {
                    var color = at.UIText.color;
                    color.a = at.Timer / at.maxTime;
                    at.UIText.color = color;
                    
                    at.MoveText();
                }
            }
        }

        public void AddText(float damage, ElementType elementType)
        {
            var temp = TextPool.Dequeue();
            temp.text = ((int)damage).ToString();
            temp.gameObject.SetActive(true);

            var rnd = new Random();
            
            var pos = temp.gameObject.transform.position + new Vector3(rnd.Next(-50, 50), rnd.Next(100, 150), 0f);
            ActiveText at = new ActiveText(temp, pos, elementType);
            
            at.MoveText();
            ActiveTextPool.Add(at);
        }
    }
}