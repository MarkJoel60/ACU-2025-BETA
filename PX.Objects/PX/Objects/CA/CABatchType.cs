// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CABatchType
{
  /// <summary>
  /// Specialized selector for CABatch RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// CABatch.batchNbr, CABatch.tranDate, CABatch.cashAccountID,
  /// CABatch.paymentMethodID, CABatch.curyDetailTotal, CABatch.extRefNbr
  /// <example>
  /// [CABatchType.RefNbr(typeof(Search/<CABatch.batchNbr />))]
  /// </example>
  /// </summary>
  public class RefNbrAttribute(Type searchType) : PXSelectorAttribute(searchType, new Type[6]
  {
    typeof (CABatch.batchNbr),
    typeof (CABatch.tranDate),
    typeof (CABatch.cashAccountID),
    typeof (CABatch.paymentMethodID),
    typeof (CABatch.curyDetailTotal),
    typeof (CABatch.extRefNbr)
  })
  {
  }

  /// <summary>
  /// Specialized for CABatch version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AR Invoice. <br />
  /// References CABatch.docType and CABatch.tranDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in CASetup (namely CASetup.cABatchNumberingID)<br />
  /// and CABatch: <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (CASetup.cABatchNumberingID), typeof (CABatch.tranDate))
    {
    }
  }
}
