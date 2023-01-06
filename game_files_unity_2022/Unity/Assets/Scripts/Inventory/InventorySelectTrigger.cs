using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

/* Checks if Inventory List is selected
 * If it is, can navigate down the inventory
 * If it isn't, set EventSystem to select the 'Return' button
 * Primarily used so the Inventory screen matches all the other menus
 */
public class InventorySelectTrigger : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool isSelected = true;

	public void OnSelect(BaseEventData data) {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        isSelected = false;
    }

    public bool CheckListSelected()
    {
        return isSelected;
    }
}
