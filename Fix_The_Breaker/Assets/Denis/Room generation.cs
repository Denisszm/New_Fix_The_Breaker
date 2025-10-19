using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
public class NewEmptyCSharpScript
{
    class Roomgeneration : MonoBehaviour
    {
        [SerializeField] private int roomCount = 5;
        private List<Rooms> rooms = new List<Rooms>();

        void Start()
        {
            
        }
    }
    class Rooms
    {
        [SerializeField] public Vector2Int position;
        public Vector2Int size;
        public List<Vector2Int> tiles = new List<Vector2Int>();

        public enum Type
        {
            Breaker,
            Breakroom,
            Office,
            Corridor,
            Factory
        }
    }

}
