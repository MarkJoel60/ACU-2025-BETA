// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDelayedQuery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class PXDelayedQuery
{
  public object[] currents;
  public object[] arguments;
  public PXView View;
  public int startRow;
  public int maxRows;
  public RestrictedFieldsSet restrictedFields;
  private bool readBranchRestricted;
  private string specificTable;
  private List<int> branches;
  private bool readDeleted;
  private bool readOnlyDeleted;
  private readonly bool readArchived;

  public PXDelayedQuery()
  {
    this.specificTable = PXDatabase.SpecificBranchTable;
    this.readBranchRestricted = PXDatabase.ReadBranchRestricted;
    this.branches = PXDatabase.BranchIDs;
    this.readDeleted = PXDatabase.ReadDeleted;
    this.readOnlyDeleted = PXDatabase.ReadOnlyDeleted;
    this.readArchived = PXDatabase.ReadThroughArchived;
  }

  public virtual List<object> GetRows(bool singleRow)
  {
    bool flag1 = false;
    string str = (string) null;
    List<int> intList = (List<int>) null;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    RestrictedFieldsSet restrictedFieldsSet = (RestrictedFieldsSet) null;
    try
    {
      flag1 = PXDatabase.ReadBranchRestricted;
      str = PXDatabase.SpecificBranchTable;
      intList = PXDatabase.BranchIDs;
      flag2 = PXDatabase.ReadDeleted;
      flag3 = PXDatabase.ReadOnlyDeleted;
      flag4 = PXDatabase.ReadThroughArchived;
      flag5 = PXDatabase.DelayedFieldScope;
      restrictedFieldsSet = this.View.RestrictedFields;
      PXDatabase.ReadBranchRestricted = this.readBranchRestricted;
      PXDatabase.BranchIDs = this.branches;
      PXDatabase.SpecificBranchTable = this.specificTable;
      PXDatabase.ReadDeleted = this.readDeleted;
      PXDatabase.ReadOnlyDeleted = this.readOnlyDeleted;
      PXDatabase.ReadThroughArchived = this.readArchived;
      if (this.restrictedFields != null)
      {
        this.View.RestrictedFields = this.restrictedFields;
        if (this.restrictedFields.Any())
          PXDatabase.DelayedFieldScope = true;
      }
      int startRow = this.startRow;
      int totalRows = 0;
      return this.View.Select(this.currents, this.arguments, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, singleRow ? 1 : this.maxRows, ref totalRows);
    }
    finally
    {
      PXDatabase.ReadBranchRestricted = flag1;
      PXDatabase.BranchIDs = intList;
      PXDatabase.SpecificBranchTable = str;
      PXDatabase.ReadDeleted = flag2;
      PXDatabase.ReadOnlyDeleted = flag3;
      PXDatabase.ReadThroughArchived = flag4;
      PXDatabase.DelayedFieldScope = flag5;
      if (this.restrictedFields != null)
        this.View.RestrictedFields = restrictedFieldsSet;
    }
  }
}
