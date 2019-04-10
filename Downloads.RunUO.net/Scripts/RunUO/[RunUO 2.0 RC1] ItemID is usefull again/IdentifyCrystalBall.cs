using System; 
using Server; 
using Server.Targeting;
using Server.Network; 

namespace Server.Items 
{ 
	public class IdentifyCrystalBall : Item 
	{ 
		private int m_charges;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Charges
		{
			get { return m_charges; }
			set { m_charges = value; }
		}
	
      	[Constructable] 
      	public IdentifyCrystalBall() : base( 0xE2E ) 
      	{ 
      	   this.Name = "a crystal ball"; 
      	   this.Weight = 10; 
      	   this.Stackable = false; 
      	   //this.LootType = LootType.Blessed; 
      	   this.Light = LightType.Circle150; 
      	} 

		private DateTime LastActiveCheck = DateTime.Now;
		private TimeSpan ActiveCheckDelay = TimeSpan.FromSeconds( 600.0 );
		public bool CanCheckActive()
		{
			 if( LastActiveCheck.Add( ActiveCheckDelay ) < DateTime.Now )
				  return true;
			 return false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ((m_charges > 0) || CanCheckActive())
			{
				if ( from.InRange( this.GetWorldLocation(), 1 ) )
				{
					this.PublicOverheadMessage( MessageType.Regular, 0x0 , false, "What do you wish to know about?");
					from.Target = new IdentifyTarget(this);
				}
				else
				{
					from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that.
				}
			}
			else
				this.PublicOverheadMessage( MessageType.Regular, 0x0 , false, "I'm tired now.  Go Away!");
		}

		private class IdentifyTarget : Target
		{
			private IdentifyCrystalBall ball;

			public IdentifyTarget(IdentifyCrystalBall b) : base( 10, false, TargetFlags.None )
			{
				ball = b;
			}
	
			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseArmor)
				{
					BaseArmor item = (BaseArmor) targ;
					if (item.IsChildOf( from.Backpack ))
					{
						if (!item.Identified)
						{
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("Your armor has been identified"));
							item.Identified = true;
							ball.LastActiveCheck = DateTime.Now;
							if (ball.Charges > 0) ball.Charges -= 1;
						}
						else
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That has already been identified."));
					}
					else
						ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That must be in your pack to identify"));
				}
				else if (targ is BaseJewel)
				{
					BaseJewel item = (BaseJewel) targ;
					if (item.IsChildOf( from.Backpack ))
					{
						if (!item.Identified)
						{
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("Your jewelry has been identified"));
							item.Identified = true;
							ball.LastActiveCheck = DateTime.Now;
							if (ball.Charges > 0) ball.Charges -= 1;
						}
						else
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That has already been identified."));
					}
					else
						ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That must be in your pack to identify"));
				}
				else if (targ is BaseWeapon)
				{
					BaseWeapon item = (BaseWeapon) targ;
					if (item.IsChildOf( from.Backpack ))
					{
						if (!item.Identified)
						{
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("Your weapon has been identified"));
							item.Identified = true;
							ball.LastActiveCheck = DateTime.Now;
							if (ball.Charges > 0) ball.Charges -= 1;
						}
						else
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That has already been identified."));
					}
					else
						ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That must be in your pack to identify"));
				}
				else if (targ is BaseClothing)
				{
					BaseClothing item = (BaseClothing) targ;
					if (item.IsChildOf( from.Backpack ))
					{
						if (!item.Identified)
						{
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("Your clothing has been identified"));
							item.Identified = true;
							ball.LastActiveCheck = DateTime.Now;
							if (ball.Charges > 0) ball.Charges -= 1;
						}
						else
							ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That has already been identified."));
					}
					else
						ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("That must be in your pack to identify"));
				}
				else 
					ball.PublicOverheadMessage( MessageType.Regular, 0x0 , false, ("I can't identify that!"));
			}
		}

		public IdentifyCrystalBall( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 

			writer.Write( (int) m_charges);
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			m_charges = reader.ReadInt(); 
		} 
	} 
}