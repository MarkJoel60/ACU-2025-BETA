// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.PoOrderFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.Subcontracts.AP.Descriptor;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

public sealed class PoOrderFilterExt : PXCacheExtension<
#nullable disable
POOrderFilter>
{
  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  public string OrderNbr { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Subcontract Nbr.")]
  [SubcontractNumberSelector]
  public string SubcontractNumber { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Unbilled Lines")]
  public bool? ShowUnbilledLines { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class showUnbilledLines : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PoOrderFilterExt.showUnbilledLines>
  {
  }

  public abstract class subcontractNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PoOrderFilterExt.subcontractNumber>
  {
  }
}
