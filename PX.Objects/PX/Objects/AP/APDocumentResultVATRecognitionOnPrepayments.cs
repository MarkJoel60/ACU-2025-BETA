// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentResultVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.AP;

/// <exclude />
public sealed class APDocumentResultVATRecognitionOnPrepayments : 
  PXCacheExtension<
  #nullable disable
  APDocumentEnq.APDocumentResult>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>();
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [Account(typeof (APRegister.branchID), IsDBField = false, DisplayName = "AP Account", BqlTable = typeof (APRegister))]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APRegister.prepaymentAccountID>, APRegister.aPAccountID>), typeof (int?))]
  public int? APAccountID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [SubAccount(typeof (APDocumentResultVATRecognitionOnPrepayments.aPAccountID), IsDBField = false, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", BqlTable = typeof (APRegister))]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APRegister.prepaymentSubID>, APRegister.aPSubID>), typeof (int?))]
  public int? APSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.APAccountID" />
  public abstract class aPAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentResultVATRecognitionOnPrepayments.aPAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.APSubID" />
  public abstract class aRSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentResultVATRecognitionOnPrepayments.aRSubID>
  {
  }
}
