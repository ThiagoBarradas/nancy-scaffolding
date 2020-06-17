namespace Nancy.Scaffolding.Healthcheck
{
    public interface IHealthcheck
    {
        string Name { get; }

        (bool result, string description) IsHealth();
    }
}
