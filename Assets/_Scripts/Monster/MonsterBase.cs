using Boxfriend.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Boxfriend.Monster
{
    public abstract class MonsterBase : MonoBehaviour
    {
        protected PriorityQueue<MonsterTasks> PriorityQueue { get; private set; }

        [SerializeField] protected NavMeshAgent _agent;
    }
}
