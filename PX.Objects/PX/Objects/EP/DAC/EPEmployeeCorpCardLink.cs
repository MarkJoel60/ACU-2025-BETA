// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DAC.EPEmployeeCorpCardLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;

#nullable enable
namespace PX.Objects.EP.DAC;

[PXCacheName("Employee Corporate Card Reference")]
public class EPEmployeeCorpCardLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXEPEmployeeSelector]
  [PXParent(typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<EPEmployeeCorpCardLink.employeeID>>>>))]
  [PXUIField(DisplayName = "Employee ID")]
  public int? EmployeeID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<CACorpCard, Where<CACorpCard.corpCardID, Equal<Current<EPEmployeeCorpCardLink.corpCardID>>>>))]
  [PXSelector(typeof (Search<CACorpCard.corpCardID>), new System.Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (CACorpCard.cashAccountID)}, SubstituteKey = typeof (CACorpCard.corpCardCD))]
  [PXUIField(DisplayName = "Corporate Card ID")]
  public int? CorpCardID { get; set; }

  public abstract class employeeID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeCorpCardLink.employeeID>
  {
  }

  public abstract class corpCardID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeCorpCardLink.corpCardID>
  {
  }
}
