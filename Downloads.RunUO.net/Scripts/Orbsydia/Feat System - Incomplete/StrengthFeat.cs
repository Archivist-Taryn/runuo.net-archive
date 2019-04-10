// Written by Celisuis ////////////////////////////////////////
// Originally Released at http://www.ultimate-coders.com //////
///////////////////////////////////////////////////////////////

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items.Feats
{
    public class StrengthFeat : Feat
    {
        private StrengthFeat StrFeat;
        [Constructable]
        public StrengthFeat()
            : base(0xE73)
        {
            Movable = true;
            Weight = 0.0;
            Name = "Titanic Strength";
            Visible = true;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.AddStatMod(new StatMod(StatType.Str, "Titanic Strength Feat", 50, TimeSpan.FromSeconds(30)));
            StrFeat.Delete();
        }

        public StrengthFeat(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}