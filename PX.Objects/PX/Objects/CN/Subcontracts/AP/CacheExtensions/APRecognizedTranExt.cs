// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.APRecognizedTranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

/// <inheritdoc cref="T:PX.Objects.AP.InvoiceRecognition.DAC.APRecognizedTran" />
public sealed class APRecognizedTranExt : PXCacheExtension<
#nullable disable
APRecognizedTran>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  /// <summary>The recognized subcontract number.</summary>
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Subcontract Nbr.", Enabled = false)]
  public string RecognizedSubcontractNumber
  {
    get => !(this.Base.POOrderType == "RS") ? (string) null : this.Base.RecognizedPONumber;
  }

  /// <summary>
  /// The position of the subcontract number in the recognized document. The position is in JSON format.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Subcontract Number (JSON)", Enabled = false, Visible = false)]
  public string SubcontractNumberJson
  {
    get => !(this.Base.POOrderType == "RS") ? (string) null : this.Base.PONumberJson;
  }

  /// <summary>The number of the recognized subcontract line.</summary>
  [PXInt]
  [PXUIField(DisplayName = "Subcontract Line", Visible = false)]
  public int? RecognizedSubcontractLineNbr
  {
    get => !(this.Base.POOrderType == "RS") ? new int?() : this.Base.POLineNbr;
  }

  public abstract class recognizedSubcontractNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedTranExt.recognizedSubcontractNumber>
  {
  }

  public abstract class subcontractNumberJson : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRecognizedTranExt.subcontractNumberJson>
  {
  }

  public abstract class recognizedSubcontractLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRecognizedTranExt.recognizedSubcontractLineNbr>
  {
  }
}
