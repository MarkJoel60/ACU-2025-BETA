// Decompiled with JetBrains decompiler
// Type: PX.SM.UserIdentity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXHidden]
[Serializable]
public class UserIdentity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "User ID")]
  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), CacheGlobal = true, DirtyRead = true)]
  public virtual Guid? UserID { get; set; }

  [PXUIField(DisplayName = "Instance Key", Enabled = false, Visible = false)]
  [PXDBString(10, IsKey = true, InputMask = "")]
  [PXDefault("Default")]
  public virtual 
  #nullable disable
  string InstanceKey { get; set; }

  [PXUIField(DisplayName = "Provider Name", Enabled = false)]
  [PXDBString(64 /*0x40*/, IsKey = true, InputMask = "")]
  public virtual string ProviderName { get; set; }

  [PXUIField(DisplayName = "Active")]
  [PXDBBool]
  public virtual bool? Active { get; set; }

  [PXUIField(DisplayName = "User Key")]
  [PXDBString]
  public virtual string UserKey { get; set; }

  [PXBool]
  public virtual bool? Databased { get; set; }

  [PXBool]
  public virtual bool? Enabled { get; set; }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserIdentity.userID>
  {
  }

  public abstract class instanceKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserIdentity.instanceKey>
  {
  }

  public abstract class providerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserIdentity.providerName>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UserIdentity.active>
  {
  }

  public abstract class userKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserIdentity.userKey>
  {
  }

  public abstract class databased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UserIdentity.databased>
  {
  }

  public abstract class enabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UserIdentity.enabled>
  {
  }
}
