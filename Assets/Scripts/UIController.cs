using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : Observer
{
    private Player playerRef;

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private float tickRate = 0.2F;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public override void OnNotify(bool hit)
    {
        if (hit == true)
        {
            void Start()
            {
                ToggleRestartButton(false);

                playerRef = FindObjectOfType<Player>();

                if (playerRef != null && lifeImages.Length == Player.PLAYER_LIVES)
                {
                    InvokeRepeating("UpdateUI", 0F, tickRate);
                }
            }

            void ToggleRestartButton(bool val)
            {
                if (restartBtn != null)
                {
                    restartBtn.gameObject.SetActive(val);
                }

                void UpdateUI(bool va)
                {
                    for (int i = 0; i < lifeImages.Length; i++)
                    {
                        if (lifeImages[i] != null && lifeImages[i].enabled)
                        {
                            lifeImages[i].gameObject.SetActive(playerRef.Lives >= i + 1);
                        }
                    }

                    if (scoreLabel != null)
                    {
                        scoreLabel.text = playerRef.Score.ToString();
                    }

                    if (playerRef.Lives <= 0)
                    {
                        CancelInvoke();

                        if (scoreLabel != null)
                        {
                            scoreLabel.text = "Game Over";
                        }

                        ToggleRestartButton(true);
                    }
                }
            }
        }
    }
}