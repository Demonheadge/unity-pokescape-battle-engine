
using System;
using UnityEngine;
using System.Collections.Generic;

public class TEST_Functions : MonoBehaviour
{
//    
//
//
//
//
//
//
//
//    
//void CreateScriptedWildMonCustom(u16 species, u8 level, u16 item, u16 abilityNum, u16 move1, u16 move2, u16 move3, u16 move4, u8 hpIV, u8 atkIV, u8 defIV, u8 speIV, u8 spaIV, u8 spdIV, u8 hpEV, u8 atkEV, u8 defEV, u8 speEV, u8 spaEV, u8 spdEV)
//    {
//        u8 heldItem[2];
//
//        ZeroEnemyPartyMons();
//        if (OW_SYNCHRONIZE_NATURE > GEN_3)
//            CreateMonWithNature(&gEnemyParty[0], species, level, USE_RANDOM_IVS, PickWildMonNature());
//        else
//            CreateMon(&gEnemyParty[0], species, level, USE_RANDOM_IVS, 0, 0, OT_ID_PLAYER_ID, 0);
//        if (item)
//        {
//            heldItem[0] = item;
//            heldItem[1] = item >> 8;
//            SetMonData(&gEnemyParty[0], MON_DATA_HELD_ITEM, heldItem);
//        }
//        SetMonData(&gEnemyParty[0], MON_DATA_ABILITY_NUM, &abilityNum);
//        SetMonMoveSlot(&gEnemyParty[0], move1, 0);
//        SetMonMoveSlot(&gEnemyParty[0], move2, 1);
//        SetMonMoveSlot(&gEnemyParty[0], move3, 2);
//        SetMonMoveSlot(&gEnemyParty[0], move4, 3);
//        SetMonData(&gEnemyParty[0], MON_DATA_HP_IV, &hpIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_ATK_IV, &atkIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_DEF_IV, &defIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPEED_IV, &speIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPATK_IV, &spaIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPDEF_IV, &spdIV);
//        SetMonData(&gEnemyParty[0], MON_DATA_HP_EV, &hpEV);
//        SetMonData(&gEnemyParty[0], MON_DATA_ATK_EV, &atkEV);
//        SetMonData(&gEnemyParty[0], MON_DATA_DEF_EV, &defEV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPEED_EV, &speEV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPATK_EV, &spaEV);
//        SetMonData(&gEnemyParty[0], MON_DATA_SPDEF_EV, &spdEV);
//    }
//
//void SetMonMoveSlot(struct Pokemon *mon, u16 move, u8 slot)
//{
//    SetMonData(mon, MON_DATA_MOVE1 + slot, &move);
//    SetMonData(mon, MON_DATA_PP1 + slot, &gBattleMoves[move].pp);
//}
//
//void ScriptSetMonMoveSlot(u8 monIndex, u16 move, u8 slot)
//{
//// Allows monIndex to go out of bounds of gPlayerParty. Doesn't occur in vanilla
//#ifdef BUGFIX
//    if (monIndex >= PARTY_SIZE)
//#else
//    if (monIndex > PARTY_SIZE)
//#endif
//        monIndex = gPlayerPartyCount - 1;
//
//    SetMonMoveSlot(&gPlayerParty[monIndex], move, slot);
//}
//
//    gPlayerPartyCount
//
//
//
//
//
//
//
//#define CALC_STAT(base, iv, ev, statIndex, field)               \
//{                                                               \
//    u8 baseStat = gSpeciesInfo[species].base;                   \
//    s32 n = (((2 * baseStat + iv + ev / 4) * level) / 100) + 5; \
//    u8 nature = GetNature(mon);                                 \
//    n = ModifyStatByNature(nature, n, statIndex);               \
//    SetMonData(mon, field, &n);                                 \
//}
//void CalculateMonStats(struct Pokemon *mon)
//{
//    s32 oldMaxHP = GetMonData(mon, MON_DATA_MAX_HP, NULL);
//    s32 currentHP = GetMonData(mon, MON_DATA_HP, NULL);
//    s32 hpIV = GetMonData(mon, MON_DATA_HP_IV, NULL);
//    s32 hpEV = GetMonData(mon, MON_DATA_HP_EV, NULL);
//    s32 attackIV = GetMonData(mon, MON_DATA_ATK_IV, NULL);
//    s32 attackEV = GetMonData(mon, MON_DATA_ATK_EV, NULL);
//    s32 defenseIV = GetMonData(mon, MON_DATA_DEF_IV, NULL);
//    s32 defenseEV = GetMonData(mon, MON_DATA_DEF_EV, NULL);
//    s32 speedIV = GetMonData(mon, MON_DATA_SPEED_IV, NULL);
//    s32 speedEV = GetMonData(mon, MON_DATA_SPEED_EV, NULL);
//    s32 spAttackIV = GetMonData(mon, MON_DATA_SPATK_IV, NULL);
//    s32 spAttackEV = GetMonData(mon, MON_DATA_SPATK_EV, NULL);
//    s32 spDefenseIV = GetMonData(mon, MON_DATA_SPDEF_IV, NULL);
//    s32 spDefenseEV = GetMonData(mon, MON_DATA_SPDEF_EV, NULL);
//    u16 species = GetMonData(mon, MON_DATA_SPECIES, NULL);
//    s32 level = GetLevelFromMonExp(mon);
//    s32 newMaxHP;
//
//    SetMonData(mon, MON_DATA_LEVEL, &level);
//
//    if (species == SPECIES_SHEDINJA)
//    {
//        newMaxHP = 1;
//    }
//    else
//    {
//        s32 n = 2 * gSpeciesInfo[species].baseHP + hpIV;
//        newMaxHP = (((n + hpEV / 4) * level) / 100) + level + 10;
//    }
//
//    gBattleScripting.levelUpHP = newMaxHP - oldMaxHP;
//    if (gBattleScripting.levelUpHP == 0)
//        gBattleScripting.levelUpHP = 1;
//
//    SetMonData(mon, MON_DATA_MAX_HP, &newMaxHP);
//
//    CALC_STAT(baseAttack, attackIV, attackEV, STAT_ATK, MON_DATA_ATK)
//    CALC_STAT(baseDefense, defenseIV, defenseEV, STAT_DEF, MON_DATA_DEF)
//    CALC_STAT(baseSpeed, speedIV, speedEV, STAT_SPEED, MON_DATA_SPEED)
//    CALC_STAT(baseSpAttack, spAttackIV, spAttackEV, STAT_SPATK, MON_DATA_SPATK)
//    CALC_STAT(baseSpDefense, spDefenseIV, spDefenseEV, STAT_SPDEF, MON_DATA_SPDEF)
//
//    if (species == SPECIES_SHEDINJA)
//    {
//        if (currentHP != 0 || oldMaxHP == 0)
//            currentHP = 1;
//        else
//            return;
//    }
//    else
//    {
//        if (currentHP == 0 && oldMaxHP == 0)
//            currentHP = newMaxHP;
//        else if (currentHP != 0) {
//            // BUG: currentHP is unintentionally able to become <= 0 after the instruction below. This causes the pomeg berry glitch.
//            currentHP += newMaxHP - oldMaxHP;
//            #ifdef BUGFIX
//            if (currentHP <= 0)
//                currentHP = 1;
//            #endif
//        }
//        else
//            return;
//    }
//
//    SetMonData(mon, MON_DATA_HP, &currentHP);
//}
//
//
//
//
//void SetMonData(struct Pokemon *mon, s32 field, const void *dataArg)
//{
//    const u8 *data = dataArg;
//
//    switch (field)
//    {
//    case MON_DATA_STATUS:
//        SET32(mon->status);
//        break;
//    case MON_DATA_LEVEL:
//        SET8(mon->level);
//        break;
//    case MON_DATA_HP:
//        SET16(mon->hp);
//        break;
//    case MON_DATA_MAX_HP:
//        SET16(mon->maxHP);
//        break;
//    case MON_DATA_ATK:
//        SET16(mon->attack);
//        break;
//    case MON_DATA_DEF:
//        SET16(mon->defense);
//        break;
//    case MON_DATA_SPEED:
//        SET16(mon->speed);
//        break;
//    case MON_DATA_SPATK:
//        SET16(mon->spAttack);
//        break;
//    case MON_DATA_SPDEF:
//        SET16(mon->spDefense);
//        break;
//    case MON_DATA_MAIL:
//        SET8(mon->mail);
//        break;
//    case MON_DATA_SPECIES_OR_EGG:
//        break;
//    default:
//        SetBoxMonData(&mon->box, field, data);
//        break;
//    }
//}
//
//
//void SetBoxMonData(struct BoxPokemon *boxMon, s32 field, const void *dataArg)
//{
//    const u8 *data = dataArg;
//
//    struct PokemonSubstruct0 *substruct0 = NULL;
//    struct PokemonSubstruct1 *substruct1 = NULL;
//    struct PokemonSubstruct2 *substruct2 = NULL;
//    struct PokemonSubstruct3 *substruct3 = NULL;
//
//    if (field > MON_DATA_ENCRYPT_SEPARATOR)
//    {
//        substruct0 = &(GetSubstruct(boxMon, boxMon->personality, 0)->type0);
//        substruct1 = &(GetSubstruct(boxMon, boxMon->personality, 1)->type1);
//        substruct2 = &(GetSubstruct(boxMon, boxMon->personality, 2)->type2);
//        substruct3 = &(GetSubstruct(boxMon, boxMon->personality, 3)->type3);
//
//        DecryptBoxMon(boxMon);
//
//        if (CalculateBoxMonChecksum(boxMon) != boxMon->checksum)
//        {
//            boxMon->isBadEgg = TRUE;
//            boxMon->isEgg = TRUE;
//            substruct3->isEgg = TRUE;
//            EncryptBoxMon(boxMon);
//            return;
//        }
//
//        switch (field)
//        {
//        case MON_DATA_SPECIES:
//        {
//            SET16(substruct0->species);
//            if (substruct0->species)
//                boxMon->hasSpecies = TRUE;
//            else
//                boxMon->hasSpecies = FALSE;
//            break;
//        }
//        case MON_DATA_HELD_ITEM:
//            SET16(substruct0->heldItem);
//            break;
//        case MON_DATA_EXP:
//            SET32(substruct0->experience);
//            break;
//        case MON_DATA_PP_BONUSES:
//            SET8(substruct0->ppBonuses);
//            break;
//        case MON_DATA_FRIENDSHIP:
//            SET8(substruct0->friendship);
//            break;
//        case MON_DATA_MOVE1:
//        case MON_DATA_MOVE2:
//        case MON_DATA_MOVE3:
//        case MON_DATA_MOVE4:
//            SET16(substruct1->moves[field - MON_DATA_MOVE1]);
//            break;
//        case MON_DATA_PP1:
//        case MON_DATA_PP2:
//        case MON_DATA_PP3:
//        case MON_DATA_PP4:
//            SET8(substruct1->pp[field - MON_DATA_PP1]);
//            break;
//        case MON_DATA_HP_EV:
//            SET8(substruct2->hpEV);
//            break;
//        case MON_DATA_ATK_EV:
//            SET8(substruct2->attackEV);
//            break;
//        case MON_DATA_DEF_EV:
//            SET8(substruct2->defenseEV);
//            break;
//        case MON_DATA_SPEED_EV:
//            SET8(substruct2->speedEV);
//            break;
//        case MON_DATA_SPATK_EV:
//            SET8(substruct2->spAttackEV);
//            break;
//        case MON_DATA_SPDEF_EV:
//            SET8(substruct2->spDefenseEV);
//            break;
//        case MON_DATA_COOL:
//            SET8(substruct2->cool);
//            break;
//        case MON_DATA_BEAUTY:
//            SET8(substruct2->beauty);
//            break;
//        case MON_DATA_CUTE:
//            SET8(substruct2->cute);
//            break;
//        case MON_DATA_SMART:
//            SET8(substruct2->smart);
//            break;
//        case MON_DATA_TOUGH:
//            SET8(substruct2->tough);
//            break;
//        case MON_DATA_SHEEN:
//            SET8(substruct2->sheen);
//            break;
//        case MON_DATA_POKERUS:
//            SET8(substruct3->pokerus);
//            break;
//        case MON_DATA_MET_LOCATION:
//            SET8(substruct3->metLocation);
//            break;
//        case MON_DATA_MET_LEVEL:
//        {
//            u8 metLevel = *data;
//            substruct3->metLevel = metLevel;
//            break;
//        }
//        case MON_DATA_MET_GAME:
//            SET8(substruct3->metGame);
//            break;
//        case MON_DATA_POKEBALL:
//        {
//            u8 pokeball = *data;
//            substruct0->pokeball = pokeball;
//            break;
//        }
//        case MON_DATA_OT_GENDER:
//            SET8(substruct3->otGender);
//            break;
//        case MON_DATA_HP_IV:
//            SET8(substruct3->hpIV);
//            break;
//        case MON_DATA_ATK_IV:
//            SET8(substruct3->attackIV);
//            break;
//        case MON_DATA_DEF_IV:
//            SET8(substruct3->defenseIV);
//            break;
//        case MON_DATA_SPEED_IV:
//            SET8(substruct3->speedIV);
//            break;
//        case MON_DATA_SPATK_IV:
//            SET8(substruct3->spAttackIV);
//            break;
//        case MON_DATA_SPDEF_IV:
//            SET8(substruct3->spDefenseIV);
//            break;
//        case MON_DATA_IS_EGG:
//            SET8(substruct3->isEgg);
//            if (substruct3->isEgg)
//                boxMon->isEgg = TRUE;
//            else
//                boxMon->isEgg = FALSE;
//            break;
//        case MON_DATA_ABILITY_NUM:
//            SET8(substruct3->abilityNum);
//            break;
//        case MON_DATA_COOL_RIBBON:
//            SET8(substruct3->coolRibbon);
//            break;
//        case MON_DATA_BEAUTY_RIBBON:
//            SET8(substruct3->beautyRibbon);
//            break;
//        case MON_DATA_CUTE_RIBBON:
//            SET8(substruct3->cuteRibbon);
//            break;
//        case MON_DATA_SMART_RIBBON:
//            SET8(substruct3->smartRibbon);
//            break;
//        case MON_DATA_TOUGH_RIBBON:
//            SET8(substruct3->toughRibbon);
//            break;
//        case MON_DATA_CHAMPION_RIBBON:
//            SET8(substruct3->championRibbon);
//            break;
//        case MON_DATA_WINNING_RIBBON:
//            SET8(substruct3->winningRibbon);
//            break;
//        case MON_DATA_VICTORY_RIBBON:
//            SET8(substruct3->victoryRibbon);
//            break;
//        case MON_DATA_ARTIST_RIBBON:
//            SET8(substruct3->artistRibbon);
//            break;
//        case MON_DATA_EFFORT_RIBBON:
//            SET8(substruct3->effortRibbon);
//            break;
//        case MON_DATA_MARINE_RIBBON:
//            SET8(substruct3->marineRibbon);
//            break;
//        case MON_DATA_LAND_RIBBON:
//            SET8(substruct3->landRibbon);
//            break;
//        case MON_DATA_SKY_RIBBON:
//            SET8(substruct3->skyRibbon);
//            break;
//        case MON_DATA_COUNTRY_RIBBON:
//            SET8(substruct3->countryRibbon);
//            break;
//        case MON_DATA_NATIONAL_RIBBON:
//            SET8(substruct3->nationalRibbon);
//            break;
//        case MON_DATA_EARTH_RIBBON:
//            SET8(substruct3->earthRibbon);
//            break;
//        case MON_DATA_WORLD_RIBBON:
//            SET8(substruct3->worldRibbon);
//            break;
//        case MON_DATA_UNUSED_RIBBONS:
//            SET8(substruct3->unusedRibbons);
//            break;
//        case MON_DATA_MODERN_FATEFUL_ENCOUNTER:
//            SET8(substruct3->modernFatefulEncounter);
//            break;
//        case MON_DATA_IVS:
//        {
//            u32 ivs = data[0] | (data[1] << 8) | (data[2] << 16) | (data[3] << 24);
//            substruct3->hpIV = ivs & MAX_IV_MASK;
//            substruct3->attackIV = (ivs >> 5) & MAX_IV_MASK;
//            substruct3->defenseIV = (ivs >> 10) & MAX_IV_MASK;
//            substruct3->speedIV = (ivs >> 15) & MAX_IV_MASK;
//            substruct3->spAttackIV = (ivs >> 20) & MAX_IV_MASK;
//            substruct3->spDefenseIV = (ivs >> 25) & MAX_IV_MASK;
//            break;
//        }
//        default:
//            break;
//        }
//    }
//    else
//    {
//        switch (field)
//        {
//        case MON_DATA_PERSONALITY:
//            SET32(boxMon->personality);
//            break;
//        case MON_DATA_OT_ID:
//            SET32(boxMon->otId);
//            break;
//        case MON_DATA_NICKNAME:
//        {
//            s32 i;
//            for (i = 0; i < POKEMON_NAME_LENGTH; i++)
//                boxMon->nickname[i] = data[i];
//            break;
//        }
//        case MON_DATA_LANGUAGE:
//            SET8(boxMon->language);
//            break;
//        case MON_DATA_SANITY_IS_BAD_EGG:
//            SET8(boxMon->isBadEgg);
//            break;
//        case MON_DATA_SANITY_HAS_SPECIES:
//            SET8(boxMon->hasSpecies);
//            break;
//        case MON_DATA_SANITY_IS_EGG:
//            SET8(boxMon->isEgg);
//            break;
//        case MON_DATA_OT_NAME:
//        {
//            s32 i;
//            for (i = 0; i < PLAYER_NAME_LENGTH; i++)
//                boxMon->otName[i] = data[i];
//            break;
//        }
//        case MON_DATA_MARKINGS:
//            SET8(boxMon->markings);
//            break;
//        case MON_DATA_CHECKSUM:
//            SET16(boxMon->checksum);
//            break;
//        case MON_DATA_ENCRYPT_SEPARATOR:
//            SET16(boxMon->unknown);
//            break;
//        }
//    }
//
//    if (field > MON_DATA_ENCRYPT_SEPARATOR)
//    {
//        boxMon->checksum = CalculateBoxMonChecksum(boxMon);
//        EncryptBoxMon(boxMon);
//    }
//}
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//void CreateBoxMon(struct BoxPokemon *boxMon, u16 species, u8 level, u8 fixedIV, u8 hasFixedPersonality, u32 fixedPersonality, u8 otIdType, u32 fixedOtId)
//{
//    u8 speciesName[POKEMON_NAME_LENGTH + 1];
//    u32 personality;
//    u32 value;
//    u16 checksum;
//    u8 i;
//    u8 availableIVs[NUM_STATS];
//    u8 selectedIvs[LEGENDARY_PERFECT_IV_COUNT];
//
//    ZeroBoxMonData(boxMon);
//
//    if (hasFixedPersonality)
//        personality = fixedPersonality;
//    else
//        personality = Random32();
//
//    // Determine original trainer ID
//    if (otIdType == OT_ID_RANDOM_NO_SHINY)
//    {
//        u32 shinyValue;
//        do
//        {
//            // Choose random OT IDs until one that results in a non-shiny Pokémon
//            value = Random32();
//            shinyValue = GET_SHINY_VALUE(value, personality);
//        } while (shinyValue < SHINY_ODDS);
//    }
//    else if (otIdType == OT_ID_PRESET)
//    {
//        value = fixedOtId;
//    }
//    else // Player is the OT
//    {
//        value = gSaveBlock2Ptr->playerTrainerId[0]
//              | (gSaveBlock2Ptr->playerTrainerId[1] << 8)
//              | (gSaveBlock2Ptr->playerTrainerId[2] << 16)
//              | (gSaveBlock2Ptr->playerTrainerId[3] << 24);
//
//#if P_FLAG_FORCE_NO_SHINY != 0
//        if (FlagGet(P_FLAG_FORCE_NO_SHINY))
//        {
//            while (GET_SHINY_VALUE(value, personality) < SHINY_ODDS)
//                personality = Random32();
//        }
//#endif
//#if P_FLAG_FORCE_SHINY != 0
//    #if P_FLAG_FORCE_NO_SHINY != 0
//        else
//    #endif
//        if (FlagGet(P_FLAG_FORCE_SHINY))
//        {
//            while (GET_SHINY_VALUE(value, personality) >= SHINY_ODDS)
//                personality = Random32();
//        }
//#endif
//#if P_FLAG_FORCE_SHINY != 0 || P_FLAG_FORCE_NO_SHINY != 0
//        else
//#endif
//        {
//            u32 totalRerolls = 0;
//            if (CheckBagHasItem(ITEM_SHINY_CHARM, 1))
//                totalRerolls += I_SHINY_CHARM_ADDITIONAL_ROLLS;
//            if (LURE_STEP_COUNT != 0)
//                totalRerolls += 1;
//            if (gIsFishingEncounter)
//                totalRerolls += 1 + 2 * gChainFishingStreak;
//
//            while (GET_SHINY_VALUE(value, personality) >= SHINY_ODDS && totalRerolls > 0)
//            {
//                personality = Random32();
//                totalRerolls--;
//            }
//        }
//    }
//
//    SetBoxMonData(boxMon, MON_DATA_PERSONALITY, &personality);
//    SetBoxMonData(boxMon, MON_DATA_OT_ID, &value);
//
//    checksum = CalculateBoxMonChecksum(boxMon);
//    SetBoxMonData(boxMon, MON_DATA_CHECKSUM, &checksum);
//    EncryptBoxMon(boxMon);
//    StringCopy(speciesName, GetSpeciesName(species));
//    SetBoxMonData(boxMon, MON_DATA_NICKNAME, speciesName);
//    SetBoxMonData(boxMon, MON_DATA_LANGUAGE, &gGameLanguage);
//    SetBoxMonData(boxMon, MON_DATA_OT_NAME, gSaveBlock2Ptr->playerName);
//    SetBoxMonData(boxMon, MON_DATA_SPECIES, &species);
//    SetBoxMonData(boxMon, MON_DATA_EXP, &gExperienceTables[gSpeciesInfo[species].growthRate][level]);
//    SetBoxMonData(boxMon, MON_DATA_FRIENDSHIP, &gSpeciesInfo[species].friendship);
//    value = GetCurrentRegionMapSectionId();
//    SetBoxMonData(boxMon, MON_DATA_MET_LOCATION, &value);
//    SetBoxMonData(boxMon, MON_DATA_MET_LEVEL, &level);
//    SetBoxMonData(boxMon, MON_DATA_MET_GAME, &gGameVersion);
//    value = ITEM_POUCH;
//    SetBoxMonData(boxMon, MON_DATA_POKEBALL, &value);
//    SetBoxMonData(boxMon, MON_DATA_OT_GENDER, &gSaveBlock2Ptr->playerGender);
//
//    if (fixedIV < USE_RANDOM_IVS)
//    {
//        SetBoxMonData(boxMon, MON_DATA_HP_IV, &fixedIV);
//        SetBoxMonData(boxMon, MON_DATA_ATK_IV, &fixedIV);
//        SetBoxMonData(boxMon, MON_DATA_DEF_IV, &fixedIV);
//        SetBoxMonData(boxMon, MON_DATA_SPEED_IV, &fixedIV);
//        SetBoxMonData(boxMon, MON_DATA_SPATK_IV, &fixedIV);
//        SetBoxMonData(boxMon, MON_DATA_SPDEF_IV, &fixedIV);
//    }
//    else
//    {
//        u32 iv;
//        value = Random();
//
//        iv = value & MAX_IV_MASK;
//        SetBoxMonData(boxMon, MON_DATA_HP_IV, &iv);
//        iv = (value & (MAX_IV_MASK << 5)) >> 5;
//        SetBoxMonData(boxMon, MON_DATA_ATK_IV, &iv);
//        iv = (value & (MAX_IV_MASK << 10)) >> 10;
//        SetBoxMonData(boxMon, MON_DATA_DEF_IV, &iv);
//
//        value = Random();
//
//        iv = value & MAX_IV_MASK;
//        SetBoxMonData(boxMon, MON_DATA_SPEED_IV, &iv);
//        iv = (value & (MAX_IV_MASK << 5)) >> 5;
//        SetBoxMonData(boxMon, MON_DATA_SPATK_IV, &iv);
//        iv = (value & (MAX_IV_MASK << 10)) >> 10;
//        SetBoxMonData(boxMon, MON_DATA_SPDEF_IV, &iv);
//
//        if (gSpeciesInfo[species].allPerfectIVs)
//        {
//            iv = MAX_PER_STAT_IVS;
//            SetBoxMonData(boxMon, MON_DATA_HP_IV, &iv);
//            SetBoxMonData(boxMon, MON_DATA_ATK_IV, &iv);
//            SetBoxMonData(boxMon, MON_DATA_DEF_IV, &iv);
//            SetBoxMonData(boxMon, MON_DATA_SPEED_IV, &iv);
//            SetBoxMonData(boxMon, MON_DATA_SPATK_IV, &iv);
//            SetBoxMonData(boxMon, MON_DATA_SPDEF_IV, &iv);
//        }
//        else if (P_LEGENDARY_PERFECT_IVS >= GEN_6
//         && (gSpeciesInfo[species].isLegendary
//          || gSpeciesInfo[species].isMythical
//          || gSpeciesInfo[species].isUltraBeast))
//        {
//            iv = MAX_PER_STAT_IVS;
//            // Initialize a list of IV indices.
//            for (i = 0; i < NUM_STATS; i++)
//            {
//                availableIVs[i] = i;
//            }
//
//            // Select the 3 IVs that will be perfected.
//            for (i = 0; i < LEGENDARY_PERFECT_IV_COUNT; i++)
//            {
//                u8 index = Random() % (NUM_STATS - i);
//                selectedIvs[i] = availableIVs[index];
//                RemoveIVIndexFromList(availableIVs, index);
//            }
//            for (i = 0; i < LEGENDARY_PERFECT_IV_COUNT; i++)
//            {
//                switch (selectedIvs[i])
//                {
//                case STAT_HP:
//                    SetBoxMonData(boxMon, MON_DATA_HP_IV, &iv);
//                    break;
//                case STAT_ATK:
//                    SetBoxMonData(boxMon, MON_DATA_ATK_IV, &iv);
//                    break;
//                case STAT_DEF:
//                    SetBoxMonData(boxMon, MON_DATA_DEF_IV, &iv);
//                    break;
//                case STAT_SPEED:
//                    SetBoxMonData(boxMon, MON_DATA_SPEED_IV, &iv);
//                    break;
//                case STAT_SPATK:
//                    SetBoxMonData(boxMon, MON_DATA_SPATK_IV, &iv);
//                    break;
//                case STAT_SPDEF:
//                    SetBoxMonData(boxMon, MON_DATA_SPDEF_IV, &iv);
//                    break;
//                }
//            }
//        }
//    }
//
//    if (gSpeciesInfo[species].abilities[1])
//    {
//        value = personality & 1;
//        SetBoxMonData(boxMon, MON_DATA_ABILITY_NUM, &value);
//    }
//
//    GiveBoxMonInitialMoveset(boxMon);
//}
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//#define GET_BASE_SPECIES_ID(speciesId) (GetFormSpeciesId(speciesId, 0))
//#define FORM_SPECIES_END (0xffff)
//
//// Property labels for Get(Box)MonData / Set(Box)MonData
//enum {
//    MON_DATA_PERSONALITY,
//    MON_DATA_OT_ID,
//    MON_DATA_NICKNAME,
//    MON_DATA_LANGUAGE,
//    MON_DATA_SANITY_IS_BAD_EGG,
//    MON_DATA_SANITY_HAS_SPECIES,
//    MON_DATA_SANITY_IS_EGG,
//    MON_DATA_OT_NAME,
//    MON_DATA_MARKINGS,
//    MON_DATA_CHECKSUM,
//    MON_DATA_ENCRYPT_SEPARATOR,
//    MON_DATA_SPECIES,
//    MON_DATA_HELD_ITEM,
//    MON_DATA_MOVE1,
//    MON_DATA_MOVE2,
//    MON_DATA_MOVE3,
//    MON_DATA_MOVE4,
//    MON_DATA_PP1,
//    MON_DATA_PP2,
//    MON_DATA_PP3,
//    MON_DATA_PP4,
//    MON_DATA_PP_BONUSES,
//    MON_DATA_COOL,
//    MON_DATA_BEAUTY,
//    MON_DATA_CUTE,
//    MON_DATA_EXP,
//    MON_DATA_HP_EV,
//    MON_DATA_ATK_EV,
//    MON_DATA_DEF_EV,
//    MON_DATA_SPEED_EV,
//    MON_DATA_SPATK_EV,
//    MON_DATA_SPDEF_EV,
//    MON_DATA_FRIENDSHIP,
//    MON_DATA_SMART,
//    MON_DATA_POKERUS,
//    MON_DATA_MET_LOCATION,
//    MON_DATA_MET_LEVEL,
//    MON_DATA_MET_GAME,
//    MON_DATA_POKEBALL,
//    MON_DATA_HP_IV,
//    MON_DATA_ATK_IV,
//    MON_DATA_DEF_IV,
//    MON_DATA_SPEED_IV,
//    MON_DATA_SPATK_IV,
//    MON_DATA_SPDEF_IV,
//    MON_DATA_IS_EGG,
//    MON_DATA_ABILITY_NUM,
//    MON_DATA_TOUGH,
//    MON_DATA_SHEEN,
//    MON_DATA_OT_GENDER,
//    MON_DATA_COOL_RIBBON,
//    MON_DATA_BEAUTY_RIBBON,
//    MON_DATA_CUTE_RIBBON,
//    MON_DATA_SMART_RIBBON,
//    MON_DATA_TOUGH_RIBBON,
//    MON_DATA_STATUS,
//    MON_DATA_LEVEL,
//    MON_DATA_HP,
//    MON_DATA_MAX_HP,
//    MON_DATA_ATK,
//    MON_DATA_DEF,
//    MON_DATA_SPEED,
//    MON_DATA_SPATK,
//    MON_DATA_SPDEF,
//    MON_DATA_MAIL,
//    MON_DATA_SPECIES_OR_EGG,
//    MON_DATA_IVS,
//    MON_DATA_CHAMPION_RIBBON,
//    MON_DATA_WINNING_RIBBON,
//    MON_DATA_VICTORY_RIBBON,
//    MON_DATA_ARTIST_RIBBON,
//    MON_DATA_EFFORT_RIBBON,
//    MON_DATA_MARINE_RIBBON,
//    MON_DATA_LAND_RIBBON,
//    MON_DATA_SKY_RIBBON,
//    MON_DATA_COUNTRY_RIBBON,
//    MON_DATA_NATIONAL_RIBBON,
//    MON_DATA_EARTH_RIBBON,
//    MON_DATA_WORLD_RIBBON,
//    MON_DATA_UNUSED_RIBBONS,
//    MON_DATA_MODERN_FATEFUL_ENCOUNTER,
//    MON_DATA_KNOWN_MOVES,
//    MON_DATA_RIBBON_COUNT,
//    MON_DATA_RIBBONS,
//    MON_DATA_ATK2,
//    MON_DATA_DEF2,
//    MON_DATA_SPEED2,
//    MON_DATA_SPATK2,
//    MON_DATA_SPDEF2,
//};
//
//struct PokemonSubstruct0
//{
//    /*0x00*/ u16 species;
//    /*0x02*/ u16 heldItem;
//    /*0x04*/ u32 experience;
//    /*0x08*/ u8 ppBonuses;
//    /*0x09*/ u8 friendship;
//    /*0x0A*/ u16 pokeball:5; //31 balls
//             u16 filler:11;
//}; /* size = 12 */
//
//struct PokemonSubstruct1
//{
//    /*0x00*/ u16 moves[MAX_MON_MOVES];
//    /*0x08*/ u8 pp[MAX_MON_MOVES];
//}; /* size = 12 */
//
//struct PokemonSubstruct2
//{
//    /*0x00*/ u8 hpEV;
//    /*0x01*/ u8 attackEV;
//    /*0x02*/ u8 defenseEV;
//    /*0x03*/ u8 speedEV;
//    /*0x04*/ u8 spAttackEV;
//    /*0x05*/ u8 spDefenseEV;
//    /*0x06*/ u8 cool;
//    /*0x07*/ u8 beauty;
//    /*0x08*/ u8 cute;
//    /*0x09*/ u8 smart;
//    /*0x0A*/ u8 tough;
//    /*0x0B*/ u8 sheen;
//}; /* size = 12 */
//
//struct PokemonSubstruct3
//{
// /* 0x00 */ u8 pokerus;
// /* 0x01 */ u8 metLocation;
//
// /* 0x02 */ u16 metLevel:7;
// /* 0x02 */ u16 metGame:4;
// /* 0x03 */ u16 unused1:4;
// /* 0x03 */ u16 otGender:1;
//
// /* 0x04 */ u32 hpIV:5;
// /* 0x04 */ u32 attackIV:5;
// /* 0x05 */ u32 defenseIV:5;
// /* 0x05 */ u32 speedIV:5;
// /* 0x05 */ u32 spAttackIV:5;
// /* 0x06 */ u32 spDefenseIV:5;
// /* 0x07 */ u32 isEgg:1;
// /* 0x07 */ u32 unused2:1;
//
// /* 0x08 */ u32 coolRibbon:3;               // Stores the highest contest rank achieved in the Cool category.
// /* 0x08 */ u32 beautyRibbon:3;             // Stores the highest contest rank achieved in the Beauty category.
// /* 0x08 */ u32 cuteRibbon:3;               // Stores the highest contest rank achieved in the Cute category.
// /* 0x09 */ u32 smartRibbon:3;              // Stores the highest contest rank achieved in the Smart category.
// /* 0x09 */ u32 toughRibbon:3;              // Stores the highest contest rank achieved in the Tough category.
// /* 0x09 */ u32 championRibbon:1;           // Given when defeating the Champion. Because both RSE and FRLG use it, later generations don't specify from which region it comes from.
// /* 0x0A */ u32 winningRibbon:1;            // Given at the Battle Tower's Level 50 challenge by winning a set of seven battles that extends the current streak to 56 or more.
// /* 0x0A */ u32 victoryRibbon:1;            // Given at the Battle Tower's Level 100 challenge by winning a set of seven battles that extends the current streak to 56 or more.
// /* 0x0A */ u32 artistRibbon:1;             // Given at the Contest Hall by winning a Master Rank contest with at least 800 points, and agreeing to have the Pokémon's portrait placed in the museum after being offered.
// /* 0x0A */ u32 effortRibbon:1;             // Given at Slateport's market to Pokémon with maximum EVs.
// /* 0x0A */ u32 marineRibbon:1;             // Never distributed.
// /* 0x0A */ u32 landRibbon:1;               // Never distributed.
// /* 0x0A */ u32 skyRibbon:1;                // Never distributed.
// /* 0x0A */ u32 countryRibbon:1;            // Distributed during Pokémon Festa '04 and '05 to tournament winners.
// /* 0x0B */ u32 nationalRibbon:1;           // Given to purified Shadow Pokémon in Colosseum/XD.
// /* 0x0B */ u32 earthRibbon:1;              // Given to teams that have beaten Mt. Battle's 100-battle challenge in Colosseum/XD.
// /* 0x0B */ u32 worldRibbon:1;              // Distributed during Pokémon Festa '04 and '05 to tournament winners.
// /* 0x0B */ u32 unusedRibbons:2;            // Discarded in Gen 4.
// /* 0x0B */ u32 abilityNum:2;
//
// // The functionality of this bit changed in FRLG:
// // In RS, this bit does nothing, is never set, & is accidentally unset when hatching Eggs.
// // In FRLG & Emerald, this controls Mew & Deoxys obedience and whether they can be traded.
// // If set, a Pokémon is a fateful encounter in FRLG's summary screen if hatched & for all Pokémon in Gen 4+ summary screens.
// // Set for in-game event island legendaries, events distributed after a certain date, & Pokémon from XD: Gale of Darkness.
// // Not to be confused with METLOC_FATEFUL_ENCOUNTER.
// /* 0x0B */ u32 modernFatefulEncounter:1;
//};
//
//// Number of bytes in the largest Pokémon substruct.
//// They are assumed to be the same size, and will be padded to
//// the largest size by the union.
//// By default they are all 12 bytes.
//#define NUM_SUBSTRUCT_BYTES (max(sizeof(struct PokemonSubstruct0),     \
//                             max(sizeof(struct PokemonSubstruct1),     \
//                             max(sizeof(struct PokemonSubstruct2),     \
//                                 sizeof(struct PokemonSubstruct3)))))
//
//union PokemonSubstruct
//{
//    struct PokemonSubstruct0 type0;
//    struct PokemonSubstruct1 type1;
//    struct PokemonSubstruct2 type2;
//    struct PokemonSubstruct3 type3;
//    u16 raw[NUM_SUBSTRUCT_BYTES / 2]; // /2 because it's u16, not u8
//};
//
//struct BoxPokemon
//{
//    u32 personality;
//    u32 otId;
//    u8 nickname[POKEMON_NAME_LENGTH];
//    u8 language;
//    u8 isBadEgg:1;
//    u8 hasSpecies:1;
//    u8 isEgg:1;
//    u8 blockBoxRS:1; // Unused, but Pokémon Box Ruby & Sapphire will refuse to deposit a Pokémon with this flag set
//    u8 unused:4;
//    u8 otName[PLAYER_NAME_LENGTH];
//    u8 markings;
//    u16 checksum;
//    u16 unknown;
//
//    union
//    {
//        u32 raw[(NUM_SUBSTRUCT_BYTES * 4) / 4]; // *4 because there are 4 substructs, /4 because it's u32, not u8
//        union PokemonSubstruct substructs[4];
//    } secure;
//};
//
//struct Pokemon
//{
//    struct BoxPokemon box;
//    u32 status;
//    u8 level;
//    u8 mail;
//    u16 hp;
//    u16 maxHP;
//    u16 attack;
//    u16 defense;
//    u16 speed;
//    u16 spAttack;
//    u16 spDefense;
//};
//
//struct MonSpritesGfxManager
//{
//    u32 numSprites:4;
//    u32 numSprites2:4; // Never read
//    u32 numFrames:8;
//    u32 active:8;
//    u32 dataSize:4;
//    u32 mode:4; // MON_SPR_GFX_MODE_*
//    void *spriteBuffer;
//    u8 **spritePointers;
//    struct SpriteTemplate *templates;
//    struct SpriteFrameImage *frameImages;
//};
//
//enum {
//    MON_SPR_GFX_MODE_NORMAL,
//    MON_SPR_GFX_MODE_BATTLE,
//    MON_SPR_GFX_MODE_FULL_PARTY,
//};
//
//enum {
//    MON_SPR_GFX_MANAGER_A,
//    MON_SPR_GFX_MANAGER_B, // Nothing ever sets up this manager.
//    MON_SPR_GFX_MANAGERS_COUNT
//};
//
//struct BattlePokemon
//{
//    /*0x00*/ u16 species;
//    /*0x02*/ u16 attack;
//    /*0x04*/ u16 defense;
//    /*0x06*/ u16 speed;
//    /*0x08*/ u16 spAttack;
//    /*0x0A*/ u16 spDefense;
//    /*0x0C*/ u16 moves[MAX_MON_MOVES];
//    /*0x14*/ u32 hpIV:5;
//    /*0x14*/ u32 attackIV:5;
//    /*0x15*/ u32 defenseIV:5;
//    /*0x15*/ u32 speedIV:5;
//    /*0x16*/ u32 spAttackIV:5;
//    /*0x17*/ u32 spDefenseIV:5;
//    /*0x17*/ u32 abilityNum:2;
//    /*0x18*/ s8 statStages[NUM_BATTLE_STATS];
//    /*0x20*/ u16 ability;
//    /*0x22*/ u8 type1;
//    /*0x23*/ u8 type2;
//    /*0x24*/ u8 type3;
//    /*0x25*/ u8 pp[MAX_MON_MOVES];
//    /*0x29*/ u16 hp;
//    /*0x2B*/ u8 level;
//    /*0x2C*/ u8 friendship;
//    /*0x2D*/ u16 maxHP;
//    /*0x2F*/ u16 item;
//    /*0x31*/ u8 nickname[POKEMON_NAME_LENGTH + 1];
//    /*0x3C*/ u8 ppBonuses;
//    /*0x3D*/ u8 otName[PLAYER_NAME_LENGTH + 1];
//    /*0x45*/ u32 experience;
//    /*0x49*/ u32 personality;
//    /*0x4D*/ u32 status1;
//    /*0x51*/ u32 status2;
//    /*0x55*/ u32 otId;
//    /*0x59*/ u8 metLevel;
//};
//
//struct Evolution
//{
//    u16 method;
//    u16 param;
//    u16 targetSpecies;
//};
//
//struct SpeciesInfo /*0x8C*/
//{
// /* 0x00 */ u8 baseHP;
// /* 0x01 */ u8 baseAttack;
// /* 0x02 */ u8 baseDefense;
// /* 0x03 */ u8 baseSpeed;
// /* 0x04 */ u8 baseSpAttack;
// /* 0x05 */ u8 baseSpDefense;
// /* 0x06 */ u8 types[2];
// /* 0x08 */ u8 catchRate;
// /* 0x09 */ u8 padding1;
// /* 0x0A */ u16 expYield; // expYield was changed from u8 to u16 for the new Exp System.
// /* 0x0C */ u16 evYield_HP:2;
//            u16 evYield_Attack:2;
//            u16 evYield_Defense:2;
//            u16 evYield_Speed:2;
// /* 0x0D */ u16 evYield_SpAttack:2;
//            u16 evYield_SpDefense:2;
//            u16 padding2:4;
// /* 0x0E */ u16 itemCommon;
// /* 0x10 */ u16 itemRare;
// /* 0x12 */ u8 genderRatio;
// /* 0x13 */ u8 eggCycles;
// /* 0x14 */ u8 friendship;
// /* 0x15 */ u8 growthRate;
// /* 0x16 */ u8 eggGroups[2];
// /* 0x18 */ u16 abilities[NUM_ABILITY_SLOTS]; // 3 abilities, no longer u8 because we have over 255 abilities now.
// /* 0x1E */ u8 safariZoneFleeRate;
//            // Pokédex data
// /* 0x1F */ u8 categoryName[13];
// /* 0x1F */ u8 speciesName[POKEMON_NAME_LENGTH + 1];
// /* 0x2C */ u16 cryId;
// /* 0x2E */ u16 natDexNum;
// /* 0x30 */ u16 height; //in decimeters
// /* 0x32 */ u16 weight; //in hectograms
// /* 0x34 */ u16 pokemonScale;
// /* 0x36 */ u16 pokemonOffset;
// /* 0x38 */ u16 trainerScale;
// /* 0x3A */ u16 trainerOffset;
// /* 0x3C */ const u8 *description;
// /* 0x40 */ u8 bodyColor : 7;
//            // Graphical Data
//            u8 noFlip : 1;
// /* 0x41 */ u8 frontAnimDelay;
// /* 0x42 */ u8 frontAnimId;
// /* 0x43 */ u8 backAnimId;
// /* 0x44 */ const union AnimCmd *const *frontAnimFrames;
// /* 0x48 */ const u32 *frontPic;
// /* 0x4C */ const u32 *frontPicFemale;
// /* 0x50 */ const u32 *backPic;
// /* 0x54 */ const u32 *backPicFemale;
// /* 0x58 */ const u32 *palette;
// /* 0x5C */ const u32 *paletteFemale;
// /* 0x60 */ const u32 *shinyPalette;
// /* 0x64 */ const u32 *shinyPaletteFemale;
// /* 0x68 */ const u8 *iconSprite;
// /* 0x6C */ const u8 *iconSpriteFemale;
// /* 0x70 */ const u8 *footprint;
//            // All Pokémon pics are 64x64, but this data table defines where in this 64x64 frame the sprite's non-transparent pixels actually are.
// /* 0x74 */ u8 frontPicSize; // The dimensions of this drawn pixel area.
// /* 0x74 */ u8 frontPicSizeFemale; // The dimensions of this drawn pixel area.
// /* 0x75 */ u8 frontPicYOffset; // The number of pixels between the drawn pixel area and the bottom edge.
// /* 0x76 */ u8 backPicSize; // The dimensions of this drawn pixel area.
// /* 0x76 */ u8 backPicSizeFemale; // The dimensions of this drawn pixel area.
// /* 0x77 */ u8 backPicYOffset; // The number of pixels between the drawn pixel area and the bottom edge.
// /* 0x78 */ u8 iconPalIndex:3;
//            u8 iconPalIndexFemale:3;
//            u8 padding3:2;
// /* 0x79 */ u8 enemyMonElevation; // This determines how much higher above the usual position the enemy Pokémon is during battle. Species that float or fly have nonzero values.
//            // Flags
// /* 0x7A */ u32 isLegendary:1;
//            u32 isMythical:1;
//            u32 isUltraBeast:1;
//            u32 isParadoxForm:1;
//            u32 isMegaEvolution:1;
//            u32 isPrimalReversion:1;
//            u32 isUltraBurst:1;
//            u32 isGigantamax:1;
//            u32 isAlolanForm:1;
//            u32 isGalarianForm:1;
//            u32 isHisuianForm:1;
//            u32 isPaldeanForm:1;
//            u32 cannotBeTraded:1;
//            u32 allPerfectIVs:1;
//            u32 dexForceRequired:1; // This species will be taken into account for Pokédex ratings even if they have the "isMythical" flag set.
//            u32 padding4:17;
//            // Move Data
// /* 0x80 */ const struct LevelUpMove *levelUpLearnset;
// /* 0x84 */ const u16 *teachableLearnset;
// /* 0x88 */ const struct Evolution *evolutions;
// /* 0x84 */ const u16 *formSpeciesIdTable;
// /* 0x84 */ const struct FormChange *formChangeTable;
//};
//
//struct BattleMove
//{
//    u16 effect;
//    u8 power;
//    u8 type;
//    u8 accuracy;
//    u8 pp;
//    u8 secondaryEffectChance;
//    u16 target;
//    s8 priority;
//    u8 split;
//    u16 argument;
//    u8 zMoveEffect;
//    // Flags
//    u32 makesContact:1;
//    u32 ignoresProtect:1;
//    u32 magicCoatAffected:1;
//    u32 snatchAffected:1;
//    u32 mirrorMoveBanned:1;
//    u32 ignoresKingsRock:1;
//    u32 highCritRatio:1;
//    u32 twoTurnMove:1;
//    u32 punchingMove:1;
//    u32 sheerForceBoost:1;
//    u32 bitingMove:1;
//    u32 pulseMove:1;
//    u32 soundMove:1;
//    u32 ballisticMove:1;
//    u32 protectionMove:1;
//    u32 powderMove:1;
//    u32 danceMove:1;
//    u32 windMove:1;
//    u32 slicingMove:1;
//    u32 minimizeDoubleDamage:1;
//    u32 ignoresTargetAbility:1;
//    u32 ignoresTargetDefenseEvasionStages:1;
//    u32 damagesUnderground:1;
//    u32 damagesUnderwater:1;
//    u32 damagesAirborne:1;
//    u32 damagesAirborneDoubleDamage:1;
//    u32 ignoreTypeIfFlyingAndUngrounded:1;
//    u32 thawsUser:1;
//    u32 ignoresSubstitute:1;
//    u32 strikeCount:4;  // Max 15 hits. Defaults to 1 if not set. May apply its effect on each hit.
//    u32 forcePressure:1;
//    u32 cantUseTwice:1;
//    u32 gravityBanned:1;
//    u32 healBlockBanned:1;
//    u32 meFirstBanned:1;
//    u32 mimicBanned:1;
//    u32 metronomeBanned:1;
//    u32 copycatBanned:1;
//    u32 assistBanned:1; // Matches same moves as copycatBanned + semi-invulnerable moves and Mirror Coat.
//    u32 sleepTalkBanned:1;
//    u32 instructBanned:1;
//    u32 encoreBanned:1;
//    u32 parentalBondBanned:1;
//    u32 skyBattleBanned:1;
//    u32 sketchBanned:1;
//};
//
//#define SPINDA_SPOT_WIDTH 16
//#define SPINDA_SPOT_HEIGHT 16
//
//struct SpindaSpot
//{
//    u8 x, y;
//    u16 image[SPINDA_SPOT_HEIGHT];
//};
//
//struct LevelUpMove
//{
//    u16 move;
//    u16 level;
//};
//
//struct FormChange
//{
//    u16 method;
//    u16 targetSpecies;
//    u16 param1;
//    u16 param2;
//    u16 param3;
//};
//
//struct Fusion
//{
//    u16 fusionStorageIndex;
//    u16 itemId;
//    u16 targetSpecies1;
//    u16 targetSpecies2;
//    u16 fusingIntoMon;
//    u16 fusionMove;
//    u16 unfuseForgetMove;
//};
//
//extern const struct Fusion *const gFusionTablePointers[NUM_SPECIES];
//
//
//
//








/*
[System.Serializable]
public class Pokemon
{
    public int SpeciesId;
    public string Nickname;
    public int Level;
    public int Experience;

    public int Personality;
    public int OriginalTrainerId;

    public Stats BaseStats;
    public Stats CurrentStats;

    public EVs EVs;
    public IVs IVs;

    public List<MoveInstance> Moves = new List<MoveInstance>(4);

    public int Friendship;
    public bool IsEgg;
    public bool IsShiny;

    public int HeldItemId;
    public int AbilityId;

    public Gender Gender;
    public Nature Nature;
}

[System.Serializable]
public class Stats
{
    public int HP;
    public int Attack;
    public int Defense;
    public int Speed;
    public int SpecialAttack;
    public int SpecialDefense;
}

[System.Serializable]
public class IVs
{
    public int HP;
    public int Attack;
    public int Defense;
    public int Speed;
    public int SpecialAttack;
    public int SpecialDefense;
}

[System.Serializable]
public class EVs
{
    public int HP;
    public int Attack;
    public int Defense;
    public int Speed;
    public int SpecialAttack;
    public int SpecialDefense;
}

[System.Serializable]
public class MoveInstance
{
    public int MoveId;
    public int CurrentPP;
    public int MaxPP;
}

[System.Serializable]
public class MoveData
{
    public int Id;
    public string Name;
    public MoveType Type;
    public int Power;
    public int Accuracy;
    public int PP;
    public int Priority;

    public bool MakesContact;
    public bool IsSoundMove;
    public bool IsPunchMove;
}


[CreateAssetMenu(menuName = "Pokemon/Species")]
public class SpeciesData : ScriptableObject
{
    public int SpeciesId;
    public string SpeciesName;

    public Stats BaseStats;
    public PokemonType PrimaryType;
    public PokemonType SecondaryType;

    public int CatchRate;
    public int ExpYield;
    public int BaseFriendship;

    public int[] Abilities;
    public int HiddenAbility;

    public EvolutionData[] Evolutions;

    public float Height;
    public float Weight;

    public bool IsLegendary;
    public bool IsMythical;
}

[System.Serializable]
public class EvolutionData
{
    public EvolutionMethod Method;
    public int Parameter;
    public int TargetSpeciesId;
}

public enum EvolutionMethod
{
    LevelUp,
    UseItem,
    Trade,
    Friendship,
    TimeOfDay,
    Location
}

public class BattlePokemon
{
    public Pokemon Source;

    public Stats BattleStats;
    public int CurrentHP;

    public int[] StatStages = new int[6];

    public List<MoveInstance> Moves;
}

public enum PokemonType
{
    Normal, Fire, Water, Grass, Electric, Ice,
    Fighting, Poison, Ground, Flying,
    Psychic, Bug, Rock, Ghost, Dragon,
    Dark, Steel, Fairy
}

public enum Gender
{
    Male,
    Female,
    Genderless
}

public enum Nature
{
    Hardy, Lonely, Brave, Adamant, Naughty,
    Bold, Docile, Relaxed, Impish, Lax,
    Timid, Hasty, Serious, Jolly, Naive,
    Modest, Mild, Quiet, Bashful, Rash,
    Calm, Gentle, Sassy, Careful, Quirky
}

public enum MoveType
{
    Physical,
    Special,
    Status
}

///
/// 
/// 

public static class PokemonFactory
{
    public static Pokemon Create(
        SpeciesData species,
        int level,
        int? forcedIV = null,
        Nature? forcedNature = null,
        int? forcedPersonality = null)
    {
        Pokemon mon = new Pokemon
        {
            SpeciesId = species.SpeciesId,
            Level = level,
            Experience = ExperienceTable.GetXPForLevel(level),
            Personality = forcedPersonality ?? UnityEngine.Random.Range(0, int.MaxValue),
            Nature = forcedNature ?? NatureService.GetNatureFromPersonality(),
            IVs = IVService.GenerateIVs(forcedIV),
            EVs = new EVs(),
            Friendship = species.BaseFriendship
        };

        mon.Gender = GenderService.DetermineGender(species, mon.Personality);
        mon.IsShiny = ShinyService.IsShiny(mon.Personality, mon.OriginalTrainerId);

        StatCalculator.Calculate(mon, species);
        MoveService.AssignInitialMoves(mon, species);

        return mon;
    }
}

public static class StatCalculator
{
    public static void Calculate(Pokemon mon, SpeciesData species)
    {
        Stats s = new Stats();

        s.HP = CalculateHP(
            species.BaseStats.HP,
            mon.IVs.HP,
            mon.EVs.HP,
            mon.Level
        );

        s.Attack = CalculateOther(
            species.BaseStats.Attack,
            mon.IVs.Attack,
            mon.EVs.Attack,
            mon.Level,
            NatureService.GetModifier(mon.Nature, StatType.Attack)
        );

        // Repeat for other stats...

        mon.CurrentStats = s;
    }

    static int CalculateHP(int baseStat, int iv, int ev, int level)
    {
        return (((2 * baseStat + iv + (ev / 4)) * level) / 100) + level + 10;
    }

    static int CalculateOther(int baseStat, int iv, int ev, int level, float nature)
    {
        int stat = (((2 * baseStat + iv + (ev / 4)) * level) / 100) + 5;
        return Mathf.FloorToInt(stat * nature);
    }
}

public static class NatureService
{
    private static readonly float[,] NatureTable =
    {
        // Atk, Def, SpA, SpD, Spe
        {1,1,1,1,1}, // Hardy
        {1.1f,0.9f,1,1,1}, // Lonely
        // ...
    };

    public static Nature GetNatureFromPersonality()
    {
        return (Nature)(UnityEngine.Random.Range(0, 25));
    }

    public static float GetModifier(Nature nature, StatType stat)
    {
        return NatureTable[(int)nature, (int)stat];
    }
}

public static class MoveService
{
    public static void AssignInitialMoves(Pokemon mon, SpeciesData species)
    {
        foreach (var move in species.LevelUpMoves)
        {
            if (move.Level <= mon.Level)
                LearnMove(mon, move.MoveId);
        }
    }

    public static bool LearnMove(Pokemon mon, int moveId)
    {
        if (mon.Moves.Count < 4)
        {
            mon.Moves.Add(new MoveInstance(moveId));
            return true;
        }

        return false; // prompt UI to replace
    }
}

public static class LevelService
{
    public static bool TryLevelUp(Pokemon mon, SpeciesData species)
    {
        int nextLevelXP = ExperienceTable.GetXPForLevel(mon.Level + 1);

        if (mon.Experience >= nextLevelXP)
        {
            mon.Level++;
            StatCalculator.Calculate(mon, species);
            return true;
        }

        return false;
    }
}

public static class ShinyService
{
    public static bool IsShiny(int personality, int otId)
    {
        int value =
            (otId >> 16) ^
            (otId & 0xFFFF) ^
            (personality >> 16) ^
            (personality & 0xFFFF);

        return value < 16;
    }
}

public static class EvolutionService
{
    public static int CheckEvolution(Pokemon mon, SpeciesData species)
    {
        foreach (var evo in species.Evolutions)
        {
            if (evo.Method == EvolutionMethod.LevelUp &&
                mon.Level >= evo.Parameter)
            {
                return evo.TargetSpeciesId;
            }
        }

        return -1;
    }
}

public enum GrowthRate
{
    MediumFast,
    Erratic,
    Fluctuating,
    MediumSlow,
    Fast,
    Slow
}

public static class ExperienceCalculator
{
    public static int GetTotalExperience(GrowthRate rate, int level)
    {
        level = Mathf.Clamp(level, 1, 100);

        return rate switch
        {
            GrowthRate.MediumFast => MediumFast(level),
            GrowthRate.Erratic => Erratic(level),
            GrowthRate.Fluctuating => Fluctuating(level),
            GrowthRate.MediumSlow => MediumSlow(level),
            GrowthRate.Fast => Fast(level),
            GrowthRate.Slow => Slow(level),
            _ => 0
        };
    }

    static int MediumFast(int n) => n * n * n;

    static int Fast(int n) => (4 * n * n * n) / 5;

    static int Slow(int n) => (5 * n * n * n) / 4;

    static int MediumSlow(int n)
    {
        return (int)((6f / 5f) * n * n * n - 15 * n * n + 100 * n - 140);
    }

    static int Erratic(int n)
    {
        if (n <= 50)
            return (100 - n) * n * n * n / 50;
        if (n <= 68)
            return (150 - n) * n * n * n / 100;
        if (n <= 98)
            return ((1911 - 10 * n) * n * n * n) / 1500;

        return (160 - n) * n * n * n / 100;
    }

    static int Fluctuating(int n)
    {
        if (n <= 15)
            return ((n + 1) / 3 + 24) * n * n * n / 50;
        if (n <= 36)
            return (n + 14) * n * n * n / 50;

        return (n / 2 + 32) * n * n * n / 50;
    }
}

public static int GetLevelFromXP(GrowthRate rate, int totalXP)
{
    for (int level = 1; level <= 100; level++)
    {
        if (totalXP < GetTotalExperience(rate, level))
            return level - 1;
    }
    return 100;
}
*/



}



