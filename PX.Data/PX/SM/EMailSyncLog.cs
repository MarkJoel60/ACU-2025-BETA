// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncLog
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
public class EMailSyncLog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? EventID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "ServerID", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<EMailSyncServer.accountID>), new System.Type[] {typeof (EMailSyncServer.accountCD), typeof (EMailSyncServer.address), typeof (EMailSyncServer.defaultPolicyName)}, SubstituteKey = typeof (EMailSyncServer.accountCD), DirtyRead = true)]
  public virtual int? ServerID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Email Address", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string Address { get; set; }

  [PXDBByte]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 0}, new string[] {"Critical", "Error", "Warning", "Informational", "Verbose", "None"})]
  [PXUIField(DisplayName = "Level", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual byte? Level { get; set; }

  [PXDBDateAndTime(InputMask = "", DisplayMask = "g", UseSmallDateTime = false, UseTimeZone = true)]
  [PXUIField(DisplayName = "Operation Date")]
  public virtual System.DateTime? Date { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Message", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Message { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Details", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Details { get; set; }

  public abstract class eventID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncLog.eventID>
  {
  }

  public abstract class serverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncLog.serverID>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncLog.address>
  {
  }

  public abstract class level : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  EMailSyncLog.level>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  EMailSyncLog.date>
  {
  }

  public abstract class message : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncLog.message>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncLog.details>
  {
  }
}
