// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.EmployeeDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class EmployeeDataProvider : IEmployeeDataProvider
{
  public EPEmployee GetEmployee(PXGraph graph, int? contactID)
  {
    return PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXViewOf<EPEmployee>.BasedOn<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPEmployee.defContactID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) contactID
    }));
  }
}
