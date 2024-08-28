namespace LineControllerInfrastructure.ContextConfiguration
{
  public class UniqueValidation
  {
    public UniqueValidation()
    {
      IsValid = true;
    }

    public UniqueValidation(string fieldName)
    {
      IsValid = false;
      FieldName = fieldName;
    }

    public bool IsValid { get; }

    public string FieldName { get; }
  }
}
