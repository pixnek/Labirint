using UnityEngine;

namespace Labirint
{
    public interface IFollower
    {
        FollowerType GetFollowerType();
        FollowerState GetState();
        bool TryGoTo(ITargetFollower Target); //���������� ���������� ������ FixedUpdate
        void StopFollow();
    }
}