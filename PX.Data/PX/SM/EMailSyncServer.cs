// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncServer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (EMailSyncServerMaint))]
[Serializable]
public class EMailSyncServer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Password;

  [PXDBIdentity]
  public virtual int? AccountID { get; set; }

  [PXDefault]
  [PXDBString(64 /*0x40*/, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Account Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<EMailSyncServer.accountCD>), new System.Type[] {typeof (EMailSyncServer.accountCD), typeof (EMailSyncServer.address), typeof (EMailSyncServer.defaultPolicyName)})]
  public virtual string AccountCD { get; set; }

  [PXDBString]
  [EmailSyncServerTypes]
  [PXDefault("E")]
  public virtual string ServerType { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "Username", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Address { get; set; }

  [PXRSACryptString]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Password", Required = true)]
  public virtual string Password
  {
    get => this._Password;
    set => this._Password = value;
  }

  [PXDefault(3)]
  [PXDBInt]
  [AuthenticationType.MethodList]
  [PXUIField(DisplayName = "Authentication Method")]
  public int? AuthenticationMethod { get; set; }

  [PXDBInt]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "External Application", Required = true)]
  public virtual int? OAuthApplicationID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Azure Tenant ID")]
  public string AzureTenantID { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "Mail Server (Optional)")]
  public virtual string ServerUrl { get; set; }

  [PXDBInt(MaxValue = 200, MinValue = 1)]
  [PXUIField(DisplayName = "Select Batch Size")]
  public virtual int? SyncSelectBatch { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Update Batch Size")]
  public virtual int? SyncUpdateBatch { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Accounts in Batch")]
  public virtual int? SyncProcBatch { get; set; }

  [PXDBInt(MaxValue = 1073741824 /*0x40000000*/, MinValue = 1)]
  [PXUIField(DisplayName = "Max Attachment Size, KB")]
  public virtual int? SyncAttachmentSize { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "Default Policy Name")]
  [PXSelector(typeof (EMailSyncPolicy.policyName), DescriptionField = typeof (EMailSyncPolicy.description))]
  public virtual string DefaultPolicyName { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Connection Mode", Visibility = PXUIVisibility.Invisible, Visible = false)]
  [PXStringList(new string[] {"D", "I"}, new string[] {"Delegation", "Impersonation"})]
  [PXDefault("D", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string ConnectionMode { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Logging Level", Visibility = PXUIVisibility.SelectorVisible)]
  [PXStringList(new string[] {"F", "I", "V", "N"}, new string[] {"Default", "Informational", "Verbose", "None"})]
  [PXDefault("F", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string LoggingLevel { get; set; }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncServer.accountID>
  {
  }

  public abstract class accountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncServer.accountCD>
  {
  }

  public abstract class serverType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncServer.serverType>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncServer.isActive>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncServer.address>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncServer.password>
  {
  }

  public abstract class authenticationMethod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncServer.authenticationMethod>
  {
  }

  public abstract class oAuthApplicationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncServer.oAuthApplicationID>
  {
  }

  public abstract class azureTenantID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncServer.azureTenantID>
  {
  }

  public abstract class serverUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncServer.serverUrl>
  {
  }

  public abstract class syncSelectBatch : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncServer.syncSelectBatch>
  {
  }

  public abstract class syncUpdateBatch : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncServer.syncUpdateBatch>
  {
  }

  public abstract class syncProcBatch : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncServer.syncProcBatch>
  {
  }

  public abstract class syncAttachmentSize : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncServer.syncAttachmentSize>
  {
  }

  public abstract class defaultPolicyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncServer.defaultPolicyName>
  {
  }

  public abstract class connectionMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncServer.connectionMode>
  {
  }

  public abstract class loggingLevel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncServer.loggingLevel>
  {
  }

  public class LogLevel
  {
    public const string Default = "F";
    public const string Informational = "I";
    public const string Verbose = "V";
    public const string None = "N";

    public class UI
    {
      public const string Default = "Default";
      public const string Informational = "Informational";
      public const string Verbose = "Verbose";
      public const string None = "None";
    }
  }
}
