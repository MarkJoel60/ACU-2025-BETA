// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAMatchProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
public class CAMatchProcess : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? ProcessUID { get; set; }

  [PXDBDate(PreserveTime = true)]
  public virtual DateTime? OperationStartDate { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? StartedByID { get; set; }

  public class PK : PrimaryKeyOf<
  #nullable disable
  CAMatchProcess>.By<CAMatchProcess.cashAccountID>
  {
    public static CAMatchProcess Find(PXGraph graph, int? cashAccount, PKFindOptions options = 0)
    {
      return (CAMatchProcess) PrimaryKeyOf<CAMatchProcess>.By<CAMatchProcess.cashAccountID>.FindBy(graph, (object) cashAccount, options);
    }
  }

  public static class FK
  {
    public class CashAcccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankTran>.By<CAMatchProcess.cashAccountID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAMatchProcess.cashAccountID>
  {
  }

  public abstract class processUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAMatchProcess.processUID>
  {
  }

  public abstract class operationStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAMatchProcess.operationStartDate>
  {
  }

  public abstract class startedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAMatchProcess.startedByID>
  {
  }
}
