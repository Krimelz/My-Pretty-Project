using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Codebase
{
	public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

		private async UniTaskVoid Start()
        {
            var result = await LoadAsync();
			await result.ActivateAsync();

            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
        }

        private async UniTask<SceneInstance> LoadAsync()
		{
            AsyncOperationHandle<SceneInstance> handle = 
                Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);

            return await handle;
        }
    }
}
