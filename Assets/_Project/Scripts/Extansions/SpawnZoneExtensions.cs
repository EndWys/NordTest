using UnityEngine;

namespace Assets._Project.Scripts.Extansions
{
    public static class SpawnZoneExtensions
    {
        public static Vector2 GetRandomPointOnBounds(this Collider2D collider)
        {
            Bounds bounds = collider.bounds;

            float perimeter = 2 * (bounds.size.x + bounds.size.y);
            float randomPerimeterPos = Random.Range(0f, perimeter);

            if (randomPerimeterPos < bounds.size.x)
            {
                return new Vector2(bounds.min.x + randomPerimeterPos, bounds.min.y);
            }
            else if (randomPerimeterPos < bounds.size.x + bounds.size.y)
            {
                return new Vector2(bounds.max.x, bounds.min.y + (randomPerimeterPos - bounds.size.x));
            }
            else if (randomPerimeterPos < bounds.size.x * 2 + bounds.size.y)
            {
                return new Vector2(bounds.max.x - (randomPerimeterPos - bounds.size.x - bounds.size.y), bounds.max.y);
            }
            else
            {
                return new Vector2(bounds.min.x, bounds.max.y - (randomPerimeterPos - 2 * bounds.size.x - bounds.size.y));
            }
        }

        public static Vector2[] GetCornerPoints(this Collider2D collider)
        {
            Bounds bounds = collider.bounds;

            return new Vector2[]
            {
                new Vector2(bounds.min.x, bounds.min.y),
                new Vector2(bounds.max.x, bounds.min.y),
                new Vector2(bounds.max.x, bounds.max.y),
                new Vector2(bounds.min.x, bounds.max.y)
            };
        }
    }
}