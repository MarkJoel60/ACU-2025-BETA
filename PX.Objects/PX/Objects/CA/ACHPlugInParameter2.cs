// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInParameter2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("ACHPlugInParameter")]
[Serializable]
public class ACHPlugInParameter2 : ACHPlugInParameter
{
  [PXDBString(10, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXParent(typeof (Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<ACHPlugInParameter2.paymentMethodID>>>>))]
  public override 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXDefault(typeof (PaymentMethod.aPBatchExportPlugInTypeName))]
  public override string PlugInTypeName { get; set; }

  [PXDBString(30, IsKey = true)]
  public override string ParameterID { get; set; }

  [PXDBString(60)]
  public override string ParameterCode { get; set; }

  [PXDBString(4000)]
  public override string Value { get; set; }

  public new abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter2.paymentMethodID>
  {
  }

  public new abstract class plugInTypeName : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ACHPlugInParameter2.plugInTypeName>
  {
  }

  public new abstract class parameterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter2.parameterID>
  {
  }

  public new abstract class parameterCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInParameter2.parameterCode>
  {
  }

  public new abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ACHPlugInParameter2.value>
  {
  }
}
