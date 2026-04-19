using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Source
{
    public class MathGameManager : MonoBehaviour
    {
        [Header("Данные")]
        [SerializeField] private Graph graph;
    
        [Header("UI Элементы")]
        [SerializeField] private Text questionText;
        [SerializeField] private Text leftButtonText;
        [SerializeField] private Text rightButtonText;
        [SerializeField] private Text statsText;
        [SerializeField] private Button backButton;

        [Header("Финал")]
        [SerializeField] private GameObject endScreen; 

        private MathNode _currentNode;
        private int _currentBrainActivity;
        private int _currentTired;

        private struct TurnState
        {
            public MathNode Node;
            public int BrainActivity;
            public int Tired;
        }
    
        private Stack<TurnState> _history = new Stack<TurnState>();

        void Start()
        {
            _currentBrainActivity = 0;
            _currentTired = 0;
            _currentNode = graph.startNode;
            UpdateUI();
        }

        public void CorrectAnswer(bool leftChosen)
        {
            _history.Push(new TurnState 
            { 
                Node = _currentNode, 
                BrainActivity = _currentBrainActivity, 
                Tired = _currentTired 
            });
            bool isCorrect = (leftChosen == _currentNode.isLeftCorrect);
            
            if (isCorrect)
            {
                _currentBrainActivity += _currentNode.brainReward;
                _currentTired += _currentNode.tired;
            }
            else
            {
                _currentBrainActivity -= _currentNode.brainMinus;
                if (_currentBrainActivity < 0) _currentBrainActivity = 0;
            }

            _currentNode = _currentNode.GetNextNode();
            UpdateUI();
        }

        public void Back()
        {
            if (_history.Count > 0)
            {
                TurnState previousState = _history.Pop();
                _currentNode = previousState.Node;
                _currentBrainActivity = previousState.BrainActivity;
                _currentTired = previousState.Tired;
            
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            backButton.interactable = _history.Count > 0;
            statsText.text = $"Brain: {_currentBrainActivity}% | Tired: {_currentTired}%";

            if (_currentNode != null)
            {
                questionText.gameObject.SetActive(true);
                questionText.text = _currentNode.questionText;
                leftButtonText.transform.parent.gameObject.SetActive(true);
                rightButtonText.transform.parent.gameObject.SetActive(true);
                leftButtonText.text = _currentNode.leftAnswerText;
                rightButtonText.text = _currentNode.rightAnswerText;
            }
            else
            {
                questionText.gameObject.SetActive(false);
                leftButtonText.transform.parent.gameObject.SetActive(false);
                rightButtonText.transform.parent.gameObject.SetActive(false);
                endScreen.SetActive(true);
            }
        }
    }
}