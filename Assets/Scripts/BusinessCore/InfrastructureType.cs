namespace BusinessCore
{
    public abstract class InfrastructureType
    {
        public string Name { get; }
        public string Description { get; }

        protected InfrastructureType(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
