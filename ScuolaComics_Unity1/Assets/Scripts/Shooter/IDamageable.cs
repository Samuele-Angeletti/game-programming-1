
public interface IDamageable
{
    int vita { get; set; }
    int vitaMassima { get; set; }
    void TakeDamage(int damage);
}
