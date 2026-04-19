using UnityEngine;
using XNode;

namespace _Source
{
    [CreateAssetMenu(fileName = "New Math Graph", menuName = "BrainGame/Math Graph")]
    public class Graph : NodeGraph 
    {
        [Tooltip("Самый первый пример при запуске")]
        public MathNode startNode;
    }
}