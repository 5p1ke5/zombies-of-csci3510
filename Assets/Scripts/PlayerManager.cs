using UnityEngine;
// code used to get zombies to know what object to be following
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameObject player;
}
