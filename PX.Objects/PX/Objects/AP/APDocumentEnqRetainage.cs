// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentEnqRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APDocumentEnqRetainage : PXGraphExtension<
#nullable disable
APDocumentEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Original Retainage")]
  protected virtual void APDocumentResult_CuryRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Unreleased Retainage")]
  protected virtual void APDocumentResult_CuryRetainageUnreleasedAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Total Amount")]
  protected virtual void APDocumentResult_CuryOrigDocAmtWithRetainageTotal_CacheAttached(
    PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Retainage", Visible = false)]
  [PXDBCalced(typeof (Switch<Case<Where<Exists<Select<APDocumentEnqRetainage.APRegisterOrig, Where<APDocumentEnqRetainage.APRegisterOrig.origDocType, Equal<APDocumentEnq.APDocumentResult.docType>, And<APDocumentEnqRetainage.APRegisterOrig.origRefNbr, Equal<APDocumentEnq.APDocumentResult.refNbr>, And<APDocumentEnqRetainage.APRegisterOrig.branchID, Equal<APDocumentEnq.APDocumentResult.branchID>, And<APDocumentEnqRetainage.APRegisterOrig.released, Equal<True>>>>>>>>, True>, False>), typeof (bool))]
  protected virtual void APDocumentResult_Retainage_CacheAttached(PXCache sender)
  {
  }

  [PXHidden]
  [Serializable]
  public class APRegisterOrig : APRegister
  {
    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnqRetainage.APRegisterOrig.docType>
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
      APDocumentEnqRetainage.APRegisterOrig.branchID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnqRetainage.APRegisterOrig.released>
    {
    }

    public new abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnqRetainage.APRegisterOrig.origDocType>
    {
    }

    public new abstract class origRefNbr : IBqlField, IBqlOperand
    {
    }

    public new abstract class hasMultipleProjects : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnqRetainage.APRegisterOrig.hasMultipleProjects>
    {
    }
  }
}
