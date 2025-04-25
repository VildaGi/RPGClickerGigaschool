using TMPro;
using UnityEngine;
using System;
using System.Diagnostics;
using Game.Elements;
using Random = System.Random;

namespace Game.DamageUI
{
    public class ActiveText
    {
        public TextMeshProUGUI UIText;
        public float maxTime;
        public float Timer;
        
        private Vector3 P1;
        private Vector3 P2;
        private Vector3 P3;
        private Vector3 P4;
        
        public ActiveText(TextMeshProUGUI text, Vector3 P1, ElementType elementType)
        {
            maxTime = 1.0f;
            Timer = 1.0f;
            UIText = text;
            UIText.color = elementType switch
            {
                ElementType.Fire => Color.red,
                ElementType.Air => Color.green,
                ElementType.Rock => Color.gray,
                ElementType.Water => Color.blue,
                ElementType.NoneElement => Color.white,
                _ => Color.white
            };
            
            this.P1 = P1;
            var rnd = new Random();
            
            var deltaX = rnd.Next(200, 500) * Mathf.Sign(UIText.transform.position.x - P1.x);
            var deltaY = rnd.Next(-300, 0);
            
            P4 = P1 + new Vector3(deltaX, deltaY, 0f);
            
            P2 = P1 + new Vector3(0f, rnd.Next(200, 500), 0f);
            
            P3 = new Vector3(P4.x, P4.y, P4.z) + new Vector3(0f, rnd.Next(200, 500), 0f);
        }

        public void MoveText()
        {
            float t = 1.0f - (Timer / maxTime); 

            var x = (Mathf.Pow((1 - t), 3) * P1.x) + 3 * ((1 - t) * (1 - t) * P2.x * t) 
                                                   + 3 *((1 - t) * t * P3.x * t) + t * t * t * P4.x;
            var y = (Mathf.Pow((1 - t), 3) * P1.y) + 3 * ((1 - t) * (1 - t) * P2.y * t) 
                                                   + 3 * ((1 - t) * t * P3.y * t) + t * t * t * P4.y;

            Vector3 pos = new Vector3(x, y, 0f);
            
            UIText.transform.position = pos;
        }
    }
}