// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubAccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GL;

public class SubAccountMaint : PXGraph<SubAccountMaint>
{
  public PXSavePerRow<Sub, Sub.subID> Save;
  public PXCancel<Sub> Cancel;
  [PXImport(typeof (Sub))]
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<Sub, OrderBy<Asc<Sub.subCD>>> SubRecords;
  public PXSetup<Branch> Company;
  public PXAction<Sub> viewRestrictionGroups;

  public SubAccountMaint()
  {
    ((PXAction) this.viewRestrictionGroups).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.rowLevelSecurity>());
    if (!((PXSelectBase<Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public static int? FindSubIDByCD(PXGraph graph, string subCD) => Sub.UK.Find(graph, subCD)?.SubID;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<Sub>) this.SubRecords).Current != null)
    {
      GLAccessBySub instance = PXGraph.CreateInstance<GLAccessBySub>();
      ((PXSelectBase<Sub>) instance.Sub).Current = PXResultset<Sub>.op_Implicit(((PXSelectBase<Sub>) instance.Sub).Search<Sub.subCD>((object) ((PXSelectBase<Sub>) this.SubRecords).Current.SubCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }
}
