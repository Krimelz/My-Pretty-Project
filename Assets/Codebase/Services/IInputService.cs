using System;
using UnityEngine;

namespace Codebase.Services
{
    public interface IInputService
    {
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnLook;
        public event Action OnJump;
        public event Action OnFire;
    }
}