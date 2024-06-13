using COM.David.TurnCharacter;

public class PlayerSkillsController : CharacterSkillBase
{
    // Start is called before the first frame update
    private void Start()
    {
        abilities.Add(new NormalAttack());
        abilities.Add(new DoubleAttack());
        abilities.Add(new Defense());
    }
}
public class NormalAttack : Ability
{
    public NormalAttack()
    {
        Name = "Attaque Normale";
        Description = "Je suis une simple attaque";
    }

    public override void ExecAbility(CharacterBase _from, CharacterBase _to)
    {
        _to.GetHit(_from);
    }
}
public class DoubleAttack : Ability
{
    public DoubleAttack()
    {
        Name = "Double Attaque";
        Description = "Je suis une double attaque";
    }

    public override void ExecAbility(CharacterBase _from, CharacterBase _to)
    {
        _to.GetHit(_from);
        _to.GetHit(_from);
    }
}
public class Defense : Ability
{
    public Defense()
    {
        Name = "Defense";
        Description = "Je suis une protection";
    }

    public override void ExecAbility(CharacterBase _from, CharacterBase _to)
    {
        // Stuff To Do
    }
}