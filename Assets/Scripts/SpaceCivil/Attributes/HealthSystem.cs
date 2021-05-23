namespace SpaceCivil.Attributes {

    public class HealthSystem {
        private const float MAXProtection = 1f;
        private float _maxHealth;
        private float curHealth;

        private readonly float _curProtection;
        public bool HealthStatus;

        public HealthSystem(float maxHealth, float curProtection) {
            System.Diagnostics.Debug.Assert(maxHealth > 0);
            _curProtection = curProtection;
            curHealth = _maxHealth = maxHealth;
            HealthStatus = true;
        }

        public void ReceiveHealthDamage(float damage) {
            curHealth -= damage * (MAXProtection - _curProtection);
            if (curHealth <= 0) {
                HealthStatus = false;
            }
        }
    }
}