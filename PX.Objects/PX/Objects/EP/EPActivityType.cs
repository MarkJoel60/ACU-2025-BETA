// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Activity Type")]
[PXPrimaryGraph(typeof (CRActivitySetupMaint))]
[Serializable]
public class EPActivityType : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ActivityService.IActivityType
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _LastModifiedByID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(5, IsFixed = true, IsKey = true, InputMask = ">AAAAA", IsUnicode = false)]
  [PXDefault]
  [PXUIField(DisplayName = "Type ID")]
  public virtual string Type { get; set; }

  [PXDBLocalizableString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  [PXDefault]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "System Default")]
  public virtual bool? IsDefault { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Image")]
  [PXIconsList]
  public virtual string ImageUrl { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Originated By")]
  [PXActivityApplication]
  [PXDefault(1)]
  public virtual int? Application { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<EPActivityType.application, Equal<PXActivityApplicationAttribute.portal>>, False>, EPActivityType.privateByDefault>))]
  [PXUIEnabled(typeof (BqlOperand<EPActivityType.application, IBqlInt>.IsNotEqual<PXActivityApplicationAttribute.portal>))]
  [PXUIField(DisplayName = "Internal")]
  public virtual bool? PrivateByDefault { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Track Time and Costs")]
  [PXFormula(typeof (Switch<Case<Where<EPActivityType.application, Equal<PXActivityApplicationAttribute.portal>>, False>, EPActivityType.requireTimeByDefault>))]
  [PXUIEnabled(typeof (BqlOperand<EPActivityType.application, IBqlInt>.IsEqual<PXActivityApplicationAttribute.backend>))]
  public virtual bool? RequireTimeByDefault { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Incoming")]
  public virtual bool? Incoming { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Outgoing")]
  public virtual bool? Outgoing { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Activity", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBInt]
  [CRActivityClass]
  [PXDefault(typeof (CRActivityClass.activity))]
  [PXUIField]
  [PXFieldDescription]
  public virtual int? ClassID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Is System")]
  [PXDBCalced(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.classID, In3<CRActivityClass.task, CRActivityClass.email, CRActivityClass.events>>>>>.Or<BqlOperand<EPActivityType.application, IBqlInt>.IsEqual<PXActivityApplicationAttribute.system>>, True, False>), typeof (bool))]
  public virtual bool? IsSystem { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPActivityType>.By<EPActivityType.type>
  {
    public static EPActivityType Find(PXGraph graph, string type, PKFindOptions options = 0)
    {
      return (EPActivityType) PrimaryKeyOf<EPActivityType>.By<EPActivityType.type>.FindBy(graph, (object) type, options);
    }
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPActivityType.type>
  {
    public const string SystemMessage = "ASI";
    public const string SystemEmail = "ASE";
    public const string Email = "AE";

    public class systemMessage : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPActivityType.type.systemMessage>
    {
      public systemMessage()
        : base("ASI")
      {
      }
    }

    public class systemEmail : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPActivityType.type.systemEmail>
    {
      public systemEmail()
        : base("ASE")
      {
      }
    }

    public class email : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPActivityType.type.email>
    {
      public email()
        : base("AE")
      {
      }
    }
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPActivityType.description>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityType.active>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityType.isDefault>
  {
  }

  public abstract class imageUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPActivityType.imageUrl>
  {
  }

  public abstract class application : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityType.application>
  {
  }

  public abstract class privateByDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPActivityType.privateByDefault>
  {
  }

  public abstract class requireTimeByDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPActivityType.requireTimeByDefault>
  {
  }

  public abstract class incoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityType.incoming>
  {
  }

  public abstract class outgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityType.outgoing>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPActivityType.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPActivityType.Tstamp>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPActivityType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPActivityType.lastModifiedDateTime>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityType.classID>
  {
  }

  public abstract class isSystem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityType.isSystem>
  {
  }
}
