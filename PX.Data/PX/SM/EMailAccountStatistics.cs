// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailAccountStatistics
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Email Account Statistics")]
public class EMailAccountStatistics : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Email Account ID")]
  [PXParent(typeof (EMailAccountStatistics.FK.EMailAccount))]
  public virtual int? EmailAccountID { get; set; }

  [PXDBDate(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Last Email Sent On", Visible = false)]
  public virtual System.DateTime? LastSendDateTime { get; set; }

  [PXDBDate(UseTimeZone = true, PreserveTime = true)]
  [PXUIField(DisplayName = "Last Email Received On")]
  public virtual System.DateTime? LastReceiveDateTime { get; set; }

  public class PK : PrimaryKeyOf<
  #nullable disable
  EMailAccountStatistics>.By<EMailAccountStatistics.emailAccountID>
  {
    public static EMailAccountStatistics Find(
      PXGraph graph,
      int? emailAccountID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<EMailAccountStatistics>.By<EMailAccountStatistics.emailAccountID>.FindBy(graph, (object) emailAccountID, options);
    }
  }

  public static class FK
  {
    public class EMailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<EMailAccountStatistics>.By<EMailAccountStatistics.emailAccountID>
    {
    }
  }

  public abstract class emailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccountStatistics.emailAccountID>
  {
  }

  public abstract class lastSendDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailAccountStatistics.lastSendDateTime>
  {
  }

  public abstract class lastReceiveDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailAccountStatistics.lastReceiveDateTime>
  {
  }
}
