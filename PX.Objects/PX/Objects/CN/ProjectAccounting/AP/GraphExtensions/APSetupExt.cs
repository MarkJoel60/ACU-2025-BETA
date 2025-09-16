// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.GraphExtensions.APSetupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;

public sealed class APSetupExt : PXCacheExtension<
#nullable disable
APSetup>
{
  /// <summary>Enable invoice reclassifications</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Bill Reclassification")]
  public bool? ReclassifyInvoices { get; set; }

  public abstract class reclassifyInvoices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APSetupExt.reclassifyInvoices>
  {
  }
}
