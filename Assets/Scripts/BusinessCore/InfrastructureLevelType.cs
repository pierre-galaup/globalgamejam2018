namespace BusinessCore
{
    public abstract class InfrastructureLevelType
    {
        public string Name { get; }
        public string Description { get; }

        protected InfrastructureLevelType(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
