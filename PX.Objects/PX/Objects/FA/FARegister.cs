// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FARegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (TransactionEntry))]
[PXCacheName("Fixed Asset Transaction")]
[Serializable]
public class FARegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _RefNbr;
  protected DateTime? _DocDate;
  protected string _FinPeriodID;
  protected string _DocDesc;
  protected int? _LineCntr;
  protected string _Status;
  protected string _Origin;
  protected bool? _Released;
  protected bool? _Hold;
  protected bool? _Posted;
  protected string _Reason;
  protected Guid? _NoteID;
  protected bool? _IsEmpty;
  protected Decimal? _TranAmt;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FARegister.refNbr>))]
  [FARegister.refNbr.Numbering]
  [PXFieldDescription]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  [PXUIField(DisplayName = "Period ID")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [FARegister.status.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [FARegister.origin.List]
  [PXUIField]
  public virtual string Origin
  {
    get => this._Origin;
    set => this._Origin = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "On Hold")]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted
  {
    get => this._Posted;
    set => this._Posted = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Reason", Enabled = false)]
  public virtual string Reason
  {
    get => this._Reason;
    set => this._Reason = value;
  }

  [PXSearchable(8, "{0}", new Type[] {typeof (FARegister.refNbr)}, new Type[] {typeof (FARegister.docDesc)}, NumberFields = new Type[] {typeof (FARegister.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new Type[] {typeof (FARegister.docDate), typeof (FARegister.status), typeof (FARegister.origin)}, Line2Format = "{0}", Line2Fields = new Type[] {typeof (FARegister.docDesc)})]
  [PXNote(DescriptionField = typeof (FARegister.refNbr), Selector = typeof (FARegister.refNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Empty")]
  [PXDBCalced(typeof (Switch<Case<Where<FARegister.lineCntr, Equal<short0>>, True>, False>), typeof (bool))]
  public virtual bool? IsEmpty
  {
    get => this._IsEmpty;
    set => this._IsEmpty = value;
  }

  [PXDecimal(2)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class Events : PXEntityEventBase<FARegister>.Container<FARegister.Events>
  {
    public PXEntityEvent<FARegister> ReleaseDocument;
  }

  public class PK : PrimaryKeyOf<FARegister>.By<FARegister.refNbr>
  {
    public static FARegister Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FARegister) PrimaryKeyOf<FARegister>.By<FARegister.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FARegister>.By<FARegister.branchID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FARegister.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FARegister.branchID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.refNbr>
  {
    public class NumberingAttribute : AutoNumberAttribute
    {
      public NumberingAttribute()
        : base(typeof (FASetup.registerNumberingID), typeof (FARegister.docDate))
      {
      }
    }
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FARegister.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.finPeriodID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.docDesc>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FARegister.lineCntr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.status>
  {
    public const string Hold = "H";
    public const string Balanced = "B";
    public const string Unposted = "U";
    public const string Posted = "P";
    public const string Completed = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[5]{ "H", "B", "U", "P", "C" }, new string[5]
        {
          "On Hold",
          "Balanced",
          "Unposted",
          "Posted",
          "Completed"
        })
      {
      }
    }

    public class hold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.status.hold>
    {
      public hold()
        : base("H")
      {
      }
    }

    public class balanced : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.status.balanced>
    {
      public balanced()
        : base("B")
      {
      }
    }

    public class unposted : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.status.unposted>
    {
      public unposted()
        : base("U")
      {
      }
    }

    public class posted : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.status.posted>
    {
      public posted()
        : base("P")
      {
      }
    }

    public class completed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.status.completed>
    {
      public completed()
        : base("C")
      {
      }
    }
  }

  public abstract class origin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.origin>
  {
    public const string Adjustment = "A";
    public const string Purchasing = "P";
    public const string Depreciation = "D";
    public const string Disposal = "S";
    public const string Transfer = "T";
    public const string Reconcilliation = "R";
    public const string Split = "I";
    public const string Reversal = "V";
    public const string DisposalReversal = "L";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[9]
        {
          "A",
          "P",
          "D",
          "S",
          "T",
          "R",
          "I",
          "V",
          "L"
        }, new string[9]
        {
          "Adjustment",
          "Purchasing",
          "Depreciation",
          "Disposal",
          "Transfer",
          "Reconcilliation",
          "Split",
          "Reversal",
          "Disposal Reversal"
        })
      {
      }
    }

    public class adjustment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.adjustment>
    {
      public adjustment()
        : base("A")
      {
      }
    }

    public class purchasing : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.purchasing>
    {
      public purchasing()
        : base("P")
      {
      }
    }

    public class depreciation : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.depreciation>
    {
      public depreciation()
        : base("D")
      {
      }
    }

    public class disposal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.disposal>
    {
      public disposal()
        : base("S")
      {
      }
    }

    public class transfer : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.transfer>
    {
      public transfer()
        : base("T")
      {
      }
    }

    public class reconcilliation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FARegister.origin.reconcilliation>
    {
      public reconcilliation()
        : base("R")
      {
      }
    }

    public class split : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.split>
    {
      public split()
        : base("I")
      {
      }
    }

    public class reversal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FARegister.origin.reversal>
    {
      public reversal()
        : base("V")
      {
      }
    }

    public class disposalReversal : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FARegister.origin.disposalReversal>
    {
      public disposalReversal()
        : base("L")
      {
      }
    }
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FARegister.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FARegister.hold>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FARegister.posted>
  {
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FARegister.reason>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FARegister.noteID>
  {
  }

  public abstract class isEmpty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FARegister.isEmpty>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FARegister.tranAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FARegister.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FARegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FARegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FARegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FARegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FARegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FARegister.lastModifiedDateTime>
  {
  }
}
