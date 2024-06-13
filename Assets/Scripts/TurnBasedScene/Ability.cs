

using COM.David.TurnCharacter;

public abstract class Ability
{
    public string Name { get; set; }
    public string Description { get; set; }

    public abstract void  ExecAbility(CharacterBase _from, CharacterBase _to);
}
