﻿[System.Serializable]
public class WeaponInfo : ItemInfo
{
    public string _name;
    public string _type1;
    public string _type2;
    public string _code;
    public string _alternateGfx;
    public string nameStr;
    public int version;
    public int compactSave;
    public int rarity;
    public bool spawnable;
    public int minDamage;
    public int maxDamage;
    public bool oneOrTwoHanded;
    public bool twoHanded;
    public int twoHandedMinDamage;
    public int twoHandedMaxDamage;
    public int missileMinDamage;
    public int missileMaxDamage;
    public int unknown;
    public int rangeAdder;
    public int speed;
    public int strBonus;
    public int dexBonus;
    public int reqStr;
    public int reqDex;
    public int durability;
    public bool noDurability;
    public int _level;
    public int _levelReq;
    public int _cost;
    public int _gambleCost;
    public int _magicLvl;
    public string autoPrefix;
    public string openBetaGfx;
    public string normCode;
    public string uberCode;
    public string ultraCode;
    public string wClass;
    public string twoHandedWClass;
    public int _component;
    public string _hitClass;
    public int _invWidth;
    public int _invHeight;
    public bool _stackable;
    public int minStack;
    public int maxStack;
    public int spawnStack;
    public string _flippyFile;
    public string _invFile;
    public string _uniqueInvFile;
    public string _setInvFile;
    public bool _hasInv;
    public int _gemSockets;
    public string _gemApplyType;
    public string special;
    public bool _useable;
    public string _dropSound;
    public int _dropSoundFrame;
    public string _useSound;
    [Datasheet.Sequence(length = 107)]
    public string[] skipped2;

    [System.NonSerialized]
    public WeaponHitClass hitClass;
}
