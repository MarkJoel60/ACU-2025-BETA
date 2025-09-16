// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocumentEnqRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARDocumentEnqRetainage : PXGraphExtension<
#nullable disable
ARDocumentEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.retainage>();

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Original Retainage")]
  protected virtual void ARDocumentResult_CuryRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Unreleased Retainage")]
  protected virtual void ARDocumentResult_CuryRetainageUnreleasedAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Total Amount")]
  protected virtual void ARDocumentResult_CuryOrigDocAmtWithRetainageTotal_CacheAttached(
    PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Retainage", Visible = false)]
  [PXDBCalced(typeof (Switch<Case<Where<Exists<Select<ARDocumentEnqRetainage.ARRegisterOrig, Where<ARDocumentEnqRetainage.ARRegisterOrig.origDocType, Equal<ARDocumentEnq.ARDocumentResult.docType>, And<ARDocumentEnqRetainage.ARRegisterOrig.origRefNbr, Equal<ARDocumentEnq.ARDocumentResult.refNbr>, And<ARDocumentEnqRetainage.ARRegisterOrig.branchID, Equal<ARDocumentEnq.ARDocumentResult.branchID>, And<ARDocumentEnqRetainage.ARRegisterOrig.released, Equal<True>>>>>>>>, True>, False>), typeof (bool))]
  protected virtual void ARDocumentResult_Retainage_CacheAttached(PXCache sender)
  {
  }

  [PXHidden]
  [Serializable]
  public class ARRegisterOrig : ARRegister
  {
    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnqRetainage.ARRegisterOrig.docType>
    {
    }

    public new abstract class refNbr : IBqlField, IBqlOperand
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnqRetainage.ARRegisterOrig.branchID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnqRetainage.ARRegisterOrig.released>
    {
    }

    public new abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnqRetainage.ARRegisterOrig.origDocType>
    {
    }

    public new abstract class origRefNbr : IBqlField, IBqlOperand
    {
    }
  }
}
