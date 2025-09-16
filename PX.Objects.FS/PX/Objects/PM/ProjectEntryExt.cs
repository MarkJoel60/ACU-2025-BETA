// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntryExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class ProjectEntryExt : PXGraphExtension<ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual List<string> ValidateProjectClosure(
    int? projectID,
    Func<int?, List<string>> baseMethod)
  {
    List<string> stringList = baseMethod(projectID);
    IEnumerable<FSServiceOrder> firstTableItems1 = PXSelectBase<FSServiceOrder, PXViewOf<FSServiceOrder>.BasedOn<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<FSServiceOrder.status, IBqlString>.IsNotIn<ListField.ServiceOrderStatus.closed, ListField.ServiceOrderStatus.canceled>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<FSServiceOrder, string>((Func<FSServiceOrder, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} service order related to the project is unprocessed.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<FSAppointment> firstTableItems2 = PXSelectBase<FSAppointment, PXViewOf<FSAppointment>.BasedOn<SelectFromBase<FSAppointment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<FSAppointment.status, IBqlString>.IsNotIn<ListField.AppointmentStatus.closed, ListField.AppointmentStatus.canceled, ListField.AppointmentStatus.billed>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<FSAppointment, string>((Func<FSAppointment, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} appointment related to the project is unprocessed.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<FSServiceContract> source = GraphHelper.RowCast<FSServiceContract>((IEnumerable) PXSelectBase<FSServiceContract, PXSelectReadonly<FSServiceContract, Where<FSServiceContract.projectID, Equal<Required<FSServiceContract.projectID>>, And<FSServiceContract.status, NotIn<Required<FSServiceContract.status>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) projectID,
      (object) new string[2]{ "E", "X" }
    }));
    stringList.AddRange(source.Select<FSServiceContract, string>((Func<FSServiceContract, string>) (x => PXMessages.LocalizeFormatNoPrefix(x.RecordType == "IRSC" ? "The project cannot be closed because the {0} route service contract related to the project is unprocessed." : "The project cannot be closed because the {0} service contract related to the project is unprocessed.", new object[1]
    {
      (object) x.RefNbr
    }))));
    return stringList;
  }
}
