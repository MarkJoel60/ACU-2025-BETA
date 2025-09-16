// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CAReconFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CARecon.reconNbr, Where<CARecon.cashAccountID, Equal<Optional<CARecon.cashAccountID>>, Or<Optional<CARecon.cashAccountID>, IsNull>>>), new Type[] {typeof (CARecon.reconNbr), typeof (CARecon.cashAccountID), typeof (CARecon.reconDate), typeof (CARecon.status)})]
  public virtual 
  #nullable disable
  string ReconNbr { get; set; }

  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.reconcile, Equal<boolTrue>, And<Match<Current<AccessInfo.userName>>>>>))]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  [PXDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  public abstract class reconNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAReconFilter.reconNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAReconFilter.cashAccountID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAReconFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAReconFilter.endDate>
  {
  }
}
