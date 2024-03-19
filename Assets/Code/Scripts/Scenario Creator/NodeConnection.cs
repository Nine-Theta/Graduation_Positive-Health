using UnityEngine;

namespace ScenarioEditor
{
    public class NodeConnection : MonoBehaviour
    {
        //The connected nodes
        protected AbstractNode _previousNode;
        protected AbstractNode _nextNode;

        private bool _traversable = true; //Determines if the next node can be accessed

        public bool traversable { get { return _traversable; } set { _traversable = value; } }

        public void SetParentConnection(AbstractNode pNode)
        {
            _previousNode = pNode;
        }

        public void SetChildConnection(AbstractNode pNode)
        {
            _nextNode = pNode;
        }
    }
}
