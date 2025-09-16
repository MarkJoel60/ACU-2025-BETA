// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesIdentityProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class PreferencesIdentityProvider : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Instance Key", Enabled = false, Visible = false)]
  [PXDBString(10, IsKey = true, InputMask = "")]
  public virtual 
  #nullable disable
  string InstanceKey { get; set; }

  [PXUIField(DisplayName = "Provider Name", Enabled = false)]
  [PXDBString(64 /*0x40*/, IsKey = true, InputMask = "")]
  public virtual string ProviderName { get; set; }

  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Active")]
  [PXDBBool]
  public virtual bool? Active { get; set; }

  [PXUIField(DisplayName = "Realm")]
  [PXDBString]
  public virtual string Realm { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Application ID")]
  public virtual string ApplicationID { get; set; }

  [PXRSACryptString]
  [PXUIField(DisplayName = "Application Secret")]
  public virtual string ApplicationSecret { get; set; }

  public abstract class instanceKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesIdentityProvider.instanceKey>
  {
  }

  public abstract class providerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesIdentityProvider.providerName>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesIdentityProvider.active>
  {
  }

  public abstract class realm : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesIdentityProvider.realm>
  {
  }

  public abstract class applicationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesIdentityProvider.applicationID>
  {
  }

  public abstract class applicationSecret : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesIdentityProvider.applicationSecret>
  {
  }
}
