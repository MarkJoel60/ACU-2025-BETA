// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMDataSourcePM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class RMDataSourcePM : PXCacheExtension<
#nullable disable
RMDataSource>
{
  protected string _StartAccountGroup;
  protected string _startProject;
  protected string _StartProjectTask;
  protected string _StartInventory;
  protected string _EndAccountGroup;
  protected string _endProject;
  protected string _EndProjectTask;
  protected string _EndInventory;

  public static bool IsActive() => true;

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Account Group")]
  [PXSelector(typeof (PMAccountGroup.groupCD))]
  public virtual string StartAccountGroup
  {
    get => this._StartAccountGroup;
    set => this._StartAccountGroup = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Project")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>>.And<MatchUserFor<PMProject>>>>, PMProject>.SearchFor<PMProject.contractCD>), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.status)})]
  public virtual string StartProject
  {
    get => this._startProject;
    set => this._startProject = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Task")]
  [PXSelector(typeof (PMTask.taskCD), new Type[] {typeof (PMTask.taskCD), typeof (PMTask.projectID), typeof (PMTask.description)})]
  public virtual string StartProjectTask
  {
    get => this._StartProjectTask;
    set => this._StartProjectTask = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Inventory")]
  [PXSelector(typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual string StartInventory
  {
    get => this._StartInventory;
    set => this._StartInventory = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "End Account Group")]
  [PXSelector(typeof (PMAccountGroup.groupCD))]
  public virtual string EndAccountGroup
  {
    get => this._EndAccountGroup;
    set => this._EndAccountGroup = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "End Project")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>>.And<MatchUserFor<PMProject>>>>, PMProject>.SearchFor<PMProject.contractCD>), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.status)})]
  public virtual string EndProject
  {
    get => this._endProject;
    set => this._endProject = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "End Task")]
  [PXSelector(typeof (PMTask.taskCD), new Type[] {typeof (PMTask.taskCD), typeof (PMTask.projectID), typeof (PMTask.description)})]
  public virtual string EndProjectTask
  {
    get => this._EndProjectTask;
    set => this._EndProjectTask = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "End Inventory")]
  [PXSelector(typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual string EndInventory
  {
    get => this._EndInventory;
    set => this._EndInventory = value;
  }

  public abstract class startAccountGroup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourcePM.startAccountGroup>
  {
  }

  public abstract class startProject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourcePM.startProject>
  {
  }

  public abstract class startProjectTask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourcePM.startProjectTask>
  {
  }

  public abstract class startInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourcePM.startInventory>
  {
  }

  public abstract class endAccountGroup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourcePM.endAccountGroup>
  {
  }

  public abstract class endProject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourcePM.endProject>
  {
  }

  public abstract class endProjectTask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourcePM.endProjectTask>
  {
  }

  public abstract class endInventory : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourcePM.endInventory>
  {
  }
}
