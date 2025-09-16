// Decompiled with JetBrains decompiler
// Type: PX.SM.TraceMessage
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
public class TraceMessage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _MailTo;
  protected string _Subject;
  protected string _Body;

  [PXString(64 /*0x40*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Address")]
  [PXDefault(typeof (Search<PreferencesEmail.supportEMailAccount>))]
  public virtual string MailTo
  {
    get => this._MailTo;
    set => this._MailTo = value;
  }

  [PXString(128 /*0x80*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Subject")]
  public virtual string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [PXString(500, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Body")]
  [PXDefault(typeof (Search<UserPreferences.mailSignature, Where<UserPreferences.userID, Equal<Current<AccessInfo.userID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string Body
  {
    get => this._Body;
    set => this._Body = value;
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceMessage.mailTo>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceMessage.subject>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TraceMessage.body>
  {
  }
}
