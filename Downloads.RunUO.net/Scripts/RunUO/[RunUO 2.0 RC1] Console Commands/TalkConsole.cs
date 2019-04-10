using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Commands
{
    public class TalkConsole
    {
        public static void Initialize()
        { CommandSystem.Register("tc", AccessLevel.Seer, new CommandEventHandler(TC_OnCommand)); }

        public static void TC_OnCommand(CommandEventArgs e)
        {
            Console.WriteLine("Message from '" + e.Mobile.Name + "': " + e.ArgString + "");
            e.Mobile.SendMessage("Message Sent");
        }
    }
}
