// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBIntListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Configures a drop-down control for an integer field. The values and labels
/// for the drop-down control are taken from the specified database table.
/// </summary>
/// <example>
/// <code>
/// [PXDBInt]
/// [PXUIField(DisplayName = "Stage", Visibility = PXUIVisibility.SelectorVisible)]
/// [PXDBIntList(
///    typeof(CROpportunityStage),
///    typeof(CROpportunityStage.stageCode),
///    typeof(CROpportunityStage.description),
///    DefaultValueField = typeof(CROpportunityStage.isDefault))]
/// public virtual int? StageID { get; set; }
/// </code>
/// </example>
/// <summary>Initializes a new instance of the attribute.</summary>
/// <param name="table">The DAC representing the table whose fields
/// are used as sources of values and labels.</param>
/// <param name="valueField">The field holding integer values.</param>
/// <param name="descriptionField">The field holding labels.</param>
public sealed class PXDBIntListAttribute(System.Type table, System.Type valueField, System.Type descriptionField) : 
  PXBaseListAttribute((IPXDBListAttributeHelper) new PXDBIntListAttribute.PXDBIntAttributeHelper(table, valueField, descriptionField))
{
  /// <exclude />
  private class PXDBIntAttributeHelper(System.Type table, System.Type valueField, System.Type descriptionField) : 
    PXDBListAttributeHelper<int>(table, valueField, descriptionField)
  {
    protected override object CreateState(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      int[] values,
      string[] labels,
      string fieldName,
      int defaultValue)
    {
      return (object) PXIntState.CreateInstance(e.ReturnState, fieldName, new bool?(), new int?(-1), new int?(), new int?(), values, labels, (System.Type) null, new int?(defaultValue));
    }
  }
}
