using System;
using Characters.Player.Scripts;
using MoreMountains.InventoryEngine;
using UnityEngine;

namespace Resources.Items.ItemClasses
{	
	[CreateAssetMenu(fileName = "RangedTool", menuName = "Items/RangedTool", order = 2)]
	[Serializable]
	public class RangedTool : InventoryItem 
	{
		[Header("Ranged Tool")]
		public Sprite RangedToolSprite;

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool Equip(string playerID)
		{
			base.Equip(playerID);
			TargetInventory(playerID).TargetTransform.GetComponent<PlayerCharacter>().SetRangedTool(RangedToolSprite,this);
			return true;
		}

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool UnEquip(string playerID)
		{
			base.UnEquip(playerID);
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetWeapon(null,this);
			return true;
		}
		
	}
}