// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (SetupMaint))]
[PXCacheName("Fixed Assets Preferences")]
[Serializable]
public class FASetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RegisterNumberingID;
  protected string _AssetNumberingID;
  protected string _BatchNumberingID;
  protected string _TagNumberingID;
  protected bool? _CopyTagFromAssetID;
  protected int? _FAAccrualAcctID;
  protected int? _FAAccrualSubID;
  protected int? _ProceedsAcctID;
  protected int? _ProceedsSubID;
  protected bool? _AutoPost;
  protected bool? _AutoReleaseAsset;
  protected bool? _AutoReleaseDepr;
  protected bool? _AutoReleaseDisp;
  protected bool? _AutoReleaseTransfer;
  protected bool? _AutoReleaseReversal;
  protected bool? _AutoReleaseSplit;
  protected bool? _UpdateGL;
  protected string _DeprHistoryView;
  protected bool? _SummPost;
  protected bool? _SummPostDepreciation;
  protected bool? _DepreciateInDisposalPeriod;
  protected bool? _AccurateDepreciation;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("FAREGISTER")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string RegisterNumberingID
  {
    get => this._RegisterNumberingID;
    set => this._RegisterNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("FASSET")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string AssetNumberingID
  {
    get => this._AssetNumberingID;
    set => this._AssetNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXUIField(DisplayName = "Batch Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tag Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string TagNumberingID
  {
    get => this._TagNumberingID;
    set => this._TagNumberingID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Tag Number from Asset ID")]
  public virtual bool? CopyTagFromAssetID
  {
    get => this._CopyTagFromAssetID;
    set => this._CopyTagFromAssetID = value;
  }

  [PXDefault]
  [Account]
  public virtual int? FAAccrualAcctID
  {
    get => this._FAAccrualAcctID;
    set => this._FAAccrualAcctID = value;
  }

  [PXDefault]
  [SubAccount(typeof (FASetup.fAAccrualAcctID))]
  public virtual int? FAAccrualSubID
  {
    get => this._FAAccrualSubID;
    set => this._FAAccrualSubID = value;
  }

  [Account(null, DisplayName = "Proceeds Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? ProceedsAcctID
  {
    get => this._ProceedsAcctID;
    set => this._ProceedsAcctID = value;
  }

  [SubAccount(typeof (FASetup.proceedsAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Proceeds Subaccount")]
  public virtual int? ProceedsSubID
  {
    get => this._ProceedsSubID;
    set => this._ProceedsSubID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Post on Release")]
  [PXDefault(true)]
  public virtual bool? AutoPost
  {
    get => this._AutoPost;
    set => this._AutoPost = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Acquisition Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseAsset
  {
    get => this._AutoReleaseAsset;
    set => this._AutoReleaseAsset = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Depreciation Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseDepr
  {
    get => this._AutoReleaseDepr;
    set => this._AutoReleaseDepr = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Disposal Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseDisp
  {
    get => this._AutoReleaseDisp;
    set => this._AutoReleaseDisp = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Transfer Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseTransfer
  {
    get => this._AutoReleaseTransfer;
    set => this._AutoReleaseTransfer = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Reversal Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseReversal
  {
    get => this._AutoReleaseReversal;
    set => this._AutoReleaseReversal = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Release Split Transactions")]
  [PXDefault(false)]
  public virtual bool? AutoReleaseSplit
  {
    get => this._AutoReleaseSplit;
    set => this._AutoReleaseSplit = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Update GL")]
  [PXDefault(false)]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Depreciation History View")]
  [FASetup.deprHistoryView.List]
  [PXDefault("B")]
  public virtual string DeprHistoryView
  {
    get => this._DeprHistoryView;
    set => this._DeprHistoryView = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? ShowSideBySide
  {
    get => new bool?(this.DeprHistoryView == "S");
    set
    {
    }
  }

  [PXBool]
  [PXUIField]
  public virtual bool? ShowBookSheet
  {
    get => new bool?(this.DeprHistoryView == "B");
    set
    {
    }
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Post Summary on Updating GL")]
  [PXDefault(false)]
  public virtual bool? SummPost
  {
    get => this._SummPost;
    set => this._SummPost = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Post Depreciation Summary on Updating GL")]
  [PXDefault]
  [PXFormula(typeof (FASetup.summPost))]
  [PXUIEnabled(typeof (Where<FASetup.summPost, NotEqual<True>>))]
  public virtual bool? SummPostDepreciation
  {
    get => this._SummPostDepreciation;
    set => this._SummPostDepreciation = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Depreciate in Disposal Period")]
  [PXDefault(true)]
  public virtual bool? DepreciateInDisposalPeriod
  {
    get => this._DepreciateInDisposalPeriod;
    set => this._DepreciateInDisposalPeriod = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Accurate Depreciation")]
  [PXDefault(false)]
  public virtual bool? AccurateDepreciation
  {
    get => this._AccurateDepreciation;
    set => this._AccurateDepreciation = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Require Full Reconciliation before Disposal")]
  [PXDefault(true)]
  public virtual bool? ReconcileBeforeDisposal { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Allow to Modify Predefined Depreciation Methods")]
  [PXDefault(false)]
  public virtual bool? AllowEditPredefinedDeprMethod { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public static class FK
  {
    public class FAAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FASetup>.By<FASetup.fAAccrualAcctID>
    {
    }

    public class FAAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FASetup>.By<FASetup.fAAccrualSubID>
    {
    }

    public class ProceedsAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FASetup>.By<FASetup.proceedsAcctID>
    {
    }

    public class ProceedsSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FASetup>.By<FASetup.proceedsSubID>
    {
    }
  }

  public abstract class registerNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FASetup.registerNumberingID>
  {
  }

  public abstract class assetNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FASetup.assetNumberingID>
  {
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FASetup.batchNumberingID>
  {
  }

  public abstract class tagNumberingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FASetup.tagNumberingID>
  {
  }

  public abstract class copyTagFromAssetID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.copyTagFromAssetID>
  {
  }

  public abstract class fAAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FASetup.fAAccrualAcctID>
  {
  }

  public abstract class fAAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FASetup.fAAccrualSubID>
  {
  }

  public abstract class proceedsAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FASetup.proceedsAcctID>
  {
  }

  public abstract class proceedsSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FASetup.proceedsSubID>
  {
  }

  public abstract class autoPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.autoPost>
  {
  }

  public abstract class autoReleaseAsset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.autoReleaseAsset>
  {
  }

  public abstract class autoReleaseDepr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.autoReleaseDepr>
  {
  }

  public abstract class autoReleaseDisp : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.autoReleaseDisp>
  {
  }

  public abstract class autoReleaseTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.autoReleaseTransfer>
  {
  }

  public abstract class autoReleaseReversal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.autoReleaseReversal>
  {
  }

  public abstract class autoReleaseSplit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.autoReleaseSplit>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.updateGL>
  {
  }

  public abstract class deprHistoryView : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FASetup.deprHistoryView>
  {
    public const string SideBySide = "S";
    public const string BookSheet = "B";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "S", "B" }, new string[2]
        {
          "Side by Side",
          "By Book"
        })
      {
      }
    }

    public class sideBySide : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FASetup.deprHistoryView.sideBySide>
    {
      public sideBySide()
        : base("S")
      {
      }
    }

    public class bookSheet : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FASetup.deprHistoryView.bookSheet>
    {
      public bookSheet()
        : base("B")
      {
      }
    }
  }

  public abstract class showSideBySide : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.showSideBySide>
  {
  }

  public abstract class showBookSheet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.showBookSheet>
  {
  }

  public abstract class summPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FASetup.summPost>
  {
  }

  public abstract class summPostDepreciation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.summPostDepreciation>
  {
  }

  public abstract class depreciateInDisposalPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.depreciateInDisposalPeriod>
  {
  }

  public abstract class accurateDepreciation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.accurateDepreciation>
  {
  }

  public abstract class reconcileBeforeDisposal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.reconcileBeforeDisposal>
  {
  }

  public abstract class allowEditPredefinedDeprMethod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FASetup.allowEditPredefinedDeprMethod>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FASetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FASetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FASetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FASetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FASetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FASetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FASetup.Tstamp>
  {
  }
}
