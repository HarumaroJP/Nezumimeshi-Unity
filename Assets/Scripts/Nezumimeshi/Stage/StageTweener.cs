using UnityEngine;

namespace Nezumimeshi.Stage
{
    public class StageTweener : MonoBehaviour
    {
        [SerializeField] float movableRange;
        [SerializeField] float speed;

        Vector2 origin;
        int direction = 1;

        void Start()
        {
            origin = transform.localPosition;
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            Vector2 current = transform.localPosition;

            if (IsOutOfRange(current.x))
            {
                direction *= -1;
            }

            transform.localPosition = new Vector3(current.x + speed * Time.deltaTime * direction, current.y, -5f);
        }

        bool IsOutOfRange(float current)
        {
            return direction == 1 ? origin.x + movableRange < current : current < origin.x - movableRange;
        }
    }
}