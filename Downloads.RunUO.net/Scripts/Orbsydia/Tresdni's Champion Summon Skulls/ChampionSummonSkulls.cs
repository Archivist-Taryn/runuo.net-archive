//Champion Summon Skulls
//By Tresdni - www.uofreedom.com

using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
	public class ChampionSummonSkullBarracoon: Item
	{
		
		[Constructable]
		public ChampionSummonSkullBarracoon() : base( 0x2203 )
		{
			Weight = 1.0;
			Name = "Champion Summon Skull (Barracoon)";
			Hue = 1779;
			LootType = LootType.Blessed;
		}

		public ChampionSummonSkullBarracoon( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( from.Region.IsPartOf( typeof( TownRegion ) ) )
			{
				from.SendMessage("You cannot use this in town.");
			}
			else if( from.Region.IsPartOf( typeof( DungeonRegion ) ) )
			{
				from.SendMessage("It would be better to use this in the wilderness somewhere.");
			}
			else if( from.Region.IsPartOf( typeof( HouseRegion ) ) )
			{
				from.SendMessage("It would be better to use this in the wilderness somewhere.");
			}
			else if ( from.Criminal )
			{
				from.SendLocalizedMessage( 1005403 ); // The magic of the stone cannot be evoked by the lawless.
			}
			else if ( Factions.Sigil.ExistsOn( from ) )
			{
				from.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
			}
			else
			{
				Movable = false;
				MoveToWorld( from.Location, from.Map );

				from.Animate( 32, 5, 1, true, false, 0 );

				new ChampSettleTimer( this, from.Location, from.Map, from ).Start();
			}
		}
		
		private class ChampSettleTimer : Timer
		{
			private Item m_Stone;
			private Point3D m_Location;
			private int m_Count;
			private Map m_Map;
			private Mobile m_Caster;

			public ChampSettleTimer( Item stone, Point3D loc, Map map, Mobile caster ) : base( TimeSpan.FromSeconds( 2.5 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Stone = stone;
				m_Location = loc;
				m_Map = map;
				m_Caster = caster;
			}

			protected override void OnTick()
			{
				++m_Count;

				if ( m_Count == 1 )
				{
					m_Stone.PublicOverheadMessage( MessageType.Regular, 0x3B2, 1005414 ); // The stone settles into the ground.
				}
				else if ( m_Count >= 10 )
				{
					m_Stone.Location = new Point3D( m_Stone.X, m_Stone.Y, m_Stone.Z - 1 );

					if ( m_Count == 16 )
					{	
						Barracoon bar = new Barracoon();

						bar.MoveToWorld( m_Location, m_Map );        
					
						m_Caster.SendMessage ( "The skull summons the Barracoon." );

						m_Stone.Delete();
						Stop();
					}
				}
			}
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
	}

	public class ChampionSummonSkullHarrower: Item
	{
		
		[Constructable]
		public ChampionSummonSkullHarrower() : base( 0x2203 )
		{
			Weight = 1.0;
			Name = "Champion Summon Skull (Harrower)";
			Hue = 1175;
			LootType = LootType.Blessed;
		}

		public ChampionSummonSkullHarrower( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( from.Region.IsPartOf( typeof( TownRegion ) ) )
			{
				from.SendMessage("You cannot use this in town.");
			}
			else if( from.Region.IsPartOf( typeof( DungeonRegion ) ) )
			{
				from.SendMessage("It would be better to use this in the wilderness somewhere.");
			}
			else if( from.Region.IsPartOf( typeof( HouseRegion ) ) )
			{
				from.SendMessage("It would be better to use this in the wilderness somewhere.");
			}
			else if ( from.Criminal )
			{
				from.SendLocalizedMessage( 1005403 ); // The magic of the stone cannot be evoked by the lawless.
			}
			else if ( Factions.Sigil.ExistsOn( from ) )
			{
				from.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
			}
			else
			{
				Movable = false;
				MoveToWorld( from.Location, from.Map );

				from.Animate( 32, 5, 1, true, false, 0 );

				new ChampSettleTimer( this, from.Location, from.Map, from ).Start();
			}
		}
		
		private class ChampSettleTimer : Timer
		{
			private Item m_Stone;
			private Point3D m_Location;
			private int m_Count;
			private Map m_Map;
			private Mobile m_Caster;

			public ChampSettleTimer( Item stone, Point3D loc, Map map, Mobile caster ) : base( TimeSpan.FromSeconds( 2.5 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Stone = stone;
				m_Location = loc;
				m_Map = map;
				m_Caster = caster;
			}

			protected override void OnTick()
			{
				++m_Count;

				if ( m_Count == 1 )
				{
					m_Stone.PublicOverheadMessage( MessageType.Regular, 0x3B2, 1005414 ); // The stone settles into the ground.
				}
				else if ( m_Count >= 10 )
				{
					m_Stone.Location = new Point3D( m_Stone.X, m_Stone.Y, m_Stone.Z - 1 );

					if ( m_Count == 16 )
					{	
						Harrower har = new Harrower();

						har.MoveToWorld( m_Location, m_Map );        
					
						m_Caster.SendMessage ( "The skull summons the Harrower." );

						m_Stone.Delete();
						Stop();
					}
				}
			}
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
	}
}	