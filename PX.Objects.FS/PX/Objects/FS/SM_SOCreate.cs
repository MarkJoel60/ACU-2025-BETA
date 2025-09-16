// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOCreate
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOCreate : PXGraphExtension<SOCreate>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<SOCreate.SOFixedDemand>) this.Base.FixedDemand).Join<LeftJoin<FSServiceOrder, On<FSServiceOrder.noteID, Equal<SOCreate.SOFixedDemand.refNoteID>>>>();
    ((PXSelectBase<SOCreate.SOFixedDemand>) this.Base.FixedDemand).WhereAnd<Where<FSServiceOrder.refNbr, IsNull>>();
  }

  protected virtual IEnumerable fixedDemand()
  {
    PXView pxView = new PXView((PXGraph) this.Base, false, ((PXSelectBase) this.Base.FixedDemand).View.BqlSelect);
    int num = 0;
    int startRow = PXView.StartRow;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }
}
