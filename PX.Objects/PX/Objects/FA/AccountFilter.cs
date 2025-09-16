// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AccountFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class AccountFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _StartPeriodID;
  protected string _EndPeriodID;
  protected int? _BookID;
  protected int? _AccountID;
  protected int? _SubID;

  [PXDBInt]
  [PXSelector(typeof (Search2<FixedAsset.assetID, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Asset ID")]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [FABookPeriodExistingInGLSelector(null, null, typeof (AccountFilter.bookID), false, typeof (AccountFilter.assetID), typeof (AccessInfo.businessDate), null, null, null, null)]
  [PXUIField]
  public virtual string StartPeriodID
  {
    get => this._StartPeriodID;
    set => this._StartPeriodID = value;
  }

  [FABookPeriodExistingInGLSelector(null, null, typeof (AccountFilter.bookID), false, typeof (AccountFilter.assetID), typeof (AccessInfo.businessDate), null, null, null, null)]
  [PXUIField]
  public virtual string EndPeriodID
  {
    get => this._EndPeriodID;
    set => this._EndPeriodID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXDefault(typeof (Search<FABook.bookID, Where<FABook.updateGL, Equal<True>>>))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [Account(DisplayName = "Account", DescriptionField = typeof (Account.description))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (AccountFilter.accountID), DisplayName = "Subaccount")]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountFilter.assetID>
  {
  }

  public abstract class startPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountFilter.startPeriodID>
  {
  }

  public abstract class endPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountFilter.endPeriodID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountFilter.bookID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountFilter.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountFilter.subID>
  {
  }
}
