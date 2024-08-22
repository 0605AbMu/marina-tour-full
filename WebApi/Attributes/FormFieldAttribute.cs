namespace WebApi.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FormFieldAttribute : Attribute
{
     public string? Name { get; set; }
}