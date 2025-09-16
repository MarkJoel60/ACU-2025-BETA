// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.SideBySideComparison.Merge;

/// <summary>The filter that is used for merging of entities.</summary>
[PXHidden]
public class MergeEntitiesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Specifies the record that is used as a target.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <see cref="F:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesFilter.targetRecord.CurrentRecord" />,
  /// <see cref="F:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesFilter.targetRecord.SelectedRecord" />.
  /// </value>
  [PXInt]
  [PXIntList(new int[] {0, 1}, new string[] {"Current Record", "Duplicate Record"})]
  [PXUnboundDefault(0)]
  [PXUIField(DisplayName = "Target Record")]
  public int? TargetRecord { get; set; }

  /// <summary>The caption under the grid with extra information.</summary>
  /// <remarks>Can be made hidden.</remarks>
  [PXString(IsUnicode = true)]
  [PXUnboundDefault("Select the field values that you want to keep in the target record.")]
  [PXUIField(Enabled = false)]
  public 
  #nullable disable
  string Caption { get; set; }

  /// <summary>The ID of the entity that is used for merging.</summary>
  /// <value>
  /// Corresponds to the real ID of the entity, such as
  /// <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> or <see cref="P:PX.Objects.CR.Contact.ContactID" />.
  /// </value>
  [PXString]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public string MergeEntityID { get; set; }

  public abstract class targetRecord : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MergeEntitiesFilter.targetRecord>
  {
    public const int CurrentRecord = 0;
    public const int SelectedRecord = 1;
  }

  public abstract class caption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MergeEntitiesFilter.caption>
  {
  }

  public abstract class mergeEntityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MergeEntitiesFilter.mergeEntityID>
  {
  }
}
