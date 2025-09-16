// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.CacheExtensions.PoSetupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.CacheExtensions;

public sealed class PoSetupExt : PXCacheExtension<POSetup>
{
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("SUBCONTR")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Subcontract Numbering Sequence")]
  public string SubcontractNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Total on Entry")]
  public bool? RequireSubcontractControlTotal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsSubcontractSetupSaved { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class subcontractNumberingID : IBqlField, IBqlOperand
  {
  }

  public abstract class requireSubcontractControlTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class isSubcontractSetupSaved : IBqlField, IBqlOperand
  {
  }
}
