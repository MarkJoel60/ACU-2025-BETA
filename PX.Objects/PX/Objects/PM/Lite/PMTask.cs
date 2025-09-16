// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Lite.PMTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Lite;

/// <exclude />
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTask : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectTaskAccountsSource,
  IProjectAccountsSource
{
  protected int? _ProjectID;
  protected int? _TaskID;
  protected 
  #nullable disable
  string _TaskCD;
  protected int? _DefaultAccrualAccountID;
  protected int? _DefaultAccrualSubID;
  protected int? _DefaultBranchID;

  [Project(DisplayName = "Project ID", IsKey = true, DirtyRead = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  [PXDefault(typeof (PMProject.defaultSalesAccountID))]
  [Account(DisplayName = "Default Sales Account", AvoidControlAccounts = true)]
  public virtual int? DefaultSalesAccountID { get; set; }

  [PXDefault(typeof (PMProject.defaultSalesSubID))]
  [SubAccount(typeof (PMTask.defaultSalesAccountID), typeof (PMTask.defaultBranchID), false)]
  public virtual int? DefaultSalesSubID { get; set; }

  [PXDefault(typeof (PMProject.defaultExpenseAccountID))]
  [Account(DisplayName = "Default Cost Account", AvoidControlAccounts = true)]
  public virtual int? DefaultExpenseAccountID { get; set; }

  [PXDefault(typeof (PMProject.defaultExpenseSubID))]
  [SubAccount(typeof (PMTask.defaultExpenseAccountID), typeof (PMTask.defaultBranchID), false)]
  public virtual int? DefaultExpenseSubID { get; set; }

  [PXDefault(typeof (PMProject.defaultAccrualAccountID))]
  [Account(DisplayName = "Accrual Account", AvoidControlAccounts = true)]
  public virtual int? DefaultAccrualAccountID
  {
    get => this._DefaultAccrualAccountID;
    set => this._DefaultAccrualAccountID = value;
  }

  [PXDefault(typeof (PMProject.defaultAccrualSubID))]
  [SubAccount]
  public virtual int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  [Branch(null, null, true, true, false)]
  [PXDefault(typeof (Search<PMProject.defaultBranchID, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  public virtual int? DefaultBranchID
  {
    get => this._DefaultBranchID;
    set => this._DefaultBranchID = value;
  }

  public class PK : PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>
  {
    public static PMTask Find(PXGraph graph, int? projectID, int? taskID, PKFindOptions options = 0)
    {
      return (PMTask) PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.FindBy(graph, (object) projectID, (object) taskID, options);
    }

    public static PMTask FindDirty(PXGraph graph, int? projectID, int? taskID)
    {
      return PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) projectID,
        (object) taskID
      }));
    }
  }

  public class UK : PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskCD>
  {
    public static PMTask Find(PXGraph graph, int? projectID, string taskCD, PKFindOptions options = 0)
    {
      return (PMTask) PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskCD>.FindBy(graph, (object) projectID, (object) taskCD, options);
    }
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.taskID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.taskCD>
  {
  }

  public abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultSalesAccountID>
  {
  }

  public abstract class defaultSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultSalesSubID>
  {
  }

  public abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultExpenseAccountID>
  {
  }

  public abstract class defaultExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultExpenseSubID>
  {
  }

  public abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTask.defaultAccrualAccountID>
  {
  }

  public abstract class defaultAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultAccrualSubID>
  {
  }

  public abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.defaultBranchID>
  {
  }
}
