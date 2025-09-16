// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocumentResultVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.AR;

/// <exclude />
public sealed class ARDocumentResultVATRecognitionOnPrepayments : 
  PXCacheExtension<
  #nullable disable
  ARDocumentEnq.ARDocumentResult>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  [PXMergeAttributes]
  [Account(typeof (ARRegister.branchID), IsDBField = false, DisplayName = "AR Account", BqlTable = typeof (ARRegister))]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARRegister.prepaymentAccountID>, ARRegister.aRAccountID>), typeof (int?))]
  public int? ARAccountID { get; set; }

  [PXMergeAttributes]
  [SubAccount(typeof (ARDocumentResultVATRecognitionOnPrepayments.aRAccountID), IsDBField = false, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AR Subaccount", BqlTable = typeof (ARRegister))]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARRegister.prepaymentSubID>, ARRegister.aRSubID>), typeof (int?))]
  public int? ARSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.ARAccountID" />
  public abstract class aRAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentResultVATRecognitionOnPrepayments.aRAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.ARSubID" />
  public abstract class aRSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentResultVATRecognitionOnPrepayments.aRSubID>
  {
  }
}
