using UnityEngine;

/*
 * Toggle for two GameObjects: Displays the one and hides the other.
 */
public class DisplayToggle : MonoBehaviour
{
    public GameObject activeDisplay;
    public GameObject inactiveDisplay;
    public bool activeOnDefault;

    private bool active;
    public bool Active
    {
        get{
            return active;
        }

        set{
            active = value;

            if (active)
            {
                activeDisplay.SetActive(true);
                inactiveDisplay.SetActive(false);
            }
            else
            {
                activeDisplay.SetActive(false);
                inactiveDisplay.SetActive(true);
            }
        }
    }
    

    void Start()
    {
        active = activeOnDefault;
    }

}
