// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.CacheExtensions.PoOrderExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CN.Subcontracts.PO.CacheExtensions;

public sealed class PoOrderExt : PXCacheExtension<
#nullable disable
PX.Objects.PO.POOrder>
{
  [CRAttributesField(typeof (PoOrderExt.subcontractClassID), typeof (PX.Objects.PO.POOrder.noteID))]
  public string[] Attributes { get; set; }

  [PXString(20)]
  public string SubcontractClassID => "SUBCONTRACTS";

  [ToWords(typeof (PX.Objects.PO.POOrder.curyOrderTotal))]
  public string OrderTotalInWords { get; set; }

  /// <summary>Used only for attribute.</summary>
  [PXBool]
  [UploadFileNameCorrectorForSubcontracts]
  public bool? UploadFileNameCorrectorStub { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class orderTotalInWords : IBqlField, IBqlOperand
  {
  }

  public abstract class subcontractClassID : IBqlField, IBqlOperand
  {
  }

  public abstract class attributes : IBqlField, IBqlOperand
  {
  }

  public abstract class uploadFileNameCorrectorStub : IBqlField, IBqlOperand
  {
  }

  public class subcontractClass : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PoOrderExt.subcontractClass>
  {
    public subcontractClass()
      : base("SUBCONTRACTS")
    {
    }
  }

  public class pOOrderExtTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PoOrderExt.pOOrderExtTypeName>
  {
    public pOOrderExtTypeName()
      : base(typeof (PoOrderExt).FullName)
    {
    }
  }
}
