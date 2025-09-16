// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAMatchProcessContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CA;

public class CAMatchProcessContext : IDisposable
{
  private int? CashAccountID { get; set; }

  public CAMatchProcessContext(CABankMatchingProcess graph, int? cashAccountID, Guid? processorID)
  {
    this.CashAccountID = cashAccountID;
    CAMatchProcessContext.VerifyRunningProcess(graph, cashAccountID);
    CAMatchProcessContext.InsertMatchInfo(processorID, cashAccountID);
  }

  private static void VerifyRunningProcess(CABankMatchingProcess graph, int? cashAccountID)
  {
    CAMatchProcess caMatchProcess = ((PXSelectBase<CAMatchProcess>) graph.MatchProcessSelect).SelectSingle(new object[1]
    {
      (object) cashAccountID
    });
    if ((caMatchProcess != null ? (!caMatchProcess.CashAccountID.HasValue ? 1 : 0) : 1) != 0)
      return;
    if (PXLongOperation.GetStatus((object) caMatchProcess.ProcessUID.GetValueOrDefault()) == 1)
      throw new CAMatchProcessContext.CashAccountLockedException("The {0} cash account is under the matching process.", new object[1]
      {
        (object) ((PXSelectBase<CashAccount>) graph.cashAccount).SelectSingle(new object[1]
        {
          (object) cashAccountID
        }).CashAccountCD
      });
    CAMatchProcessContext.DeleteMatchInfo(cashAccountID);
  }

  private static void InsertMatchInfo(Guid? processUID, int? cashAccountID)
  {
    PXDatabase.Insert<CAMatchProcess>(new PXDataFieldAssign[4]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<CAMatchProcess.processUID>((object) processUID),
      (PXDataFieldAssign) new PXDataFieldAssign<CAMatchProcess.cashAccountID>((object) cashAccountID),
      (PXDataFieldAssign) new PXDataFieldAssign<CAMatchProcess.operationStartDate>((object) PXTimeZoneInfo.Now),
      (PXDataFieldAssign) new PXDataFieldAssign<CAMatchProcess.startedByID>((object) PXAccess.GetUserID())
    });
  }

  private static void DeleteMatchInfo(int? cashAccountID)
  {
    PXDatabase.Delete<CAMatchProcess>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<CAMatchProcess.cashAccountID>((PXDbType) 8, (object) cashAccountID)
    });
  }

  public void Dispose() => CAMatchProcessContext.DeleteMatchInfo(this.CashAccountID);

  public class CashAccountLockedException : PXException
  {
    public CashAccountLockedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public CashAccountLockedException(string message, params object[] prms)
      : base(message, prms)
    {
    }
  }
}
