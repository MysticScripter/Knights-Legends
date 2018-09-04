using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHowToPlay : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.collider.name == "Knight"))
            return;
        SceneManager.LoadScene(2);
    }
}