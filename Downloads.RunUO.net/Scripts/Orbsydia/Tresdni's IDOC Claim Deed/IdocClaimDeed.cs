//IDOC Claim Deed
//By Tresdni - www.uofreedom.com
using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class IdocClaimTarget : Target 
	{
		private IdocClaimDeed m_Deed;
	
		public IdocClaimTarget( IdocClaimDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}
	
		protected override void OnTarget( Mobile from, object target ) 
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;
		
			if ( target is HouseSign )
			{
				HouseSign item = (HouseSign)target;
				BaseHouse bh = item.m_Owner;
		
				if ( bh.DecayLevel != DecayLevel.IDOC )
				{
					from.SendMessage ( "This house is not in danger of collapsing." );
				}
				else
				{
					if( !BaseHouse.HasAccountHouse(from) )
					{
					
						//clear the current house info and do the transfer
						bh.RemoveKeys( bh.Owner );
					
						bh.Owner = from;
						bh.Bans.Clear();
						bh.Friends.Clear();
						bh.CoOwners.Clear();
						bh.ChangeLocks( from );
						bh.LastTraded = DateTime.Now;
						bh.RefreshDecay();
					
						from.SendMessage("You are now the proud owner of this IDOC.  The house has been refreshed.");
					
						m_Deed.Delete(); // Delete the deed
					}
					else
					{
						from.SendMessage("You cannot own another house at this time.");
					}
				}
			}
			else
			{
				from.SendMessage ( "You must target a house sign!" );
			}
		}
	}

	public class IdocClaimDeed : Item 
	{
		
		[Constructable]
		public IdocClaimDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 1159;
			LootType = LootType.Blessed;
			Name = "An Idoc Claim Deed";
				
		}

		public IdocClaimDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}


		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage ( "Which IDOC would you like to claim complete ownership of?" );
				from.Target = new IdocClaimTarget( this ); // Call our target
			 }
		}	
	}
}