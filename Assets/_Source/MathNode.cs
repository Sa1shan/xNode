using UnityEngine;
using XNode;

namespace _Source
{
    public class MathNode : Node
    {
        [Input] public int entry;
        [TextArea(2, 4)] public string questionText;
        
        [Header("Варианты ответа")]
        public string leftAnswerText;
        public string rightAnswerText;
        public bool isLeftCorrect;

        [Header("Настройки статов")] 
        public int brainReward = 10;
        public int brainMinus  = 10;
        public int tired  = 10;

        [Output] public int next;

        public override object GetValue(NodePort port) 
        {
            return null;
        }
        
        public MathNode GetNextNode()
        {
            NodePort port = GetOutputPort("next");
            if (port != null && port.IsConnected)
            {
                return port.Connection.node as MathNode;
            }
            return null;
        }
    }
}
