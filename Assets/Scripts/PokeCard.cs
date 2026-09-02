using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokeCard
{
    public List<Ability> abilities ;
    public long base_experience ;
    public Cries cries ;
    public List<Species> forms ;
    public List<GameIndex> game_indices ;
    public long height ;
    public List<object> held_items ;
    public long id ;
    public bool is_default ;
    public string location_area_encounters ;
    public List<Move> moves ;
    public string name ;
    public long order ;
    public List<object> past_abilities ;
    public List<object> past_types ;
    public Species species ;
    public Sprites sprites ;
    public List<Stat> stats ;
    public List<TypeElement> types ;
    public long weight ;
}

[Serializable]
public class Ability
{
    public Species ability ;
    public bool is_hidden ;
    public long slot ;
}

[Serializable]
public class Species
{
    public string name ;
    public string url ;
}

[Serializable]
public class Cries
{
    public string latest ;
    public string legacy ;
}

[Serializable]
public class GameIndex
{
    public long game_index ;
    public Species version ;
}

[Serializable]
public class Move
{
    public Species move ;
    public List<VersionGroupDetail> version_group_details ;
}

[Serializable]
public class VersionGroupDetail
{
    public long level_learned_at ;
    public Species move_learn_method ;
    public Species version_group ;
}

[Serializable]
public class GenerationV
{
    public Sprites blackwhite ;
}

[Serializable]
public class GenerationIv
{
    public Sprites diamondpearl ;
    public Sprites heartgoldsoulsilver ;
    public Sprites platinum ;
}

[Serializable]
public class Versions
{
    public GenerationI generationi ;
    public GenerationIi generationii ;
    public GenerationIii generationiii ;
    public GenerationIv generationiv ;
    public GenerationV generationv ;
    public Dictionary<string, Home> generationvi ;
    public GenerationVii generationvii ;
    public GenerationViii generationviii ;
}

[Serializable]
public class Other
{
    public DreamWorld dream_world ;
    public Home home ;
    public OfficialArtwork officialartwork ;
    public Sprites showdown ;
}

[Serializable]
public class Sprites
{
    public string back_default ;
    public object back_female ;
    public string back_shiny ;
    public object back_shiny_female ;
    public string front_default ;
    public object front_female ;
    public string front_shiny ;
    public object front_shiny_female ;
    public Other other ;
    public Versions versions ;
    public Sprites animated ;
}

[Serializable]
public class GenerationI
{
    public RedBlue redblue ;
    public RedBlue yellow ;
}

[Serializable]
public class RedBlue
{
    public string back_default ;
    public string back_gray ;
    public string back_transparent ;
    public string front_default ;
    public string front_gray ;
    public string front_transparent ;
}

[Serializable]
public class GenerationIi
{
    public Crystal crystal ;
    public Gold gold ;
    public Gold silver ;
}

[Serializable]
public class Crystal
{
    public string back_default ;
    public string back_shiny ;
    public string back_shiny_transparent ;
    public string back_transparent ;
    public string front_default ;
    public string front_shiny ;
    public string front_shiny_transparent ;
    public string front_transparent ;
}

[Serializable]
public class Gold
{
    public string back_default ;
    public string back_shiny ;
    public string front_default ;
    public string front_shiny ;
    public string front_transparent ;
}

[Serializable]
public class GenerationIii
{
    public OfficialArtwork emerald ;
    public Gold fireredleafgreen ;
    public Gold rubysapphire ;
}

[Serializable]
public class OfficialArtwork
{
    public string front_default ;
    public string front_shiny ;
}

[Serializable]
public class Home
{
    public string front_default ;
    public object front_female ;
    public string front_shiny ;
    public object front_shiny_female ;
}

[Serializable]
public class GenerationVii
{
    public DreamWorld icons ;
    public Home ultrasunultramoon ;
}

[Serializable]
public class DreamWorld
{
    public string front_default ;
    public object front_female ;
}

[Serializable]
public class GenerationViii
{
    public DreamWorld icons ;
}

[Serializable]
public class Stat
{
    public long base_stat ;
    public long effort ;
    public Species stat ;
}

[Serializable]
public class TypeElement
{
    public long slot ;
    public Species type ;
}
