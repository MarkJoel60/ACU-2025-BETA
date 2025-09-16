// Decompiled with JetBrains decompiler
// Type: PX.SM.UPSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (InstallationSetup))]
[Serializable]
public class UPSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _UpdateEnabled;
  protected 
  #nullable disable
  string _UpdateServer;
  protected string _UpdateServerAlternative;
  protected bool? _UpdateAlternativeEnabled;
  protected bool? _UpdateNotification;
  protected string _StorageProvider;
  protected string _LicensingServer;
  protected string _ISVUpdateEndpoint;

  [PXDBBool]
  [PXUIField(DisplayName = "Use Update Server")]
  public virtual bool? UpdateEnabled
  {
    get => this._UpdateEnabled;
    set => this._UpdateEnabled = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Update Server Address")]
  public virtual string UpdateServer
  {
    get => this._UpdateServer;
    set => this._UpdateServer = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Alternative Update Server Address")]
  public virtual string UpdateServerAlternative
  {
    get => this._UpdateServerAlternative;
    set => this._UpdateServerAlternative = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Alternative Update Server")]
  public virtual bool? UpdateAlternativeEnabled
  {
    get => this._UpdateAlternativeEnabled;
    set => this._UpdateAlternativeEnabled = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Check for Updates")]
  public virtual bool? UpdateNotification
  {
    get => this._UpdateNotification;
    set => this._UpdateNotification = value;
  }

  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Storage Provider")]
  [StorageProviderSelector]
  public virtual string StorageProvider
  {
    get => this._StorageProvider;
    set => this._StorageProvider = value;
  }

  [PXDBString(255 /*0xFF*/)]
  public virtual string LicensingServer
  {
    get => this._LicensingServer;
    set => this._LicensingServer = value;
  }

  [PXDBString(255 /*0xFF*/)]
  public virtual string ISVUpdateEndpoint
  {
    get => this._ISVUpdateEndpoint;
    set => this._ISVUpdateEndpoint = value;
  }

  public abstract class updateEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPSetup.updateEnabled>
  {
  }

  public abstract class updateServer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSetup.updateServer>
  {
  }

  public abstract class updateServerAlternative : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPSetup.updateServerAlternative>
  {
  }

  public abstract class updateAlternativeEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UPSetup.updateAlternativeEnabled>
  {
  }

  public abstract class updateNotification : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UPSetup.updateNotification>
  {
  }

  public abstract class storageProvider : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSetup.storageProvider>
  {
  }

  public abstract class licensingServer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSetup.licensingServer>
  {
  }

  public abstract class iSVUpdateEndpoint : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPSetup.iSVUpdateEndpoint>
  {
  }
}
