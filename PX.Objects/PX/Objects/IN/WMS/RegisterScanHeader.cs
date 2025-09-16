// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.RegisterScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class RegisterScanHeader : PXCacheExtension<
#nullable disable
WMSScanHeader, QtyScanHeader, ScanHeader>
{
  [PXUnboundDefault(typeof (INRegister.docType))]
  [PXString(1, IsFixed = true)]
  [INDocType.List]
  public string DocType { get; set; }

  [PXString]
  [PXSelector(typeof (SearchFor<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.CS.ReasonCode.usage, IBqlString>.IsEqual<BqlField<RegisterScanHeader.docType, IBqlString>.FromCurrent>>), "The usage type of the reason code does not match the document type.", new Type[] {})]
  public string ReasonCodeID { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RegisterScanHeader.docType>
  {
  }

  public abstract class reasonCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RegisterScanHeader.reasonCodeID>
  {
  }
}
