using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Infrastructure
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}