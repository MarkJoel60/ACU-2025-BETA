// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.APInvoiceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public sealed class APInvoiceExt : PXCacheExtension<
#nullable disable
APInvoice>
{
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? RenameFileScreenId { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.apDocumentRecognition>();

  public abstract class renameFileScreenId : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceExt.renameFileScreenId>
  {
  }
}
