// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARIntegrityCheckFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARIntegrityCheckFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CustomerClassID;
  protected string _FinPeriodID;

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Customer Class")]
  public virtual string CustomerClassID
  {
    get => this._CustomerClassID;
    set => this._CustomerClassID = value;
  }

  [FinPeriodNonLockedSelector]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Document Balances")]
  public virtual bool? RecalcDocumentBalances { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Customer Balances by Released Document")]
  public virtual bool? RecalcCustomerBalancesReleased { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Customer Balances by Unreleased Document")]
  public virtual bool? RecalcCustomerBalancesUnreleased { get; set; }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARIntegrityCheckFilter.customerClassID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARIntegrityCheckFilter.finPeriodID>
  {
  }

  public abstract class recalcDocumentBalances : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARIntegrityCheckFilter.recalcDocumentBalances>
  {
  }

  public abstract class recalcCustomerBalancesReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARIntegrityCheckFilter.recalcCustomerBalancesReleased>
  {
  }

  public abstract class recalcCustomerBalancesUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARIntegrityCheckFilter.recalcCustomerBalancesUnreleased>
  {
  }
}
