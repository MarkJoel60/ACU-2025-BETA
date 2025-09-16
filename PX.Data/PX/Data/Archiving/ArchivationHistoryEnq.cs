// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.ArchivationHistoryEnq
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Archiving.DAC;
using PX.Data.BQL;
using System;
using System.Linq;

#nullable enable
namespace PX.Data.Archiving;

public class ArchivationHistoryEnq : PXGraph<
#nullable disable
ArchivationHistoryEnq>
{
  public const string AllArchivedField = "AllArchived";
  public const string ArchivedSuffix = "Archived";
  public PXCancel<ArchivationHistoryEnq.Filter> Cancel;
  public PXFilter<ArchivationHistoryEnq.Filter> Header;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<ArchivationHistoryEnq.ArchivingExecution, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  ArchivationHistoryEnq.ArchivingExecution.executionDate, 
  #nullable disable
  GreaterEqual<BqlField<
  #nullable enable
  ArchivationHistoryEnq.Filter.startDate, IBqlInt>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  ArchivationHistoryEnq.Filter.startDate>, IBqlInt>.IsNull>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  ArchivationHistoryEnq.ArchivingExecution.executionDate, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  ArchivationHistoryEnq.Filter.endDate, IBqlInt>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  ArchivationHistoryEnq.Filter.endDate>, IBqlInt>.IsNull>>>, 
  #nullable disable
  OrderBy<Desc<ArchivationHistoryEnq.ArchivingExecution.executionDate>>> ArchivingExecutions;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<ArchivationHistoryEnq.ArchivedDate, Where<BqlOperand<
  #nullable enable
  ArchivationHistoryEnq.ArchivedDate.executionDate, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ArchivationHistoryEnq.ArchivingExecution.executionDate, IBqlInt>.FromCurrent>>, 
  #nullable disable
  OrderBy<Desc<ArchivationHistoryEnq.ArchivedDate.dateToArchive>>> ArchivedDates;

  public ArchivationHistoryEnq()
  {
    this.AppendDocumentCounterFields();
    this.AppendExecutionTimeFieldFor<ArchivationHistoryEnq.ArchivingExecution.executionTimeInSeconds>();
    this.AppendExecutionTimeFieldFor<ArchivationHistoryEnq.ArchivedDate.executionTimeInSeconds>();
  }

  protected virtual void AppendDocumentCounterFields()
  {
    ArchiveInfoHelper slot1 = ArchiveInfoHelper.Instance;
    foreach (ArchivalPolicy policy1 in slot1.GetPolicies())
    {
      ArchivalPolicy policy = policy1;
      string fieldName = policy.TableName + "Archived";
      this.ArchivedDates.Cache.Fields.Add(fieldName);
      this.FieldSelecting.AddHandler(typeof (ArchivationHistoryEnq.ArchivedDate), fieldName, (PXFieldSelecting) ((c, e) => DocumentArchivedFieldSelecting(c, e, fieldName, policy.TableName, policy.TypeName)));
    }

    static PXFieldState MakeIntState(int value, string fieldName, string displayName)
    {
      PXFieldState instance = PXIntState.CreateInstance((object) value, fieldName, new bool?(false), new int?(), new int?(), new int?(), (int[]) null, (string[]) null, typeof (int), new int?(), (string[]) null);
      instance.Enabled = false;
      instance.Visibility = PXUIVisibility.Dynamic;
      instance.DisplayName = PXMessages.LocalizeNoPrefix(displayName);
      return instance;
    }
    ArchivationHistoryEnq archivationHistoryEnq;
    ArchiveInfoHelper slot2;

    void DocumentArchivedFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      string fieldName,
      string tableName,
      string dacFullName)
    {
      int documentsArchived = !(e.Row is ArchivationHistoryEnq.ArchivedDate row) || !row.DateToArchive.HasValue ? 0 : archivationHistoryEnq.GetDocumentsArchived(row, tableName);
      e.ReturnState = (object) MakeIntState(documentsArchived, fieldName, slot2.GetCacheName(sender.Graph, dacFullName));
      e.ReturnValue = (object) documentsArchived;
    }
  }

  protected virtual int GetDocumentsArchived(
    ArchivationHistoryEnq.ArchivedDate date,
    string tableName)
  {
    return PXSelectBase<ArchivedDocumentBatchByDate, PXSelect<ArchivedDocumentBatchByDate, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<ArchivedDocumentBatchByDate.dateToArchive, Equal<BqlField<ArchivationHistoryEnq.ArchivedDate.dateToArchive, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<ArchivedDocumentBatchByDate.executionDate, IBqlInt>.IsEqual<BqlField<ArchivationHistoryEnq.ArchivedDate.executionDate, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) date
    }).FirstTableItems.Where<ArchivedDocumentBatchByDate>((Func<ArchivedDocumentBatchByDate, bool>) (a => a.TableName == tableName)).Select<ArchivedDocumentBatchByDate, int?>((Func<ArchivedDocumentBatchByDate, int?>) (a => a.ArchivedRowsCount)).FirstOrDefault<int?>().GetValueOrDefault();
  }

  protected virtual void AppendExecutionTimeFieldFor<TExecutionTimeInSeconds>() where TExecutionTimeInSeconds : IBqlField
  {
    System.Type itemType = BqlCommand.GetItemType<TExecutionTimeInSeconds>();
    this.Caches[itemType].Fields.Add("ExecutionTime");
    this.FieldSelecting.AddHandler(itemType, "ExecutionTime", (PXFieldSelecting) ((c, e) =>
    {
      string str = (string) null;
      if (e.Row != null && c.GetValue<TExecutionTimeInSeconds>(e.Row) is int num2)
      {
        TimeSpan timeSpan = TimeSpan.FromSeconds((double) num2);
        str = ((int) timeSpan.TotalHours).ToString() + timeSpan.ToString("\\:mm\\:ss");
      }
      PXFieldState instance = PXStringState.CreateInstance((object) str, new int?(), new bool?(true), "ExecutionTime", new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
      instance.Enabled = false;
      instance.DisplayName = ((PXFieldState) c.GetStateExt<TExecutionTimeInSeconds>(e.Row)).DisplayName;
      e.ReturnState = (object) instance;
      e.ReturnValue = (object) str;
    }));
  }

  [PXHidden]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    [PXUIField(DisplayName = "From")]
    public virtual System.DateTime? StartDate { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "To")]
    public virtual System.DateTime? EndDate { get; set; }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.Filter.startDate>
    {
    }

    public abstract class endDate : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivationHistoryEnq.Filter.endDate>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select4<ArchivedDocumentBatchByDate, Aggregate<GroupBy<ArchivedDocumentBatchByDate.executionDate, Sum<ArchivedDocumentBatchByDate.executionTimeInSeconds, Sum<ArchivedDocumentBatchByDate.archivedRowsCount>>>>>), Persistent = false)]
  public class ArchivingExecution : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true, PreserveTime = true, BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "Execution Date")]
    public virtual System.DateTime? ExecutionDate { get; set; }

    [PXDBInt(BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "Duration")]
    public virtual int? ExecutionTimeInSeconds { get; set; }

    [PXDBInt(BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "All Documents")]
    public virtual int? ArchivedRowsCount { get; set; }

    [PXDBCreatedByID(DisplayName = "Executed By", Visible = true, BqlTable = typeof (ArchivedDocumentBatchByDate))]
    public virtual Guid? CreatedByID { get; set; }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivingExecution.executionDate>
    {
    }

    public abstract class executionTimeInSeconds : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivingExecution.executionTimeInSeconds>
    {
    }

    public abstract class archivedRowsCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivingExecution.archivedRowsCount>
    {
    }

    public abstract class createdByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivingExecution.createdByID>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select4<ArchivedDocumentBatchByDate, Aggregate<GroupBy<ArchivedDocumentBatchByDate.dateToArchive, GroupBy<ArchivedDocumentBatchByDate.executionDate, Sum<ArchivedDocumentBatchByDate.executionTimeInSeconds, Sum<ArchivedDocumentBatchByDate.archivedRowsCount>>>>>>), Persistent = false)]
  public class ArchivedDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true, BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "Ready-to-Archive Date")]
    public virtual System.DateTime? DateToArchive { get; set; }

    [PXDBDate(IsKey = true, PreserveTime = true, BqlTable = typeof (ArchivedDocumentBatchByDate))]
    public virtual System.DateTime? ExecutionDate { get; set; }

    [PXDBInt(BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "Duration")]
    public virtual int? ExecutionTimeInSeconds { get; set; }

    [PXDBInt(BqlTable = typeof (ArchivedDocumentBatchByDate))]
    [PXUIField(DisplayName = "All Documents")]
    public virtual int? ArchivedRowsCount { get; set; }

    public abstract class dateToArchive : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivedDate.dateToArchive>
    {
    }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivedDate.executionDate>
    {
    }

    public abstract class executionTimeInSeconds : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivedDate.executionTimeInSeconds>
    {
    }

    public abstract class archivedRowsCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchivationHistoryEnq.ArchivedDate.archivedRowsCount>
    {
    }
  }
}
