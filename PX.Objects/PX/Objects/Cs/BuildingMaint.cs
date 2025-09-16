// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.BuildingMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CS;

public class BuildingMaint : PXGraph<
#nullable disable
BuildingMaint>
{
  public PXSave<BuildingMaint.BuildingFilter> Save;
  public PXCancel<BuildingMaint.BuildingFilter> Cancel;
  public PXCopyPasteAction<BuildingMaint.BuildingFilter> CopyPaste;
  public PXFilter<BuildingMaint.BuildingFilter> filter;
  public PXSelect<Building, Where<Building.branchID, Equal<Current<BuildingMaint.BuildingFilter.branchID>>>> building;

  public virtual bool CanClipboardCopyPaste() => true;

  [Serializable]
  public class BuildingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDBField = false)]
    public int? BranchID { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BuildingMaint.BuildingFilter.branchID>
    {
    }
  }
}
