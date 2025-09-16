// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBStringListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Configures a dropdown control for a string field. The values and labels
/// for the dropdown control are taken from the specified database table.
/// </summary>
/// <example><para>In the example below, the lists of values and labels are taken from the CROpportunityProbability table.</para>
/// <code title="Example" lang="CS">
/// [PXDBString(1, IsUnicode = true)]
/// [PXUIField(DisplayName = "Probability", Visibility = PXUIVisibility.SelectorVisible)]
/// [PXDBStringList(
///     typeof(CROpportunityProbability),
///     typeof(CROpportunityProbability.code),
///     typeof(CROpportunityProbability.description),
///     DefaultValueField = typeof(CROpportunityProbability.isDefault))]
/// public virtual string Probability { get; set; }</code>
/// </example>
/// <summary>Initializes a new instance of the attribute.</summary>
/// <param name="table">The DAC representing the table whose fields
/// are used as sources of values and labels.</param>
/// <param name="valueField">The field holding string values.</param>
/// <param name="descriptionField">The field holding labels.</param>
public sealed class PXDBStringListAttribute(System.Type table, System.Type valueField, System.Type descriptionField) : 
  PXBaseListAttribute((IPXDBListAttributeHelper) new PXDBStringListAttribute.PXDBStringAttributeHelper(table, valueField, descriptionField))
{
  /// <exclude />
  private class PXDBStringAttributeHelper(System.Type table, System.Type valueField, System.Type descriptionField) : 
    PXDBListAttributeHelper<string>(table, valueField, descriptionField)
  {
    protected override object CreateState(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      string[] values,
      string[] labels,
      string fieldName,
      string defaultValue)
    {
      if (values.Length != labels.Length)
      {
        PXTrace.WriteInformation($"CRStringAttributeHelper CreateState {sender.GetItemType().Name}_{fieldName}: Invalide values and labels");
        int num = System.Math.Max(values.Length, labels.Length);
        for (int index = 0; index < num; ++index)
          PXTrace.WriteInformation($"'{(index < values.Length ? (object) values[index] : (object) "<error>")}' -> '{(index < labels.Length ? (object) labels[index] : (object) "<error>")}'");
      }
      return (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), fieldName, new bool?(), new int?(-1), (string) null, values, labels, new bool?(true), defaultValue);
    }

    protected override string EmptyLabelValue => string.Empty;
  }
}
