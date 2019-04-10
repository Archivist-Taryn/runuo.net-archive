//Tribute to Baldurs Gate 2 by Paradyme.
using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Scripts.Commands;

namespace Server.Items
{
	[FlipableAttribute( 0x26CE, 0x26CF )]
	public class Lilarcor : BaseSword
	{
		public override int ArtifactRarity{ get{ return 15; } }
	 	public override int InitMinHits{ get{ return 250; } }
	 	public override int InitMaxHits{ get{ return 255; } }
		
		public override int DefMaxRange{ get{ return 2; } }
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 125; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 24; } }
		public override int AosSpeed{ get{ return 35; } }
		
		//Weapon Sound Hits
		public override int DefHitSound { get { return 567; } }
        //public override int DefMissSound { get { return 570; } }
        
        //Emote Sounds - Lilarcor is supposed to be annoying!
        //public override int DefHitSound { get { return 1050 + Utility.Random( 57 ); } }
        public override int DefMissSound { get { return 1050 + Utility.Random( 57 ); } }
        
        
        public virtual int AosHitSound { get { return DefHitSound; } }
        public virtual int AosMissSound { get { return DefMissSound; } }
        public virtual int OldHitSound { get { return DefHitSound; } }
        public virtual int OldMissSound { get { return DefMissSound; } }
		
		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 37; } }
		public override float MlSpeed{ get{ return 2.75f; } }
		
		
        private SkillMod m_SkillMod0;

		private DateTime timeTalk = DateTime.Now;
		
		[Constructable]
		public Lilarcor()
			: this("<BIG><BASEFONT COLOR=#CC3352>Lilarcor</BIG><BASEFONT COLOR=#FFFFFF>")
		{ }
		
		[Constructable]
		public Lilarcor( string name )
			: this(name, Utility.RandomMinMax( 2674, 2718 ))
		{ }
		
	 	[Constructable]
	 	public Lilarcor( string name, int hue ) 
	 		: base( 0x26CF )
	 	{
	 	 	Name = name;
	 	 	EngravedText = "<BASEFONT COLOR=#E69419>The Village Idiot<BASEFONT COLOR=#FFFFFF>";
	 	 	Hue = hue;
	 	 	Slayer = SlayerName.DragonSlaying;
	 	 	Attributes.AttackChance = 10;
	 	 	Attributes.DefendChance = 10;
	 	 	Attributes.WeaponDamage = 50;
	 	 	Attributes.WeaponSpeed = 35;
	 	 	WeaponAttributes.HitLowerDefend = 25;
			WeaponAttributes.HitLowerAttack = 25;
	 	 	WeaponAttributes.HitFireball = 25;

            DefineMods();
        }
        //Custom Ascii Text
        public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
				list.Add(1070722,
			 	"<BASEFONT COLOR=#8C94D9>+20 Swordsmanship Above Cap<BASEFONT COLOR=#FFFFFF>" 
			 			); 									
		}
		//Skillmod
        private void DefineMods()
        {
            m_SkillMod0 = new DefaultSkillMod(SkillName.Swords, true, 20);
        }

        private void SetMods(Mobile wearer)
        {
            wearer.AddSkillMod(m_SkillMod0);
        }

        public override bool OnEquip(Mobile from)
        {
            SetMods(from);
            return true;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile m = (Mobile)parent;
                m.RemoveStatMod("Lilarcor");

                if (m_SkillMod0 != null)
                    m_SkillMod0.Remove();
            }
        }

	 	public Lilarcor(Serial serial) : base( serial )
	 	{
	 	}
	 	
	 	public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 20; fire = 20; cold = 20; nrgy = 20; chaos = 0; direct = 0;
			pois = 20;
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

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus ) 
			{ 
				base.OnHit( attacker, defender, damageBonus );
				
				if( DateTime.Now >= timeTalk ) 
					{ 
						switch ( Utility.Random( 72 )) 
						{ 
							//Speech
							case 0: attacker.Say( "So, are we gonna kill something now?" ); break; 
							case 1: attacker.Say( "I am invincible, INVINCIBLE I say! *laughs*" ); break; 
							case 2: attacker.Say( "I know! Find someone rich, and kill them! Then find someone richer, and kill them too! Hack and slash your way to fortune! Whoo-hoo!!" ); break; 
							case 3: attacker.Say( "I'm sharp, I can come up with something... OK... find someone who knows what you want to know and threaten to kill them! Yeah! Then kill them! Woo-hoo!!!" ); break; 
							case 4: attacker.Say( "I know! Start swinging! Eventually you'll lop off the head of someone important and then the good fights will REALLY start!" ); break; 
							case 5: attacker.Say( "You know, once, long time ago, I was, like, a Moonblade." ); break; 
							case 6: attacker.Say( "Err... find that wizard guy. Yeah... find him and kill him. Kill kill kill kill KILL!! Whoo-hoo!!" ); break; 
							case 7: attacker.Say( "My brother's a +12 Hackmaster!" ); break; 
							case 8: attacker.Say( "Choke up, dolt, your grip's all-wrong!" ); break; 
							case 9: attacker.Say( "What's my status? Since when do you care about me unless I'm impaled in something's guts? Oh well, fine, let me think for a minute... Well, as a matter of fact I would like to register a complaint." ); break;
							case 10: attacker.Say( "Murder! Death! Kill! Murder! Death! Kill! Bouah-ha-ha-ha!" ); break; 
							case 11: attacker.Say( "Hands up, kiddies, who wants to die?" ); break; 
							case 12: attacker.Say( "(sigh) ...come on..." ); break; 
							case 13: attacker.Say( "(double sigh) Rassa-frackin' (grumbling) c'mon-c'mon-C'MON!!!" ); break; 
							case 14: attacker.Say( "Come get some! Boo-yah!" ); break; 
							case 15: attacker.Say( "YOINK!!! Got your nose!" ); break; 
							case 16: attacker.Say( "You really need to clean me. I like to shine! Ha-ha-ha!" ); break; 
							case 17: attacker.Say( "Kill it! Kill it quick before they're all gone!" ); break; 
							case 18: attacker.Say( "Mwoo-ha-ha-ha-ha-ha-ha!" ); break;
							case 19: attacker.Say( "Swish! Hot butta!" ); break;  
							case 20: attacker.Say( "I refuse to answer any more questions until I'm cleaned and polished thoroughly. Grab a rag already!" ); break;
							case 21: attacker.Say( "I'm the best at what I do, and what I do ain't pretty! *laughs*" ); break;
							case 22: attacker.Say( "I think you need to take better care of me. I've got more chips than a blind beaver! I look like a second rate pig-poker!" ); break;
							case 23: attacker.Say( "You talking to me?" ); break;
							case 24: attacker.Say( "I love the smell of daisies in the morning!" ); break;
							case 25: attacker.Say( "Listen beefy, I may be an intelligent sword, but I've had no formal edjumacation." ); break;
							case 26: attacker.Say( "You know, my last owner always said I was 'sharp' and 'edgy'. He was such an ass." ); break;
							case 27: attacker.Say( "And that's for grandma, who said I'd never amount to anything more than a butterknife!" ); break;
							case 28: attacker.Say( "Kill! Kill! Kill! Yeah cool!!" ); break;
							case 29: attacker.Say( "I don't know what you have expected, but as a sword I'm pretty one-dimensional in what I waaant!!!" ); break;
							case 30: attacker.Say( "I don't chop wood, OK? I'm not an axe!" ); break;
							case 31: attacker.Say( "I want to kill a dragon. Right now. Go find one and kill it. That would be SO cool." ); break;
							case 32: attacker.Say( "Can we go kill something now, huh?" ); break;
							case 33: attacker.Say( "How about now?  No?" ); break;
							case 34: attacker.Say( "Come on let's kill something NOW!" ); break;
							case 35: attacker.Say( "mmmm.... now?" ); break;
							case 36: attacker.Say( "What about now?" ); break;
							case 37: attacker.Say( "Now!  Now!  Kill something now!! Yeah!" ); break;
							case 38: attacker.Say( "Now?  Please?  Pretty please?" ); break;
							case 39: attacker.Say( "Can we go whack something now?" ); break;
							case 40: attacker.Say( "Let's whack something eeeeevvvvillllll...." ); break;
							case 41: attacker.Say( "Why don't we go kill that over there?" ); break;
							case 42: attacker.Say( "Are we going to kill something now, maybe?  Huh?" ); break;
							case 43: attacker.Say( "Booooo-ring!" ); break;
							case 44: attacker.Say( "(sigh)" ); break;
							case 45: attacker.Say( "(double sigh)" ); break;
							case 46: attacker.Say( "Wanna go kill that over there? C'mon, let's kill somthin'!" ); break;
							case 47: attacker.Say( "You deal, I'll cut!" ); break;
							case 48: attacker.Say( "Let's see what's inside this one! Yeah!" ); break;
							case 49: attacker.Say( "Choke up, dolt, your grip is all wrong!" ); break;
							case 50: attacker.Say( "Hands up, kiddies, who wants to die?!" ); break;
							case 51: attacker.Say( "Mmmm... tastes like chicken!" ); break;
							case 52: attacker.Say( "Sissy fighter!  You grab, I'll scratch!'" ); break;
							case 53: attacker.Say( "Muwahahaha-ha-ha!!" ); break;
							case 54: attacker.Say( "You can't be serious!" ); break;
							case 55: attacker.Say( "Ooo, that'll leave a mark!" ); break;
							case 56: attacker.Say( "Murder!  Death!!  KILL!!!" ); break;
							case 57: attacker.Say( "Who's your daddy!" ); break;
							case 58: attacker.Say( "We got a gusher!" ); break;
							case 59: attacker.Say( "Are YOU talking to ME?!!" ); break;
							case 60: attacker.Say( "Some of my finest work!" ); break;
							case 61: attacker.Say( "I'm the best at what I do, and what I do ain't pretty!" ); break;
							case 62: attacker.Say( "Yeah! Hit it!  Hit it again!" ); break;
							case 63: attacker.Say( "Wouldn't it be cool if you could dual-wield me?" ); break;
							case 64: attacker.Say( "YOINK!  Got yer nose!" ); break;
							case 65: attacker.Say( "Swing harder!  Swing harder!!" ); break;
							case 66: attacker.Say( "Bring 'em on!  I ain't done!" ); break;
							case 67: attacker.Say( "Oh yeah!" ); break;
							case 68: attacker.Say( "You feel lucky, punk?" ); break;
							case 69: attacker.Say( "Oooh, throw a coin in that fountain! Hahaha!" ); break;
							case 70: attacker.Say( "Oooh, I'm shaking! Haha!" ); break;
							
							// Poison Chance		
							case 71: defender.ApplyPoison( attacker, Poison.Lethal );
									 attacker.SendMessage( "Lilarcor: Oop's, i must be dirty, he looks ill!" ); break;					
							
						}
					
					timeTalk = DateTime.Now + TimeSpan.FromSeconds(30.0);
				}
			//Critical Hit
			if(attacker == null || defender == null)
            return;
				//Exceptions Go Here, 1% chance for success.
            	if( defender is BaseCreature && .01 >= Utility.RandomDouble())
            	{
               		if( defender is BaseChampion || defender is BasePeerless || defender is Harrower )
                	{
                    	attacker.SendMessage( "Lilarcor: Attack Roll 1 + 5 = 6 : Miss" );
                    	return;
                	}
                	else
                	{
                    	defender.Kill();
                    	attacker.SendMessage( "Lilarcor: Attack Roll 19 + 5 = 24 : Critical Hit!" );
                	}

            	}
				
			}
		}
	}