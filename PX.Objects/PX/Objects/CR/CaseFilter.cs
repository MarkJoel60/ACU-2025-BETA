// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CaseFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class CaseFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Case Class")]
  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string CaseClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Business Account Class")]
  public virtual string CustomerClassID { get; set; }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null)]
  public virtual int? CustomerID { get; set; }

  [Contract(typeof (Where<PX.Objects.CT.Contract.customerID, Equal<Current<CaseFilter.customerID>>, Or<Current<CaseFilter.customerID>, IsNull>>), DisplayName = "Contract")]
  public virtual int? ContractID { get; set; }

  public abstract class caseClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CaseFilter.caseClassID>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CaseFilter.customerClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CaseFilter.customerID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CaseFilter.contractID>
  {
  }
}
