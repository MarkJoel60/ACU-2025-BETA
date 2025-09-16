// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitSerialPart
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INKitSerialPart : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _KitLineNbr;
  protected int? _KitSplitLineNbr;
  protected int? _PartLineNbr;
  protected int? _PartSplitLineNbr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INRegister.docType))]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INRegister.refNbr))]
  [PXParent(typeof (INKitSerialPart.FK.Register))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? KitLineNbr
  {
    get => this._KitLineNbr;
    set => this._KitLineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? KitSplitLineNbr
  {
    get => this._KitSplitLineNbr;
    set => this._KitSplitLineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? PartLineNbr
  {
    get => this._PartLineNbr;
    set => this._PartLineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? PartSplitLineNbr
  {
    get => this._PartSplitLineNbr;
    set => this._PartSplitLineNbr = value;
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

  public class PK : 
    PrimaryKeyOf<INKitSerialPart>.By<INKitSerialPart.docType, INKitSerialPart.refNbr, INKitSerialPart.kitLineNbr, INKitSerialPart.kitSplitLineNbr, INKitSerialPart.partLineNbr, INKitSerialPart.partSplitLineNbr>
  {
    public static INKitSerialPart Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? kitLineNbr,
      int? kitSplitLineNbr,
      int? partLineNbr,
      int? partSplitLineNbr,
      PKFindOptions options = 0)
    {
      return (INKitSerialPart) PrimaryKeyOf<INKitSerialPart>.By<INKitSerialPart.docType, INKitSerialPart.refNbr, INKitSerialPart.kitLineNbr, INKitSerialPart.kitSplitLineNbr, INKitSerialPart.partLineNbr, INKitSerialPart.partSplitLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) kitLineNbr, (object) kitSplitLineNbr, (object) partLineNbr, (object) partSplitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Register : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INKitSerialPart>.By<INKitSerialPart.docType, INKitSerialPart.refNbr>
    {
    }

    public class TranSplit : 
      PrimaryKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.lineNbr, INTranSplit.splitLineNbr>.ForeignKeyOf<INKitSerialPart>.By<INKitSerialPart.docType, INKitSerialPart.refNbr, INKitSerialPart.kitLineNbr, INKitSerialPart.kitSplitLineNbr>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSerialPart.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSerialPart.refNbr>
  {
  }

  public abstract class kitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSerialPart.kitLineNbr>
  {
  }

  public abstract class kitSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INKitSerialPart.kitSplitLineNbr>
  {
  }

  public abstract class partLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSerialPart.partLineNbr>
  {
  }

  public abstract class partSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INKitSerialPart.partSplitLineNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitSerialPart.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitSerialPart.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSerialPart.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSerialPart.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitSerialPart.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSerialPart.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSerialPart.lastModifiedDateTime>
  {
  }
}
