// Decompiled with JetBrains decompiler
// Type: PX.SM.TraceItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (TraceMaint))]
[Serializable]
public class TraceItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _Source;
  protected System.DateTime? _RaiseDateTime;
  protected int? _EventType;
  protected string _Message;
  protected string _FullMessage;
  protected string _StackTrace;
  protected string _Details;

  [PXString(8, InputMask = "CC.CC.CC.CC")]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  public virtual string Source
  {
    get => this._Source;
    set => this._Source = value;
  }

  [PXDateAndTime(InputMask = "G")]
  [PXUIField(DisplayName = "Raised At", Enabled = false)]
  public virtual System.DateTime? RaiseDateTime
  {
    get => this._RaiseDateTime;
    set => this._RaiseDateTime = value;
  }

  [PXInt]
  [PXIntList(new int[] {1, 2, 4, 8, 16 /*0x10*/}, new string[] {"Critical", "Error", "Warning", "Information", "Verbose"})]
  [PXUIField(DisplayName = "Event Type")]
  public virtual int? EventType
  {
    get => this._EventType;
    set => this._EventType = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Message")]
  public virtual string Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "FullMessage")]
  public virtual string FullMessage
  {
    get => this._FullMessage;
    set => this._FullMessage = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Stack Trace")]
  public virtual string StackTrace
  {
    get => this._StackTrace;
    set => this._StackTrace = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  public virtual string Details
  {
    get => this._Details;
    set => this._Details = value;
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.screenID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.source>
  {
  }

  public abstract class raiseDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TraceItem.raiseDateTime>
  {
  }

  public abstract class eventType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TraceItem.eventType>
  {
  }

  public abstract class message : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.message>
  {
  }

  public abstract class fullmessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.fullmessage>
  {
  }

  public abstract class stackTrace : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.stackTrace>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceItem.details>
  {
  }
}
