// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CADepositType
{
  public class RefNbrAttribute(Type searchType) : PXSelectorAttribute(searchType, new Type[7]
  {
    typeof (CADeposit.refNbr),
    typeof (CADeposit.tranDate),
    typeof (CADeposit.finPeriodID),
    typeof (CADeposit.cashAccountID),
    typeof (CADeposit.curyID),
    typeof (CADeposit.curyTranAmt),
    typeof (CADeposit.extRefNbr)
  })
  {
  }

  /// <summary>
  /// Specialized for CADeposit version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AR Invoice. <br />
  /// References CADeposit.tranType and CADeposit.tranDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in CASetup (namely CASetup.registerNumberingID)<br />
  /// and CADeposit: <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (CADeposit.tranType), typeof (CADeposit.tranDate), new string[2]
      {
        "CDT",
        "CVD"
      }, new Type[2]
      {
        typeof (CASetup.registerNumberingID),
        null
      })
    {
    }
  }
}
