using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	public class RechargeScroll : Item
	{
		[Constructable]
		public RechargeScroll() : this( 1 )
		{
		}

		[Constructable]
		public RechargeScroll(int amount) : base( 0x1F65)
		{
			Name = "Recharge Scroll";
			Stackable = true;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool CanUse = from.CheckSkill( SkillName.Magery, 50, 80 );
			if (CanUse)
			{
				if (this.IsChildOf( from.Backpack ))
				{
					from.SendMessage("Target item to Recharge");
					from.Target = new RechargeTarget(this);
				}
				else
					from.SendMessage("That must be in your backpack to use");
			}
			else
				from.SendMessage("You can't make sense of the letters");
		}

		private class RechargeTarget : Target
		{
			private RechargeScroll rs;

			public RechargeTarget(RechargeScroll s) : base( 10, false, TargetFlags.None )
			{
				rs = s;
			}
	
			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is Item)
				{
					Item item = (Item) targ;
					if ( from.InRange( item.GetWorldLocation(), 1 ) )
					{
						if (targ is BaseWand)
						{
							BaseWand bw = (BaseWand) targ;
							int addnum = Utility.RandomMinMax(10,30);
							if ((bw.Charges + addnum) > 30) 
							{
								bw.Charges = 0;
								Effects.PlaySound( bw.Location, bw.Map, 0x1E4 );
							}
							else
							{
								bw.Charges += addnum;
								Effects.PlaySound( bw.Location, bw.Map, 0x1EB );
							}
							Effects.SendLocationParticles( EffectItem.Create( bw.Location, bw.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
							rs.Delete();
						}
						else if (targ is IdentifyCrystalBall)
						{
							IdentifyCrystalBall icb = (IdentifyCrystalBall) targ;
							int addnum = Utility.RandomMinMax(10,30);
							if ((icb.Charges + addnum) > 30) 
							{
								icb.Charges = 0;
								Effects.PlaySound( icb.Location, icb.Map, 0x1E4 );
							}
							else
							{
								icb.Charges += addnum;
								Effects.PlaySound( icb.Location, icb.Map, 0x1EB );
							}
							Effects.SendLocationParticles( EffectItem.Create( icb.Location, icb.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
							rs.Delete();
						}
						else
							from.SendMessage("That can't be recharged");
					}
					else
						from.SendMessage("That's too far away");
				}
			}
		}

		public RechargeScroll( Serial serial ) : base( serial )
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