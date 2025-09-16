// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectAttributeGroupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectAttributeGroupMaint : PXGraph<
#nullable disable
ProjectAttributeGroupMaint>
{
  public PXSave<ProjectAttributeGroupMaint.GroupTypeFilter> Save;
  public PXCancel<ProjectAttributeGroupMaint.GroupTypeFilter> Cancel;
  public PXFilter<ProjectAttributeGroupMaint.GroupTypeFilter> Filter;
  public FbqlSelect<SelectFromBase<CSAttributeGroup, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAttribute>.On<BqlOperand<
  #nullable enable
  CSAttribute.attributeID, IBqlString>.IsEqual<
  #nullable disable
  CSAttributeGroup.attributeID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CSAttributeGroup.entityClassID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ProjectAttributeGroupMaint.GroupTypeFilter.classID, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CSAttributeGroup.entityType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProjectAttributeGroupMaint.GroupTypeFilter.entityType, IBqlString>.FromCurrent>>>, 
  #nullable disable
  CSAttributeGroup>.View Mapping;

  public ProjectAttributeGroupMaint()
  {
    if (((PXGraph) this).Views.Caches.Contains(typeof (CSAnswers)))
      return;
    ((PXGraph) this).Views.Caches.Add(typeof (CSAnswers));
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    try
    {
      return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    }
    catch (PXException ex) when (((Exception) ex).InnerException is PXFieldProcessingException && ((Exception) ex).InnerException.InnerException is PXSetPropertyException)
    {
      throw ((Exception) ex).InnerException.InnerException;
    }
  }

  /// <summary>
  ///  This function just call to getEntityNameStatic because in another way, a infinty cicle was performed in the
  ///  called point of this method
  /// </summary>
  /// <param name="classid"></param>
  /// <returns></returns>
  public virtual string getEntityName(string classid)
  {
    return ProjectAttributeGroupMaint.getEntityNameStatic(classid);
  }

  /// <summary>
  /// This function get the originals entity names of the DACs listed in Attributes screen
  /// Without Service Management module enabled
  /// </summary>
  /// <param name="classid"></param>
  /// <returns></returns>
  public static string getEntityNameStatic(string classid)
  {
    switch (classid)
    {
      case "ACCGROUP":
        return typeof (PMAccountGroup).FullName;
      case "TASK":
        return typeof (PMTask).FullName;
      case "PROJECT":
        return typeof (PMProject).FullName;
      case "EQUIPMENT":
        return typeof (EPEquipment).FullName;
      default:
        return (string) null;
    }
  }

  protected virtual void GroupTypeFilter_EntityType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ProjectAttributeGroupMaint.GroupTypeFilter row = (ProjectAttributeGroupMaint.GroupTypeFilter) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) this.getEntityName(row.ClassID);
  }

  protected virtual void CSAttributeGroup_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CSAttributeGroup row = (CSAttributeGroup) e.Row;
    if (row == null)
      return;
    if (row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (((PXSelectBase<CSAttributeGroup>) this.Mapping).Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) != 1)
      ((CancelEventArgs) e).Cancel = true;
    else
      CSAttributeGroupList<CSAttributeGroup, CSAttributeGroup>.DeleteAttributesForGroup((PXGraph) this, row);
  }

  protected virtual void CSAttributeGroup_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is CSAttributeGroup row) || ((PXSelectBase<ProjectAttributeGroupMaint.GroupTypeFilter>) this.Filter).Current == null)
      return;
    row.EntityClassID = ((PXSelectBase<ProjectAttributeGroupMaint.GroupTypeFilter>) this.Filter).Current.ClassID;
    row.EntityType = ((PXSelectBase<ProjectAttributeGroupMaint.GroupTypeFilter>) this.Filter).Current.EntityType;
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class GroupTypeFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ClassID;

    [PXUIField]
    [PXDBString(20, IsUnicode = true)]
    [GroupTypes.List]
    [PXDefault("PROJECT")]
    public virtual string ClassID
    {
      get => this._ClassID;
      set => this._ClassID = value;
    }

    [PXString(200, IsUnicode = true)]
    [PXFormula(typeof (Default<ProjectAttributeGroupMaint.GroupTypeFilter.classID>))]
    public virtual string EntityType { get; set; }

    public abstract class classID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectAttributeGroupMaint.GroupTypeFilter.classID>
    {
    }

    public abstract class entityType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProjectAttributeGroupMaint.GroupTypeFilter.entityType>
    {
    }
  }
}
