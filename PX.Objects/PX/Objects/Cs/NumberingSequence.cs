// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NumberingSequence
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Numbering Sequence Detail")]
[Serializable]
public class NumberingSequence : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _NumberingID;
  protected int? _NumberingSEQ;
  protected int? _NBranchID;
  protected string _StartNbr;
  protected string _EndNbr;
  protected DateTime? _StartDate;
  protected string _LastNbr;
  protected string _WarnNbr;
  protected int? _NbrStep;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (Numbering.numberingID))]
  [PXParent(typeof (Select<Numbering, Where<Numbering.numberingID, Equal<Current<NumberingSequence.numberingID>>>>))]
  public virtual string NumberingID
  {
    get => this._NumberingID;
    set => this._NumberingID = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Numbering Seq", Visible = false, Enabled = false)]
  public virtual int? NumberingSEQ
  {
    get => this._NumberingSEQ;
    set => this._NumberingSEQ = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? NBranchID
  {
    get => this._NBranchID;
    set => this._NBranchID = value;
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string StartNbr
  {
    get => this._StartNbr;
    set => this._StartNbr = value;
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string EndNbr
  {
    get => this._EndNbr;
    set => this._EndNbr = value;
  }

  [PXDefault]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string LastNbr
  {
    get => this._LastNbr;
    set => this._LastNbr = value;
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string WarnNbr
  {
    get => this._WarnNbr;
    set => this._WarnNbr = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXDefault(1)]
  public virtual int? NbrStep
  {
    get => this._NbrStep;
    set => this._NbrStep = value;
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

  public abstract class numberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NumberingSequence.numberingID>
  {
  }

  public abstract class numberingSEQ : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NumberingSequence.numberingSEQ>
  {
  }

  public abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NumberingSequence.nBranchID>
  {
  }

  public abstract class startNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NumberingSequence.startNbr>
  {
  }

  public abstract class endNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NumberingSequence.endNbr>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NumberingSequence.startDate>
  {
  }

  public abstract class lastNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NumberingSequence.lastNbr>
  {
  }

  public abstract class warnNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NumberingSequence.warnNbr>
  {
  }

  public abstract class nbrStep : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NumberingSequence.nbrStep>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  NumberingSequence.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NumberingSequence.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NumberingSequence.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NumberingSequence.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NumberingSequence.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NumberingSequence.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NumberingSequence.lastModifiedDateTime>
  {
  }
}
