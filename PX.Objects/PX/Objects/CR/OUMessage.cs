// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class OUMessage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _From;
  protected string _To;
  protected string _CC;
  protected string _Subject;
  protected bool? _IsIncome;
  protected DateTime? _StartDate;

  [PXString]
  public virtual string MessageId { get; set; }

  [PXString]
  public virtual string ItemId { get; set; }

  [PXString]
  public virtual string EwsUrl { get; set; }

  [PXString]
  public virtual string Body { get; set; }

  [PXString]
  public virtual string AttachmentsInfo { get; set; }

  [PXString]
  public virtual string Token { get; set; }

  [PXBool]
  public virtual bool? IsOidcFirstRun { get; set; }

  [PXString]
  public virtual string OboToken { get; set; }

  [PXDateAndTime]
  public virtual DateTime? OboTokenExpiresOn { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string From
  {
    get => this._From;
    set => this._From = value;
  }

  [PXDBString(IsUnicode = true)]
  public virtual string To
  {
    get => this._To;
    set => this._To = value;
  }

  [PXDBString(IsUnicode = true)]
  public virtual string CC
  {
    get => this._CC;
    set => this._CC = value;
  }

  [PXDBString(IsUnicode = true)]
  public virtual string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [PXDBBool]
  public virtual bool? IsIncome { get; set; }

  [PXDateAndTime]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class messageId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.messageId>
  {
  }

  public abstract class itemId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.itemId>
  {
  }

  public abstract class ewsUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.ewsUrl>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.body>
  {
  }

  public abstract class attachmentsInfo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUMessage.attachmentsInfo>
  {
  }

  public abstract class token : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.token>
  {
  }

  public abstract class isOidcFirstRun : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OUMessage.isOidcFirstRun>
  {
  }

  public abstract class oboToken : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.oboToken>
  {
  }

  public abstract class oboTokenExpiresOn : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OUMessage.oboTokenExpiresOn>
  {
  }

  public abstract class from : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.from>
  {
  }

  public abstract class to : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.to>
  {
  }

  public abstract class cC : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.cC>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUMessage.subject>
  {
  }

  public abstract class isIncome : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OUMessage.isIncome>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  OUMessage.startDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OUMessage.noteID>
  {
  }
}
