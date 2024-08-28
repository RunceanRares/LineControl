namespace Schaeffler.Common.Validation
{
   public class UniqueValidation
   {
      public UniqueValidation()
      {
         IsValid = true;
      }

      public UniqueValidation(string fieldName, object parameters = null)
      {
         IsValid = false;
         FieldName = fieldName;
         Parameters = parameters;
      }

      public bool IsValid { get; }

      public string FieldName { get; }

      public object Parameters { get; }
   }
}
