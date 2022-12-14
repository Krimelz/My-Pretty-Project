using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Codebase
{
    public class SceneLoader : MonoBehaviour
    {
        [FormerlySerializedAs("_sceneName")] [SerializeField]
        private string sceneName;

        private async void Start()
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (loading.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
