using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    private GameObject player;
    private PlayerUIUpdates playerUIUpdates;
    public int experience;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerUIUpdates = player.GetComponent<PlayerUIUpdates>();
    }
    private void OnDestroy()
    {
        playerUIUpdates.updateExperience(experience);
    }
}
