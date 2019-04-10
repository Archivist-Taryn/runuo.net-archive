// MagicKey.cs by Alari (alarihyena@gmail.com)

using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{

/*
You will have to add "Magic = 0x1012" to KeyType in \scripts\items\misc\key.cs

	public enum KeyType
	{
		... ,   // remember the comma
		Magic  = 0x1012
	}
*/

	public class MagicKey : Key // Key
	{
		[Constructable]
		public MagicKey() : base( KeyType.Magic )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;
			int number;

			if ( !IsChildOf( from.Backpack ) )
			{
				number = 1060640;  // The item must be in your backpack to use it.
			}
			else
			{
				number = 501662;  // What shall I use this key on?
				t = new UnlockTarget( this );
				from.Target = t;
			}

			from.SendLocalizedMessage( number );
		}

		private class UnlockTarget : Target
		{
			private Key m_Key;

			public UnlockTarget( Key key ) : base( key.MaxRange, false, TargetFlags.None )
			{
				m_Key = key;
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int number = -1;

				if ( !m_Key.IsChildOf( from.Backpack ) )
				{
					number = 1060640;  // The item must be in your backpack to use it.
				}

				else if ( targeted == m_Key )
				{
					number = 501666;  // You can't unlock that!
				}

// I added this. The basedoor check below "if ( o.Locked )" also seemed to check for housedoor type situations.

				else if ( targeted is BaseHouseDoor )  // house door check
				{
					number = 501666; // You can't unlock that!
				}

// does this do what I think it does? edit: Nope, don't work at all. :>
// Seems unnecessary tho, basic testing already showed secures seem to be OK, but better safe than sorry.

//				else if ( targeted is LockableContainer && !((LockableContainer)targeted).UseLocks() )  // secure container check??
//				{
//					number = 501666;  // You can't unlock that!
//				}

				else if ( targeted is ILockable )
				{
					ILockable o = (ILockable)targeted;

					if ( o.Locked )
					{
						if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )  // this seems to check house doors also
						{
							number = 501668; // This key doesn't seem to unlock that.
						}
						else
						{
							o.Locked = false;

							if ( o is LockableContainer )
							{
								LockableContainer cont = (LockableContainer)o;

								if ( cont.LockLevel == -255 )
									cont.LockLevel = cont.RequiredSkill - 10;

								cont.Picker = from;  // new, sets "lockpicker" to the user.
							}

// Traps don't seem to get disabled at all in the regular key.cs. Bug?
							if ( o is TrapableContainer )
							{
								TrapableContainer cont = (TrapableContainer)o;

								if ( cont.TrapType != TrapType.None )
									cont.TrapType = TrapType.None; // this stumped me for reasons I don't care to admit. :>
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;
								item.SendLocalizedMessageTo( from, 1048001 ); // you unlock it
							}

							// m_Key.Delete(); // I think this is a no-no.
							m_Key.Consume();   // bye bye key =)
							from.SendMessage( "The magic key vanishes!" );

						}
					}
					else
					{
						number = 501668; // This key doesn't seem to unlock that.
					}
				}
				else
				{
					number = 501666; // You can't unlock that!
				}

				if ( number != -1 )
				{
					from.SendLocalizedMessage( number );
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "(single use)" );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			from.Send( new UnicodeMessage( Serial, ItemID, MessageType.Regular, 0x3B2, 3, "ENU", "", "(single use)" ) );
		}

		public MagicKey( Serial serial ) : base( serial )
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

			int version = reader.ReadInt();
		}
	}
}


/*

MODIFY - Scripts\Items\Misc\Keys.Cs to the following:

	public enum KeyType
	{
		Copper = 0x100E,
		Gold   = 0x100F,
		Iron   = 0x1010,
		Rusty  = 0x1013,       // remember comma
		Magic  = 0x1012        // add this
	}

And a friendly reminder: If you sell it in stores,
remember to take into account the gold gained from locked and
trapped treasure chests on your shard. ;)

*/