// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.EMailAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.SM;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
public sealed class EMailAccountExt : PXCacheExtension<
#nullable disable
EMailAccount>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>();

  [PXDBBool]
  [PXUIField(DisplayName = "Submit Incoming Documents")]
  public bool? SubmitToIncomingAPDocuments { get; set; }

  [Branch(null, null, false, true, false, Required = false)]
  [PXUIField(DisplayName = "Default Branch")]
  public int? DefaultBranchID { get; set; }

  public abstract class submitToIncomingAPDocuments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailAccountExt.submitToIncomingAPDocuments>
  {
  }

  public abstract class defaultBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccountExt.defaultBranchID>
  {
  }
}
