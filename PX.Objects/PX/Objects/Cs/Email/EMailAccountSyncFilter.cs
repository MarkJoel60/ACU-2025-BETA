// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.EMailAccountSyncFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS.Email;

[PXPrimaryGraph(typeof (EmailsSyncMaint))]
[PXHidden]
[Serializable]
public class EMailAccountSyncFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Exchange Server")]
  [PXSelector(typeof (Search<EMailSyncServer.accountID>), new Type[] {typeof (EMailSyncServer.accountCD), typeof (EMailSyncServer.address), typeof (EMailSyncServer.defaultPolicyName)}, SubstituteKey = typeof (EMailSyncServer.accountCD), ValidateValue = false)]
  public virtual int? ServerID { get; set; }

  [PXString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "Policy Name")]
  [PXSelector(typeof (Search<EMailSyncPolicy.policyName>), DescriptionField = typeof (EMailSyncPolicy.description), ValidateValue = false)]
  public virtual 
  #nullable disable
  string PolicyName { get; set; }

  public abstract class serverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccountSyncFilter.serverID>
  {
  }

  public abstract class policyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccountSyncFilter.policyName>
  {
  }
}
