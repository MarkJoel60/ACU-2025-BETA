// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.Utility.ExternalTransactionDetailMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.PaymentTransaction;
using System;

#nullable disable
namespace PX.Objects.CC.Utility;

public class ExternalTransactionDetailMapping : IBqlMapping
{
  public Type TransactionID = typeof (ExternalTransactionDetail.transactionID);
  public Type PMInstanceID = typeof (ExternalTransactionDetail.pMInstanceID);
  public Type DocType = typeof (ExternalTransactionDetail.docType);
  public Type RefNbr = typeof (ExternalTransactionDetail.refNbr);
  public Type OrigDocType = typeof (ExternalTransactionDetail.origDocType);
  public Type OrigRefNbr = typeof (ExternalTransactionDetail.origRefNbr);
  public Type VoidDocType = typeof (ExternalTransactionDetail.voidDocType);
  public Type VoidRefNbr = typeof (ExternalTransactionDetail.voidRefNbr);
  public Type TranNumber = typeof (ExternalTransactionDetail.tranNumber);
  public Type AuthNumber = typeof (ExternalTransactionDetail.authNumber);
  public Type Amount = typeof (ExternalTransactionDetail.amount);
  public Type ProcStatus = typeof (ExternalTransactionDetail.procStatus);
  public Type LastActivityDate = typeof (ExternalTransactionDetail.lastActivityDate);
  public Type Direction = typeof (ExternalTransactionDetail.direction);
  public Type Active = typeof (ExternalTransactionDetail.active);
  public Type Completed = typeof (ExternalTransactionDetail.completed);
  public Type ParentTranID = typeof (ExternalTransactionDetail.parentTranID);
  public Type ExpirationDate = typeof (ExternalTransactionDetail.expirationDate);
  public Type CVVVerification = typeof (ExternalTransactionDetail.cVVVerification);
  public Type NeedSync = typeof (ExternalTransactionDetail.needSync);
  public Type SaveProfile = typeof (ExternalTransactionDetail.saveProfile);
  public Type SyncStatus = typeof (ExternalTransactionDetail.syncStatus);
  public Type SyncMessage = typeof (ExternalTransactionDetail.syncMessage);
  public Type NoteID = typeof (ExternalTransactionDetail.noteID);

  public Type Extension => typeof (ExternalTransactionDetail);

  public Type Table { get; private set; }

  public ExternalTransactionDetailMapping(Type table) => this.Table = table;
}
