using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author's name: Tom Tsiliopoulos
//Enum class for the order of audio sources.
[System.Serializable]
public enum SoundClip
{
    NONE = -1,
    Ghost_Dead,
    Zombie_dead,
    Skeletone_Dead,
    Game_Theme,
    GameOver,
    Fire,
    NUM_OF_CLIPS
}