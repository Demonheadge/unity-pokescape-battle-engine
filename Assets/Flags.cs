using Unity.VisualScripting;
using UnityEngine;


namespace PokeScape.Unity
{
    public class Flags
    {
        // Opponent trainer count copied from include/constants/opponents.h
        public const int MAX_TRAINERS_COUNT = 864;
        public const int MAX_ITEM_COUNT = 999;

    // Temporary Flags
        // These temporary flags are cleared every time a map is loaded.
        public bool FLAG_TEMP_1;
        public bool FLAG_TEMP_2;
        public bool FLAG_TEMP_3;
        public bool FLAG_TEMP_4;
        public bool FLAG_TEMP_5;
        public bool FLAG_TEMP_6;
        public bool FLAG_TEMP_7;
        public bool FLAG_TEMP_8;
        public bool FLAG_TEMP_9;
        public bool FLAG_TEMP_A;
        public bool FLAG_TEMP_B;
        public bool FLAG_TEMP_C;
        public bool FLAG_TEMP_D;
        public bool FLAG_TEMP_E;
        public bool FLAG_TEMP_F;
        public bool FLAG_TEMP_10;
        public bool FLAG_TEMP_11;
        public bool FLAG_TEMP_12;
        public bool FLAG_TEMP_13;
        public bool FLAG_TEMP_14;
        public bool FLAG_TEMP_15;
        public bool FLAG_TEMP_16;
        public bool FLAG_TEMP_17;
        public bool FLAG_TEMP_18;
        public bool FLAG_TEMP_19;
        public bool FLAG_TEMP_1A;
        public bool FLAG_TEMP_1B;
        public bool FLAG_TEMP_1C;
        public bool FLAG_TEMP_1D;
        public bool FLAG_TEMP_1E;
        public bool FLAG_TEMP_1F;


    //Trade Flags
        public bool FLAG_LUMBRIDGE_TRADE_1;
        public bool FLAG_BKFORT_TRADE_1;

        

    // Scripts
        public bool FLAG_A_NUB_TOT_TUTORIAL;
        public bool FLAG_MITHRILMAN2;
        public bool FLAG_MITHRILMAN3;
        public bool FLAG_MITHRILMAN4;
        public bool FLAG_MITHRILMAN5;
        public bool FLAG_MITHRILMAN6;
        public bool FLAG_MITHRILMAN7;
        public bool FLAG_MITHRILMAN8;
        public bool FLAG_MITHRILMAN9;
        public bool FLAG_MITHRILMAN10;
        public bool FLAG_TZHAAR_RANDOM;
        public bool FLAG_EVENT_PORTSARIM_HAM_BOAT;
        public bool FLAG_RECEIVED_WIZARDSHAT;
        public bool FLAG_MOD_TIMBO;
        public bool FLAG_DUNGEONEERING_DOOR_ROUTE29;
        public bool FLAG_ABYSS_PORTALS_ENABLED;
        public bool FLAG_HAIRDRESSER;
        public bool FLAG_SANDWICH_LADY;
        public bool FLAG_RECEIVED_TERROBIRD_MOUNT;
        public bool FLAG_RANDOM_EVENT;
        public bool FLAG_BRASS_KEY;
        public bool FLAG_EVENT_TEAM_JATIZSO;
        public bool FLAG_EVENT_TEAM_NEITIZNOT;
        public bool FLAG_DG_DOOR_VARROCK_SEWERS;
        public bool FLAG_LIGHTHOUSE_PIRATE;
        public bool FLAG_PENGUIN_HUNT_ON_ROUTE;
        public bool FLAG_TZHAAR_FOLLOW_GYM_INFO;
        public bool FLAG_ALKHARIDGYM_1;
        public bool FLAG_ALKHARIDGYM_2;
        public bool FLAG_ALKHARIDGYM_4;
        public bool FLAG_ALKHARIDGYM_3;
        public bool FLAG_VARROCKGYM_BOOK_1;
        public bool FLAG_VARROCKGYM_BOOK_2;
        public bool FLAG_VARROCKGYM_BOOK_3;
        public bool FLAG_VARROCKGYM_BOOK_4;
        public bool FLAG_URI_ITEMFINDER;
        public bool FLAG_POKESCAPE_RIMMINGTON_HAM_LOST_TIME;        
        public bool FLAG_MIND_TALISMAN;
        public bool FLAG_MALIGNIUS_BATTLE;
        public bool FLAG_ROCKS_ROUTE23;
        public bool FLAG_BKF_JAILED;
        public bool FLAG_HAM_JAILED;
        public bool FLAG_MOD_ASH_FANFIC;
        public bool FLAG_ROUTE13_BLOCK;
        public bool FLAG_HAM_HIDEOUT_DEFEATED_GUARDS_TO_LEAVE;
        public bool FLAG_BEATEN_FIGHTCAVES;
        public bool FLAG_NORMALTREE_ASSISTANT_1;
        public bool FLAG_NORMALTREE_ASSISTANT_2;
        public bool FLAG_NORMALTREE_ASSISTANT_3;
        public bool FLAG_NORMALTREE_ASSISTANT_4;
        public bool FLAG_NORMALTREE_ASSISTANT_5;
        public bool FLAG_NORMALTREE_ASSISTANT_6;
        public bool FLAG_NORMALTREE_ASSISTANT_7;
   
    //Quests
        public bool FLAG_ITEM_DRAYNORMANOR_FISHFOOD;
        public bool FLAG_ITEM_DRAYNORMANOR_RUBBERTUBE;
        public bool FLAG_ITEM_DRAYNORMANOR_OILCAN;
        public bool FLAG_BKF_QUIZ_1;
        public bool FLAG_BKF_QUIZ_2;
        public bool FLAG_BKF_QUIZ_3;
        public bool FLAG_BKF_QUIZ_4;
        public bool FLAG_BKF_QUIZ_5;
        public bool FLAG_BKF_QUIZ_6;
        public bool FLAG_BKF_QUIZ_7;
        public bool FLAG_COOKS_ASSIST_STARTED;
        public bool FLAG_COOKS_ASSIST_FINISHED;
        public bool FLAG_DUKE_TALKED;
        
    //Monsters
        public bool FLAG_SEA_TROLL;
        public bool FLAG_CAPTURED_GIANT_MOLE;
        public bool FLAG_SINKHOLE_STALKER_1;
        public bool FLAG_JUNA;
        public bool FLAG_CAPTURED_SNOWIMP;
        public bool FLAG_RECIEVED_EX_EX_PARROT;
        public bool FLAG_RECEIVED_SARADOMIN_EGG;
        public bool FLAG_RECEIVED_ZAMORAK_EGG;
        public bool FLAG_RECEIVED_BANDLING_EGG;
        public bool FLAG_RECEIVED_ZAROLING_EGG;
        public bool FLAG_RECEIVED_SERELING_EGG;
        public bool FLAG_RECEIVED_UNGODLING_EGG;
        public bool FLAG_RECEIVED_ARMALING_EGG;
        public bool FLAG_RECEIVED_RAVENLING_EGG;
        public bool FLAG_RECEIVED_BASILISK_EGG;
        public bool FLAG_RECEIVED_TUMEKLING_EGG;
        public bool FLAG_GIFT_BIRDS_NEST;
        public bool FLAG_CAPTURED_VORKATH;
        public bool FLAG_SINKHOLE_BEHEMOTH_1;
        public bool FLAG_HAM_HIDEOUT_GIFTMON;
        public bool FLAG_RECEIVED_YAK;
        public bool FLAG_CAUGHT_ELVARG;
        public bool FLAG_RECEIVED_REGULAR_EGG;
        public bool FLAG_RECEIVED_GUTHIX_EGG;
        public bool FLAG_RECIEVED_KITTEN;
        public bool FLAG_KALPHITE_KING_DEFEATED;
        public bool FLAG_KALPHITE_QUEEN_DEFEATED;

    
    //Core Game Flags
        public bool FLAG_DISABLE_RUN;
        public bool FLAG_ADVENTURE_STARTED;
        public bool FLAG_SET_WALL_CLOCK;
        public bool FLAG_RECEIVED_EXP_SHARE;
        public bool FLAG_EXP_ALL;
        public bool FLAG_RECEIVED_RUNNING_SHOES;
        public bool FLAG_RECEIVED_SMALL_FISHING_NET;
        public bool FLAG_PARTNER_BATTLE;
        public bool FLAG_TOGGLE_FORCED_WILD_DOUBLE_BATTLES;
        public bool FLAG_FORCE_SHINY;
        public bool FLAG_ENABLE_MEGA_EVOLUTIONS;
        public bool FLAG_POKESCAPE_TIME_RESET_MORNING;
        public bool FLAG_POKESCAPE_TIME_RESET_DAY;
        public bool FLAG_POKESCAPE_TIME_RESET_EVENING;
        public bool FLAG_POKESCAPE_TIME_RESET_NIGHT;
        public bool FLAG_POKESCAPE_USECUSTOM_POOL_LEVEL;
        public bool FLAG_GAMEMODE_SCALE_EVOLUTION;
        public bool FLAG_GAMEMODE_MONSTER_SPAWN;
        public bool FLAG_SKY_BATTLE;            
        public bool FLAG_TOGGLE_NO_CATCHING;    
        public bool FLAG_TOGGLE_NO_BAG_USE;     
        public bool FLAG_TOGGLE_INVERSE_BATTLE; 
        public bool FLAG_SUPPRESS_SPEAKER_NAME;
        public bool FLAG_ENABLE_P2P_BADGES;    

    //Diango Gifts
        public bool FLAG_DIANGO_GIFT_DRAGONKITE;
        public bool FLAG_DIANGO_GIFT_FROGEELBURGER;
        public bool FLAG_DIANGO_GIFT_SPARKLES24;

    //Dungeoneering
        public bool FLAG_DUNGEONEERING_ITEM_1;
        public bool FLAG_DUNGEONEERING_ITEM_2;
        public bool FLAG_DUNGEONEERING_ITEM_3;
        public bool FLAG_DUNGEONEERING_ITEM_4;
        public bool FLAG_DUNGEONEERING_ITEM_5;
        public bool FLAG_DUNGEONEERING_ITEM_6;
        public bool FLAG_DUNGEONEERING_ITEM_7;
        public bool FLAG_DUNGEONEERING_ITEM_8;
        public bool FLAG_DUNGEONEERING_ITEM_9;
        public bool FLAG_DUNGEONEERING_ITEM_10;
        public bool FLAG_DUNGEONEERING_ITEM_11;
        public bool FLAG_DUNGEONEERING_ITEM_12;
        public bool FLAG_DUNGEONEERING_ITEM_13;
        public bool FLAG_DUNGEONEERING_ITEM_14;
        public bool FLAG_DUNGEONEERING_ITEM_15;
        public bool FLAG_DUNGEONEERING_ITEM_16;
        public bool FLAG_DG_PUZZLE_ROOM_1;
        public bool FLAG_DG_PUZZLE_ROOM_2;
        public bool FLAG_DG_PUZZLE_ROOM_3;
        public bool FLAG_DG_PUZZLE_ROOM_4;
        public bool FLAG_DG_PUZZLE_ROOM_5;
        public bool FLAG_DG_PUZZLE_ROOM_6;
        public bool FLAG_DG_PUZZLE_ROOM_7;
        public bool FLAG_DG_PUZZLE_ROOM_8;

    //Visited Locations
        public bool FLAG_VISITED_LUMBRIDGE;
        public bool FLAG_VISITED_DRAYNOR;
        public bool FLAG_VISITED_PORT_SARIM;
        public bool FLAG_VISITED_RIMMINGTON;
        public bool FLAG_VISITED_TAVERLEY;
        public bool FLAG_VISITED_FALADOR;
        public bool FLAG_VISITED_BARBARIAN_VILLAGE;
        public bool FLAG_VISITED_VARROCK;
        public bool FLAG_VISITED_EDGEVILLE;
        public bool FLAG_VISITED_AL_KHARID;
        public bool FLAG_VISITED_MUSA_POINT;
        public bool FLAG_VISITED_MOR_UL_REK;
        public bool FLAG_VISITED_DAEMONHEIM;
        public bool FLAG_VISITED_WILDERNESS_CRATER;

    //Badges
        public bool FLAG_BADGE01_GET;
        public bool FLAG_BADGE02_GET;
        public bool FLAG_BADGE03_GET;
        public bool FLAG_BADGE04_GET;
        public bool FLAG_BADGE05_GET;
        public bool FLAG_BADGE06_GET;
        public bool FLAG_BADGE07_GET;
        public bool FLAG_BADGE08_GET;

    
    //daily flags
        public bool FLAG_GOODIE_BAG; // NIGHTMARERH GOODIE BAG 
        public bool FLAG_HIDDEN_GROTTO_1; 
        public bool FLAG_HIDDEN_GROTTO_2; 
        public bool FLAG_HIDDEN_GROTTO_3; 
        public bool FLAG_HIDDEN_GROTTO_4; 
        public bool FLAG_HIDDEN_GROTTO_5; 
        public bool FLAG_HIDDEN_GROTTO_6; 
        public bool FLAG_HIDDEN_GROTTO_7; 
        public bool FLAG_HIDDEN_GROTTO_8; 
        public bool FLAG_HIDDEN_GROTTO_9; 
        public bool FLAG_HIDDEN_GROTTO_10; 
        public bool FLAG_HIDDEN_GROTTO_11; 
        public bool FLAG_HIDDEN_GROTTO_12; 
        public bool FLAG_HIDDEN_GROTTO_13; 
        public bool FLAG_HIDDEN_GROTTO_14; 
        public bool FLAG_HIDDEN_GROTTO_15; 
        public bool FLAG_PUB_TRAINER_1;  //Port Sarim - Pirate
        public bool FLAG_PUB_TRAINER_2;  //Port Sarim - Pirate
        public bool FLAG_PUB_TRAINER_3;  //Port Sarim - Sailor 
        public bool FLAG_PUB_TRAINER_4;  //Port Sarim - Barbarian (PubCrawl)
        public bool FLAG_PUB_TRAINER_5;  //Falador 1 - Guard 
        public bool FLAG_PUB_TRAINER_6;  //Falador 2 - White Knight
        public bool FLAG_PUB_TRAINER_7;  //Falador 3 - Black Knight
        public bool FLAG_PUB_TRAINER_8;  //Falador 4 - Dwarf
        public bool FLAG_PUB_TRAINER_9;  //Falador - Barbarian (PubCrawl)
        public bool FLAG_PUB_TRAINER_10;  //Musa Point - Sailor
        public bool FLAG_PUB_TRAINER_11;  //Musa Point - Barbarian (PubCrawl) 
        public bool FLAG_PUB_TRAINER_12; 
        public bool FLAG_PUB_TRAINER_13; 
        public bool FLAG_PUB_TRAINER_14; 
        public bool FLAG_PUB_TRAINER_15; 
        public bool FLAG_PUB_TRAINER_16; 
        public bool FLAG_PUB_TRAINER_17; 
        public bool FLAG_PUB_TRAINER_18; 
        public bool FLAG_PUB_TRAINER_19; 
        public bool FLAG_PUB_TRAINER_20; 
        




// Item Flags
        public bool FLAG_ITEM_LUMBRIDGE_1;
        public bool FLAG_ITEM_LUMBRIDGE_2;
        public bool FLAG_ITEM_LUMBRIDGE_3;
        public bool FLAG_ITEM_LUMBRIDGE_4;
        public bool FLAG_ITEM_LUMBRIDGE_5;
        public bool FLAG_ITEM_LUMBRIDGE_6;
        public bool FLAG_ITEM_ROUTE1_1;
        public bool FLAG_ITEM_ROUTE1_2;
        public bool FLAG_ITEM_ROUTE1_3;
        public bool FLAG_ITEM_ROUTE1_4;
        public bool FLAG_ITEM_ROUTE1_5;
        public bool FLAG_ITEM_MILLLANEMILL_1;
        public bool FLAG_ITEM_MILLLANEMILL_2;
        public bool FLAG_ITEM_ROUTE12_1;
        public bool FLAG_ITEM_ROUTE12_2;
        public bool FLAG_ITEM_ROUTE12_3;
        public bool FLAG_ITEM_ROUTE12_4;
        public bool FLAG_ITEM_ROUTE12_5;
        public bool FLAG_ITEM_ROUTE12_6;
        public bool FLAG_ITEM_LUMBRIDGESWAMP_1;
        public bool FLAG_ITEM_LUMBRIDGESWAMP_2;
        public bool FLAG_ITEM_LUMBRIDGESWAMP_3;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_1;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_2;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_3;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_4;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_5;
        public bool FLAG_ITEM_LUMBRIDGESWAMPCAVE_6;
        public bool FLAG_ITEM_LUMBRIDGEFOREST_1;
        public bool FLAG_ITEM_LUMBRIDGEFOREST_2;
        public bool FLAG_ITEM_LUMBRIDGEFOREST_3;
        public bool FLAG_ITEM_DRAYNORMANOR_1;
        public bool FLAG_ITEM_DRAYNORMANOR_2;
        public bool FLAG_ITEM_DRAYNORMANOR_3;
        public bool FLAG_ITEM_DRAYNORMANOR_4;
        public bool FLAG_ITEM_DRAYNORMANOR_5;
        public bool FLAG_ITEM_DRAYNOR_1;
        public bool FLAG_ITEM_DRAYNORSEWERS_1;
        public bool FLAG_ITEM_ROUTE2_1;
        public bool FLAG_ITEM_ROUTE2_2;
        public bool FLAG_ITEM_WIZARDSTOWER_1;
        public bool FLAG_ITEM_WIZARDSTOWER_2;
        public bool FLAG_ITEM_WIZARDSTOWER_3;
        public bool FLAG_ITEM_WIZARDSTOWER_4;
        public bool FLAG_ITEM_WIZARDSTOWER_5;
        public bool FLAG_ITEM_WIZARDSTOWER_6;
        public bool FLAG_ITEM_WIZARDSTOWER_7;
        public bool FLAG_ITEM_ROUTE3_1;
        public bool FLAG_ITEM_ROUTE3_2;
        public bool FLAG_ITEM_ROUTE4_1;
        public bool FLAG_ITEM_ROUTE5_1;
        public bool FLAG_ITEM_PORTSARIM_1;
        public bool FLAG_ITEM_PORTSARIM_2;
        public bool FLAG_ITEM_PORTSARIM_3;
        public bool FLAG_ITEM_PORTSARIM_4;
        public bool FLAG_ITEM_PORTSARIM_5;
        public bool FLAG_ITEM_PORTSARIM_LIGHTHOUSE_1;
        public bool FLAG_ITEM_ROUTE10_1;
        public bool FLAG_ITEM_ROUTE11_1;
        public bool FLAG_ITEM_ROUTE11_2;
        public bool FLAG_ITEM_ROUTE11_3;
        public bool FLAG_ITEM_ROUTE11_4;
        public bool FLAG_ITEM_MUDSKIPPERPOINT_1;
        public bool FLAG_ITEM_ASGARNIADUNGEON_1;
        public bool FLAG_ITEM_ASGARNIADUNGEON_2;
        public bool FLAG_ITEM_ASGARNIADUNGEON_3;
        public bool FLAG_ITEM_ASGARNIADUNGEON_4;
        public bool FLAG_ITEM_ASGARNIADUNGEON_5;
        public bool FLAG_ITEM_ROUTE8_1;
        public bool FLAG_ITEM_ROUTE8_2;
        public bool FLAG_ITEM_ROUTE8_3;
        public bool FLAG_ITEM_MAKEOVER_MAGE_1;
        public bool FLAG_ITEM_ROUTE9_1;
        public bool FLAG_ITEM_ROUTE9_2;
        public bool FLAG_ITEM_RIMMINGTON_1;
        public bool FLAG_ITEM_RIMMINGTON_2;
        public bool FLAG_ITEM_RIMMINGTON_3;
        public bool FLAG_ITEM_RIMMINGTON_4;
        public bool FLAG_ITEM_RIMMINGTON_5;
        public bool FLAG_ITEM_RIMMINGTONMINES_1;
        public bool FLAG_ITEM_RIMMINGTONMINES_2;
        public bool FLAG_ITEM_MELZARSMAZE_1;
        public bool FLAG_ITEM_MELZARSMAZE_2;
        public bool FLAG_ITEM_MELZARSMAZE_3;
        public bool FLAG_ITEM_MELZARSMAZE_4;
        public bool FLAG_ITEM_ROUTE7_1;
        public bool FLAG_ITEM_AIRALTAR_1;
        public bool FLAG_ITEM_FALADOR_1;
        public bool FLAG_ITEM_FALADOR_2;
        public bool FLAG_ITEM_FALADOR_3;
        public bool FLAG_ITEM_FALADOR_4;
        public bool FLAG_ITEM_FALADOR_5;
        public bool FLAG_ITEM_DWARVENMINES_1;
        public bool FLAG_ITEM_DWARVENMINES_2;
        public bool FLAG_ITEM_DWARVENMINES_3;
        public bool FLAG_ITEM_DWARVENMINES_4;
        public bool FLAG_ITEM_DWARVENMINES_5;
        public bool FLAG_ITEM_POWERSTATION_1;
        public bool FLAG_ITEM_POWERSTATION_2;
        public bool FLAG_ITEM_PORT_SARIM_CHURCH;
        public bool FLAG_ITEM_ROUTE24_1;
        public bool FLAG_ITEM_ROUTE24_2;
        public bool FLAG_ITEM_ROUTE24_3;
        public bool FLAG_ITEM_ROUTE24_4;
        public bool FLAG_ITEM_GOBLINVILLAGE_1;
        public bool FLAG_ITEM_GOBLINVILLAGE_2;
        public bool FLAG_ITEM_GOBLINVILLAGE_3;
        public bool FLAG_ITEM_GOBLINVILLAGE_4;
        public bool FLAG_ITEM_ROUTE33_1;
        public bool FLAG_ITEM_ROUTE40_1;
        public bool FLAG_ITEM_BARBARIANVILLAGE_1;
        public bool FLAG_ITEM_BARBARIANVILLAGE_2;
        public bool FLAG_ITEM_BARBARIANVILLAGE_3;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_1;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_2;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_3;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_4;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_5;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_6;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_7;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_8;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_9;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_10;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_11;
        public bool FLAG_ITEM_ROUTE26_1;
        public bool FLAG_ITEM_ROUTE26_2;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_1;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_2;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_3;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_4;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_5;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_6;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_7;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_8;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_9;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_10;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_11;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_12;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_13;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_14;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_15;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_16;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_17;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_18;
        public bool FLAG_ITEM_BLACKKNIGHTSFORTRESS_19;
        public bool FLAG_ITEM_EDGEVILLE_1;
        public bool FLAG_ITEM_EDGEVILLE_2;
        public bool FLAG_ITEM_EDGEVILLE_3;
        public bool FLAG_ITEM_ROUTE20_1;
        public bool FLAG_ITEM_ROUTE21_1;
        public bool FLAG_ITEM_ROUTE18_1;
        public bool FLAG_ITEM_ROUTE18_2;
        public bool FLAG_ITEM_ROUTE19_1;
        public bool FLAG_ITEM_VARROCK_1;
        public bool FLAG_ITEM_VARROCK_2;
        public bool FLAG_ITEM_VARROCK_3;
        public bool FLAG_ITEM_VARROCK_4;
        public bool FLAG_ITEM_VARROCK_5;
        public bool FLAG_ITEM_VARROCKSEWERS_1;
        public bool FLAG_ITEM_VARROCKSEWERS_2;
        public bool FLAG_ITEM_VARROCKSEWERS_3;
        public bool FLAG_ITEM_VARROCKSEWERS_4;
        public bool FLAG_ITEM_VARROCKSEWERS_5;
        public bool FLAG_ITEM_VARROCKSEWERS_6;
        public bool FLAG_ITEM_VARROCKSEWERS_7;
        public bool FLAG_ITEM_VARROCKSEWERS_8;
        public bool FLAG_ITEM_VARROCKSEWERS_9;
        public bool FLAG_ITEM_ROUTE16_1;
        public bool FLAG_ITEM_ROUTE16_2;
        public bool FLAG_ITEM_ROUTE17_1;
        public bool FLAG_ITEM_ROUTE15_1;
        public bool FLAG_ITEM_ROUTE15_2;
        public bool FLAG_ITEM_ROUTE15_3;
        public bool FLAG_ITEM_ROUTE15_4;
        public bool FLAG_ITEM_ROUTE13_1;
        public bool FLAG_ITEM_ROUTE13_2;
        public bool FLAG_ITEM_ROUTE13_3;
        public bool FLAG_ITEM_ROUTE14_1;
        public bool FLAG_ITEM_ROUTE22_1;
        public bool FLAG_ITEM_ROUTE29_1;
        public bool FLAG_ITEM_ROUTE29_2;
        public bool FLAG_ITEM_KALPHITECAVE_1;
        public bool FLAG_ITEM_KALPHITECAVE_2;
        public bool FLAG_ITEM_KALPHITECAVE_3;
        public bool FLAG_ITEM_KALPHITECAVE_4;
        public bool FLAG_ITEM_KALPHITECAVE_5;
        public bool FLAG_ITEM_ROUTE28_1;
        public bool FLAG_ITEM_ROUTE28_2;
        public bool FLAG_ITEM_ALKHARID_1;
        public bool FLAG_ITEM_ALKHARID_2;
        public bool FLAG_ITEM_ALKHARID_3;
        public bool FLAG_ITEM_ALKHARID_4;
        public bool FLAG_ITEM_SHANTYPASS_1;
        public bool FLAG_ITEM_MUDSKIPPERSOUND_1;
        public bool FLAG_ITEM_MUDSKIPPERSOUND_2;
        public bool FLAG_ITEM_MUDSKIPPERSOUND_3;
        public bool FLAG_ITEM_MUDSKIPPERSOUND_4;
        public bool FLAG_ITEM_MUSAPOINT_1;
        public bool FLAG_ITEM_MUSAPOINT_2;
        public bool FLAG_ITEM_ROUTE39_1;
        public bool FLAG_ITEM_ROUTE39_2;
        public bool FLAG_ITEM_ROUTE39_3;
        public bool FLAG_ITEM_ROUTE39_4;
        public bool FLAG_ITEM_CRANDORSEAROUTE_1;
        public bool FLAG_ITEM_CRANDORSEAROUTE_2;
        public bool FLAG_ITEM_CRANDORSEAROUTE_3;
        public bool FLAG_ITEM_CRANDORSEAROUTE_4;
        public bool FLAG_ITEM_CRANDOR_1;
        public bool FLAG_ITEM_CRANDOR_2;
        public bool FLAG_ITEM_CRANDOR_3;
        public bool FLAG_ITEM_CRANDOR_4;
        public bool FLAG_ITEM_ROUTE9_3;
        public bool FLAG_ITEM_CRANDORVOLCANO_2;
        public bool FLAG_ITEM_CRANDORVOLCANO_3;
        public bool FLAG_ITEM_CRANDORVOLCANO_4;
        public bool FLAG_ITEM_TZHAAR_1;
        public bool FLAG_ITEM_TZHAAR_2;
        public bool FLAG_ITEM_TZHAAR_3;
        public bool FLAG_ITEM_SINKHOLEICE_1;
        public bool FLAG_ITEM_SINKHOLEICE_2;
        public bool FLAG_ITEM_SINKHOLEICE_3;
        public bool FLAG_ITEM_ROUTE30_1;
        public bool FLAG_ITEM_ROUTE30_2;
        public bool FLAG_ITEM_ROUTE31_1;
        public bool FLAG_ITEM_ROUTE31_2;
        public bool FLAG_ITEM_ROUTE31_3;
        public bool FLAG_ITEM_DAEMONHEIM_1;
        public bool FLAG_ITEM_DAEMONHEIM_2;
        public bool FLAG_ITEM_ROUTE41_1;
        public bool FLAG_ITEM_ROUTE41_2;
        public bool FLAG_ITEM_ROUTE41_3;
        public bool FLAG_ITEM_ROUTE42_1;
        public bool FLAG_ITEM_ROUTE43_1;
        public bool FLAG_ITEM_ROUTE43_2;
        public bool FLAG_ITEM_DIGSITE_1;
        public bool FLAG_ITEM_DIGSITE_2;
        public bool FLAG_ITEM_DIGSITE_3;
        public bool FLAG_ITEM_DIGSITE_4;
        public bool FLAG_ITEM_DIGSITE_5;
        public bool FLAG_ITEM_DIGSITE_6;
        public bool FLAG_ITEM_DIGSITE_7;
        public bool FLAG_ITEM_DIGSITE_8;
        public bool FLAG_ITEM_ROUTE38_1;
        public bool FLAG_ITEM_ROUTE38_2;
        public bool FLAG_ITEM_ROUTE38_3;
        public bool FLAG_ITEM_ROUTE45_1;
        public bool FLAG_ITEM_ROUTE45_2;
        public bool FLAG_ITEM_CANIFIS_1;
        public bool FLAG_ITEM_ROUTE180_1;
        public bool FLAG_ITEM_ROUTE180_2;
        public bool FLAG_ITEM_ROUTE180_3;
        public bool FLAG_ITEM_ROUTE35_1;
        public bool FLAG_ITEM_ROUTE35_2;
        public bool FLAG_ITEM_ROUTE35_3;
        public bool FLAG_ITEM_RELLEKKA_1;
        public bool FLAG_ITEM_RELLEKKA_2;
        public bool FLAG_ITEM_RELLEKKA_3;
        public bool FLAG_ITEM_RELLEKKA_4;
        public bool FLAG_ITEM_VARROCK_6;
        public bool FLAG_ITEM_PUROPURO_1;
        public bool FLAG_ITEM_PUROPURO_2;
        public bool FLAG_ITEM_PUROPURO_3;
        public bool FLAG_ITEM_ABYSSALREALM_1;
        public bool FLAG_ITEM_YANILLE_1;
        public bool FLAG_ITEM_ROUTE52_1;
        public bool FLAG_ITEM_PORTKHAZARD_1;
        public bool FLAG_ITEM_ROUTE54_1;
        public bool FLAG_ITEM_ROUTE50_1;
        public bool FLAG_ITEM_ROUTE50_2;
        public bool FLAG_ITEM_ROUTE50_3;
        public bool FLAG_ITEM_ROUTE61_1;
        public bool FLAG_ITEM_ROUTE61_2;
        public bool FLAG_ITEM_ROUTE60_1;
        public bool FLAG_ITEM_ROUTE60_2;
        public bool FLAG_ITEM_ROUTE60_3;
        public bool FLAG_ITEM_PORTKHAZARD_2;
        public bool FLAG_ITEM_HAMHIDEOUT_1;
        public bool FLAG_ITEM_HAMHIDEOUT_2;
        public bool FLAG_ITEM_HAMHIDEOUT_3;
        public bool FLAG_ITEM_HAMHIDEOUT_4;
        public bool FLAG_ITEM_HAMHIDEOUT_5;
        public bool FLAG_ITEM_HAMHIDEOUT_6;
        public bool FLAG_ITEM_HAMHIDEOUT_7;
        public bool FLAG_ITEM_ROUTE19_2;
        public bool FLAG_ITEM_EDGEVILLE_4;
        public bool FLAG_ITEM_BARBARIANSTRONGHOLD_12;
        public bool FLAG_ITEM_ROUTE11_5;
        public bool FLAG_ITEM_DRAYNORSEWERS_2;
        public bool FLAG_ITEM_ASGARNIADUNGEON_6;
        public bool FLAG_ITEM_SINKHOLE2_1;
        public bool FLAG_ITEM_SINKHOLE2_2;
        public bool FLAG_ITEM_HIDDEN_HERB_1;
        public bool FLAG_ITEM_HIDDEN_HERB_2;
        public bool FLAG_ITEM_HIDDEN_HERB_3;
        public bool FLAG_ITEM_KOUREND_CATACOMBS_1;
        public bool FLAG_ITEM_KOUREND_CATACOMBS_2;
        public bool FLAG_ITEM_KOUREND_CATACOMBS_3;
        public bool FLAG_ITEM_KOUREND_CATACOMBS_4;
        public bool FLAG_ITEM_KOUREND_CATACOMBS_5;
        public bool FLAG_ITEM_REPEATABLE_ITEM_1;
        public bool FLAG_ITEM_REPEATABLE_ITEM_2;
        public bool FLAG_ITEM_REPEATABLE_ITEM_3;
        public bool FLAG_ITEM_REPEATABLE_ITEM_4;
        public bool FLAG_ITEM_REPEATABLE_ITEM_5;
        public bool FLAG_ITEM_REPEATABLE_ITEM_6;
        public bool FLAG_ITEM_REPEATABLE_ITEM_7;
        public bool FLAG_ITEM_REPEATABLE_ITEM_8;
        public bool FLAG_ITEM_REPEATABLE_ITEM_9;
        public bool FLAG_ITEM_REPEATABLE_ITEM_10;
        public bool FLAG_CHEAT_ITEM_PULSE_CORE;
        public bool FLAG_ITEM_PRISMATIC_STAR;
        public bool FLAG_RECEIVED_PULSECORE;

    //HIDDEN ITEMS
        public bool FLAG_HIDDEN_ITEM_LUMBRIDGE_ITEM_1;
        public bool FLAG_HIDDEN_ITEM_LUMBRIDGE_ITEM_2;
        public bool FLAG_HIDDEN_ITEM_LUMBRIDGE_BEER;
        public bool FLAG_HIDDEN_ITEM_LUMBRIDGE_CABBAGE;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_ITEM_LAMP_HP;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_ITEM_MANY_SWEETS;
        public bool FLAG_HIDDEN_ITEM_ROUTE10_MINT_CAKE;
        public bool FLAG_HIDDEN_ITEM_ROUTE10_LAMP_SPDEF;
        public bool FLAG_HIDDEN_ITEM_PORTSARIM_WATER_RUNE;
        public bool FLAG_HIDDEN_ITEM_PORTSARIM_LAMP_SPEED;
        public bool FLAG_HIDDEN_ITEM_PORTSARIM_FLAX;
        public bool FLAG_HIDDEN_ITEM_AsgarnianDungeon_1;
        public bool FLAG_HIDDEN_ITEM_AsgarnianDungeon_2;
        public bool FLAG_HIDDEN_ITEM_AsgarnianDungeon_3;
        public bool FLAG_HIDDEN_ITEM_GoblinVillage_1;
        public bool FLAG_HIDDEN_ITEM_GoblinVillage_2;
        public bool FLAG_HIDDEN_FLAG_ITEM_RimmingtonMines_1;
        public bool FLAG_HIDDEN_FLAG_ITEM_RimmingtonMines_2;
        public bool FLAG_HIDDEN_ITEM_ROUTE9_1;
        public bool FLAG_HIDDEN_ITEM_ROUTE9_2;
        public bool FLAG_HIDDEN_ITEM_ROUTE9_3;
        public bool FLAG_HIDDEN_ITEM_ROUTE9_4;
        public bool FLAG_HIDDEN_ITEM_ROUTE29_1;
        public bool FLAG_HIDDEN_ITEM_ROUTE29_2;
        public bool FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_1_KEY;
        public bool FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_2_KEY;
        public bool FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_4_KEY;
        public bool FLAG_HIDDEN_ITEM_ABANDONED_SHIP_RM_6_KEY;
        public bool FLAG_HIDDEN_ITEM_ROUTE29_3;
        public bool FLAG_HIDDEN_ITEM_MUSA_POINT_1;
        public bool FLAG_HIDDEN_ITEM_MUSA_POINT_2;
        public bool FLAG_HIDDEN_ITEM_MUSA_POINT_3;
        public bool FLAG_HIDDEN_ITEM_MUSA_POINT_4;
        public bool FLAG_HIDDEN_ITEM_KARAMJA_VOLCANO_1;
        public bool FLAG_HIDDEN_ITEM_LUMBRIDGE_FOREST_1;
        public bool FLAG_HIDDEN_ITEM_TEMP_1;
        public bool FLAG_HIDDEN_ITEM_TEMP_2;
        public bool FLAG_HIDDEN_ITEM_TEMP_3;
        public bool FLAG_HIDDEN_ITEM_TEMP_4;
        public bool FLAG_HIDDEN_ITEM_TEMP_5;
        public bool FLAG_HIDDEN_ITEM_TEMP_6;
        public bool FLAG_HIDDEN_ITEM_TEMP_7;
        public bool FLAG_HIDDEN_ITEM_TEMP_8;
        public bool FLAG_HIDDEN_ITEM_TEMP_9;
        public bool FLAG_HIDDEN_ITEM_TEMP_10;
        public bool FLAG_HIDDEN_ITEM_TEMP_11;
        public bool FLAG_HIDDEN_ITEM_TEMP_12;
        public bool FLAG_HIDDEN_ITEM_TEMP_13;
        public bool FLAG_HIDDEN_ITEM_TEMP_14;
        public bool FLAG_HIDDEN_ITEM_TEMP_15;
        public bool FLAG_HIDDEN_ITEM_TEMP_16;
        public bool FLAG_HIDDEN_ITEM_TEMP_17;
        public bool FLAG_HIDDEN_ITEM_TEMP_18;
        public bool FLAG_HIDDEN_ITEM_TEMP_19;
        public bool FLAG_HIDDEN_ITEM_TEMP_20;
        public bool FLAG_HIDDEN_ITEM_VARROCK_1;
        public bool FLAG_HIDDEN_ITEM_VARROCK_2;
        public bool FLAG_HIDDEN_ITEM_VARROCK_3;
        public bool FLAG_HIDDEN_ITEM_VARROCK_4;
        public bool FLAG_HIDDEN_ITEM_VARROCK_5;
        public bool FLAG_HIDDEN_ITEM_VARROCK_6;
        public bool FLAG_HIDDEN_ITEM_DWARVEN_MINES_1;
        public bool FLAG_HIDDEN_ITEM_DWARVEN_MINES_2;
        public bool FLAG_HIDDEN_ITEM_DWARVEN_MINES_3;
        public bool FLAG_HIDDEN_ITEM_DWARVEN_MINES_4;
        public bool FLAG_HIDDEN_ITEM_MUDSKIPPER_POINT_1;
        public bool FLAG_HIDDEN_ITEM_MUDSKIPPER_POINT_2;
        public bool FLAG_HIDDEN_ITEM_MUDSKIPPER_POINT_3;
        public bool FLAG_HIDDEN_ITEM_VARROCK_SEWER_1;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_1;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_2;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_3;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_4;
        public bool FLAG_HIDDEN_ITEM_DRAYNORMANOR_5;
        public bool FLAG_HIDDEN_ITEM_MELZAR_1;
        public bool FLAG_HIDDEN_ITEM_MELZAR_2;
        public bool FLAG_HIDDEN_ITEM_MELZAR_3;
        public bool FLAG_HIDDEN_ITEM_MELZAR_4;
        public bool FLAG_HIDDEN_ITEM_MELZAR_5;
        public bool FLAG_HIDDEN_ITEM_MELZAR_6;
        public bool FLAG_HIDDEN_ITEM_MELZAR_7;
        public bool FLAG_HIDDEN_ITEM_ROUTE12_1;
        public bool FLAG_HIDDEN_ITEM_ROUTE39_1;
        public bool FLAG_HIDDEN_ITEM_ROUTE39_2;
        public bool FLAG_HIDDEN_ITEM_TRAINING_ROOM_1;
        public bool FLAG_HIDDEN_ITEM_TRAINING_ROOM_2;
        public bool FLAG_HIDDEN_ITEM_TRAINING_ROOM_3;
        public bool FLAG_HIDDEN_ITEM_TRAINING_ROOM_4;
        public bool FLAG_HIDDEN_ITEM_BKFORT_CABBAGE;
        public bool FLAG_HIDDEN_ITEM_AsgarnianDungeon_4;
        public bool FLAG_HIDDEN_ITEM_ROUTE_18_1;
        public bool FLAG_HIDDEN_ITEM_FALADOR_1;
        public bool FLAG_HIDDEN_ITEM_FALADOR_2;
        public bool FLAG_HIDDEN_ITEM_FALADOR_3;

    // Scrolls / TMs
        public bool FLAG_RECEIVED_TM06;
        public bool FLAG_UNUSED_0x3D7;
        public bool FLAG_RECEIVED_TM64;
        public bool FLAG_RECEIVED_TM22;
        public bool FLAG_UNUSED_0x3DA;
        public bool FLAG_RECEIVED_TM51;
        public bool FLAG_RECEIVED_TM26;
        public bool FLAG_RECEIVED_TM63;
        public bool FLAG_RECEIVED_TM47;
        public bool FLAG_RECEIVED_TM21;
        public bool FLAG_RECEIVED_TM49;
        public bool FLAG_RECEIVED_TM05;
        public bool FLAG_RECEIVED_TM50;
        public bool FLAG_RECEIVED_TM38;
        public bool FLAG_RECEIVED_TM54;
        public bool FLAG_RECEIVED_TM40;
        public bool FLAG_RECEIVED_TM33;
        public bool FLAG_RECEIVED_TM55;
        public bool FLAG_RECEIVED_TM37_DARKERMANZ;


















    }            























}
