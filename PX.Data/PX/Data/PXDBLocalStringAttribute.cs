// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLocalStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Maps a string DAC field to a localized string column in the
/// database.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database columns that have the culture
/// information specified in their names. For example, for the
/// <tt>Description</tt> field, the English-specific column is
/// <tt>DescriptionenGB</tt>, the Russian-specific column is
/// <tt>DescriptionruRU</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBLocalString(255, IsUnicode = true)]
/// [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
/// public virtual String Title { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBLocalStringAttribute : PXDBStringAttribute
{
  protected static Dictionary<string, bool> _FieldForCultureExists = new Dictionary<string, bool>();
  private static ReaderWriterLock rwLock = new ReaderWriterLock();

  /// <summary>Initializes a new instance with the default
  /// parameters.</summary>
  public PXDBLocalStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the specified maximum
  /// length.</summary>
  /// <param name="Length">The maximum length of the field value.</param>
  public PXDBLocalStringAttribute(int Length)
    : base(Length)
  {
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    string str = Thread.CurrentThread.CurrentCulture.Name.Replace("-", "");
    if ("enUS".Equals(str, StringComparison.OrdinalIgnoreCase))
    {
      this.PrepareCommandImpl(this._DatabaseFieldName, e);
    }
    else
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXDBLocalStringAttribute.rwLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        bool flag;
        if (PXDBLocalStringAttribute._FieldForCultureExists.TryGetValue(str, out flag))
        {
          if (flag)
            this.PrepareCommandImpl(this._DatabaseFieldName + str, e);
          else
            this.PrepareCommandImpl(this._DatabaseFieldName, e);
        }
        else
        {
          ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
          if (PXDBLocalStringAttribute._FieldForCultureExists.TryGetValue(str, out flag))
          {
            if (flag)
              this.PrepareCommandImpl(this._DatabaseFieldName + str, e);
            else
              this.PrepareCommandImpl(this._DatabaseFieldName, e);
          }
          else
            this.TryPrepareCommand(e, str);
        }
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }
  }

  private bool TryPrepareCommand(PXCommandPreparingEventArgs e, string culturename)
  {
    try
    {
      using (PXDatabase.SelectSingle(this.BqlTable, new PXDataField(this._DatabaseFieldName + culturename)))
        ;
    }
    catch (Exception ex)
    {
      this.PrepareCommandImpl(this._DatabaseFieldName, e);
      PXDBLocalStringAttribute._FieldForCultureExists.Add(culturename, false);
      return false;
    }
    this.PrepareCommandImpl(this._DatabaseFieldName + culturename, e);
    PXDBLocalStringAttribute._FieldForCultureExists.Add(culturename, true);
    return true;
  }

  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    if (dbFieldName == null || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
      return;
    System.Type type = e.Table;
    if ((object) type == null)
      type = this._BqlTable;
    System.Type dac = type;
    e.Expr = new Column(dbFieldName, dac).Coalesce((SQLExpression) new Column(this._DatabaseFieldName, dac));
  }
}
