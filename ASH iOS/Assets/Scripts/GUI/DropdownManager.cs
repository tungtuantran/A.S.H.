using System.Collections.Generic;
using UnityEngine;

/*
 * Dropdown Mananger for the customized Dropdowns. If one dropdown is expanded, then collapse the others.
 */
public class DropdownManager : MonoBehaviour
{

    [SerializeField]
    private List<DropdownButton> dropdownList;

    private DropdownButton currentlyExpandedDropdown;
    private bool collapsing;

    
    void Update()
    {
        // pause updating if CollapseAllExceptCurrentlyExpanded() is still running
        if (!collapsing)
        {
            foreach (DropdownButton dropdown in dropdownList)
            {
                if (dropdown.body.activeSelf && !dropdown.Equals(currentlyExpandedDropdown))
                {
                    currentlyExpandedDropdown = dropdown;
                    CollapseAllExceptCurrentlyExpanded();
                }
            }
        }
    }
    

    public void CollapseAllExceptCurrentlyExpanded()
    {
        collapsing = true;

        foreach (DropdownButton dropdown in dropdownList)
        {
            if (!dropdown.Equals(currentlyExpandedDropdown))
            {
                dropdown.Collapse();
            }
        }

        collapsing = false;
    }
}
