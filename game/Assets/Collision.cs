using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;
namespace Algebra
{
	public static class Collision
	{
		public static bool CircleCollision(Vector2 c1Pos, float c1Radius, Vector2 c2Pos, float c2Radius)
		{
			float distance = Mathf.Sqrt((c1Pos.x - c2Pos.x) * (c1Pos.x - c2Pos.x) + ((c1Pos.y - c2Pos.y) * (c1Pos.y - c2Pos.y)));
			return distance < c1Radius + c2Radius;
		}

		public static bool AABBCollision(Vector2 s1Pos, Vector2 s1Size, Vector2 s2Pos, Vector2 s2Size) 
		{
			return (s1Pos.x < s2Pos.x + s2Size.x &&
					s1Pos.x + s1Size.x > s2Pos.x &&
					s1Pos.y < s2Pos.y + s2Size.y &&
					s1Pos.y + s1Size.y > s2Pos.y);
		}
	}
}
