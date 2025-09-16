// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingRecordEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXBreakInheritance]
[PXHidden]
[ExcludeFromCodeCoverage]
public class PMBillingRecordEx : PMBillingRecord
{
  public new abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecordEx.projectID>
  {
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRecordEx.recordID>
  {
  }

  public new abstract class billingTag : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRecordEx.billingTag>
  {
  }

  public new abstract class proformaRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRecordEx.proformaRefNbr>
  {
  }

  public new abstract class aRDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRecordEx.aRDocType>
  {
  }

  public new abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRecordEx.aRRefNbr>
  {
  }
}
