// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class EMailSyncReference : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [EmailAccountRaw(DisplayName = "Server ID", IsKey = true)]
  [PXDBDefault(typeof (EMailAccount.emailAccountID))]
  public virtual int? ServerID { get; set; }

  [PXDBString(100, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Email Address", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string Address { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBBinaryString]
  public virtual string BinaryReference { get; set; }

  [PXDBBinaryString]
  public virtual string BinaryChangeKey { get; set; }

  [PXDBString]
  public virtual string Conversation { get; set; }

  [PXDBString]
  public virtual string Hash { get; set; }

  [PXDBBool]
  public virtual bool? IsSynchronized { get; set; }

  public abstract class serverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncReference.serverID>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncReference.address>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EMailSyncReference.noteID>
  {
  }

  public abstract class binaryReference : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncReference.binaryReference>
  {
  }

  public abstract class binaryChangeKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncReference.binaryChangeKey>
  {
  }

  public abstract class conversation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncReference.conversation>
  {
  }

  public abstract class hash : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncReference.hash>
  {
  }

  public abstract class isSynchronized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncReference.isSynchronized>
  {
  }
}
