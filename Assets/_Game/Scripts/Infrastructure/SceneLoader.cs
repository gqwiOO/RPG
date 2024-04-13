using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineHandler _coroutineHandler;

        public SceneLoader(ICoroutineHandler coroutineHandler)
        {
            _coroutineHandler = coroutineHandler;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineHandler.StartCoroutine(LoadScene(name, onLoaded));
        }
        private IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }   
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            yield return new WaitUntil(() => waitNextScene.isDone);
            onLoaded?.Invoke();

        }
    }
}