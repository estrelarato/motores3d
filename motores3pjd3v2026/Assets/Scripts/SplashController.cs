using System.Collections;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private string nextSceneName = "MenuPrincipal_Scene";

    private IEnumerator Start()
    {
        // Aguarda os 2 segundos requeridos pela atividade
        yield return new WaitForSeconds(waitTime);

        // Solicita a mudança de cena através do GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneChange(nextSceneName);
        }
    }
}