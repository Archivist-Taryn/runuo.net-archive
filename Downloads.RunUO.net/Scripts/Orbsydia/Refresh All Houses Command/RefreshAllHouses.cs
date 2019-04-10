using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Multis;

namespace Server.Commands
{
	public class RefreshAllHousesCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "RefreshAllHouses", AccessLevel.Administrator, new CommandEventHandler( RefreshAllHouses_OnCommand ) );
		}
		
		[Usage( "RefreshAllHouses" )]
		[Description( "Refreshes all houses in the world." )]
		public static void RefreshAllHouses_OnCommand( CommandEventArgs e )
		{
			ArrayList houseslist = new ArrayList();
			int counter = 0;
			
			foreach (Item house in World.Items.Values)
            {
				if( house is BaseHouse )
					houseslist.Add(house);
            }
			
			if( houseslist.Count == 0 )
			{
				e.Mobile.SendMessage(39, "There are no houses in the world to refresh.");
				return;
			}
			
			foreach ( BaseHouse house in houseslist )
            {
				if( house is BaseHouse )
				{
					BaseHouse bh = (BaseHouse)house;
					bh.RefreshDecay();
					
					counter++;
				}
			}
			
			e.Mobile.SendMessage(1153, "{0} houses in the world have been refreshed.", counter.ToString());
		}
	}
}
