// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.PXReferentialIntegrityCheckAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes.Handlers;
using PX.Data.ReferentialIntegrity.Merging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Passively includes targeted <see cref="T:PX.Data.IBqlTable" /> in referential integrity check without creating any <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s.<para />
/// <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s could be declared explicitly by using <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXForeignReferenceAttribute" /> (on child side).<para />
/// Certain rows could be excluded from referential integrity check by using <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute" />.<para />
/// In case when only parent-<see cref="T:PX.Data.IBqlTable" /> is included in referential integrity check,
/// only children presence checking will be performed on parent-row deleting.<para />
/// In case when only child-<see cref="T:PX.Data.IBqlTable" /> is included in referential integrity check,
/// only parent presence checking will be performed on child-row inserting.<para />
/// In case when both child-<see cref="T:PX.Data.IBqlTable" /> and parent-<see cref="T:PX.Data.IBqlTable" /> are included in referential integrity check,
/// both checks will be performed on corresponding events.<para />
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public class PXReferentialIntegrityCheckAttribute : 
  PXEventSubscriberAttribute,
  IPXRowDeletingSubscriber,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber
{
  private System.Type _rowsExcludingCondition;

  void IPXRowDeletingSubscriber.RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    this.HandleChildrenRows(sender, e.Row, false);
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.HandleParentRows(sender, e.Row, false);
  }

  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.HandleParentRows(sender, e.Row, false);
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    this.HandleRowPersisting(sender, e.Row, e.Operation);
  }

  public PXReferentialIntegrityCheckAttribute()
    : this(ReferentialIntegrityRole.AsParent)
  {
  }

  /// <summary>
  /// Instantiate new instance of <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXReferentialIntegrityCheckAttribute" />
  /// </summary>
  /// <param name="referentialIntegrityRole">Indicates <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ReferentialIntegrityRole" /> of targeted <see cref="T:PX.Data.IBqlTable" /></param>
  internal PXReferentialIntegrityCheckAttribute(ReferentialIntegrityRole referentialIntegrityRole)
  {
    this.ReferentialIntegrityRole = referentialIntegrityRole;
    this.SetDefaultCheckPointFor(referentialIntegrityRole);
  }

  private void SetDefaultCheckPointFor(ReferentialIntegrityRole referentialIntegrityRole)
  {
    switch (referentialIntegrityRole)
    {
      case ReferentialIntegrityRole.AsParent:
        this.CheckPoint = CheckPoint.BeforePersisting;
        break;
      case ReferentialIntegrityRole.AsChild:
        this.CheckPoint = CheckPoint.OnPersisting;
        break;
      case ReferentialIntegrityRole.Full:
        this.CheckPoint = CheckPoint.Both;
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof (referentialIntegrityRole), (object) referentialIntegrityRole, (string) null);
    }
  }

  public void SetDefaultCheckPoint() => this.SetDefaultCheckPointFor(this.ReferentialIntegrityRole);

  [InjectDependencyOnTypeLevel]
  public ITableMergedReferencesInspector TableMergedReferencesInspector { get; set; }

  /// <summary>
  /// Indicates <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ReferentialIntegrityRole" /> of targeted <see cref="T:PX.Data.IBqlTable" />
  /// </summary>
  internal ReferentialIntegrityRole ReferentialIntegrityRole { get; }

  /// <summary>
  /// Indicates on which events of current entity system should check referential integrity.
  /// Default value for entities in a parent role is <see cref="F:PX.Data.ReferentialIntegrity.Attributes.CheckPoint.BeforePersisting" />,
  /// for entities in a child role - <see cref="F:PX.Data.ReferentialIntegrity.Attributes.CheckPoint.OnPersisting" />,
  /// and for entities in both roles - <see cref="F:PX.Data.ReferentialIntegrity.Attributes.CheckPoint.Both" />.
  /// </summary>
  public CheckPoint CheckPoint { get; set; }

  internal bool IsSuspended { get; set; }

  /// <summary>
  /// <see cref="T:PX.Data.IBqlWhere" />-clause or <see cref="T:PX.Data.IBqlSearch" />-clause that indicates whether the row of targeted
  /// <see cref="T:PX.Data.IBqlTable" /> should be excluded from referential integrity check.
  /// Row will be excluded if <see cref="T:PX.Data.IBqlWhere" />-clause is true or <see cref="T:PX.Data.IBqlSearch" />-clause
  /// returns any result
  /// </summary>
  [PXInternalUseOnly]
  public System.Type RowsExcludingCondition
  {
    get => this._rowsExcludingCondition;
    set
    {
      this._rowsExcludingCondition = !(value != (System.Type) null) || typeof (IBqlWhere).IsAssignableFrom(value) || typeof (IBqlSearch).IsAssignableFrom(value) ? value : throw new PXArgumentException(nameof (value), "The assigned type must implement the {0} interface.", new object[1]
      {
        (object) "IBqlWhere or IBqlSearch"
      });
    }
  }

  private void HandleRowPersisting(PXCache cache, object row, PXDBOperation operation)
  {
    if (operation.HasFlag((Enum) PXDBOperation.Delete))
    {
      this.HandleChildrenRows(cache, row, true);
    }
    else
    {
      if (!operation.HasFlag((Enum) PXDBOperation.Insert) && !operation.HasFlag((Enum) PXDBOperation.Update))
        return;
      this.HandleParentRows(cache, row, true);
    }
  }

  private void HandleChildrenRows(PXCache cache, object parentRow, bool persisting)
  {
    if (this.IsSuspended || !this.HandleParentEvents || !this.AppropriateCheckPoint(persisting) || EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(parentRow), PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted) || !cache.IsKeysFilled(parentRow) || this.HasNegativeKeyComponent(cache, parentRow) || this.RowIsExcluded(cache, parentRow))
      return;
    new ParentDeletingHandler(this.TableMergedReferencesInspector, cache, parentRow).Handle();
  }

  private void HandleParentRows(PXCache cache, object childRow, bool persisting)
  {
    if (this.IsSuspended || !this.HandleChildEvents || !this.AppropriateCheckPoint(persisting) || this.RowIsExcluded(cache, childRow))
      return;
    new ChildUpdatingHandler(this.TableMergedReferencesInspector, cache, childRow).Handle();
  }

  private bool HandleParentEvents
  {
    get => this.ReferentialIntegrityRole.HasFlag((Enum) ReferentialIntegrityRole.AsParent);
  }

  private bool HandleChildEvents
  {
    get => this.ReferentialIntegrityRole.HasFlag((Enum) ReferentialIntegrityRole.AsChild);
  }

  private bool HandleBeforePersisting
  {
    get => this.CheckPoint.HasFlag((Enum) CheckPoint.BeforePersisting);
  }

  private bool HandleOnPersisting => this.CheckPoint.HasFlag((Enum) CheckPoint.OnPersisting);

  private bool AppropriateCheckPoint(bool isPersistingPoint)
  {
    if (!isPersistingPoint && this.HandleBeforePersisting)
      return true;
    return isPersistingPoint && this.HandleOnPersisting;
  }

  private bool RowIsExcluded(PXCache cache, object row)
  {
    if (this.RowsExcludingCondition == (System.Type) null)
      return false;
    if (typeof (IBqlSearch).IsAssignableFrom(this.RowsExcludingCondition))
    {
      BqlCommand instance = BqlCommand.CreateInstance(this.RowsExcludingCondition);
      return cache.Graph.TypedViews.GetView(instance, false).SelectSingle() != null;
    }
    IBqlCreator formula = PXFormulaAttribute.InitFormula(BqlCommand.MakeGenericType(typeof (Switch<,>), typeof (Case<,>), this.RowsExcludingCondition, typeof (True), typeof (False)));
    bool? result = new bool?();
    object obj = (object) null;
    BqlFormula.Verify(cache, row, formula, ref result, ref obj);
    bool? nullable = obj as bool?;
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  private bool HasNegativeKeyComponent(PXCache cache, object row)
  {
    if (cache.Identity != null && GetNumber(cache.GetValue(row, cache.Identity)) < 0L)
      return true;
    foreach (object o in cache.Keys.Select<string, object>((Func<string, object>) (k => cache.GetValue(row, k))))
    {
      if (GetNumber(o) < 0L)
        return true;
    }
    return false;

    static long GetNumber(object o)
    {
      switch (o)
      {
        case short number1:
          return (long) number1;
        case int number2:
          return (long) number2;
        case long number3:
          return number3;
        default:
          return 0;
      }
    }
  }

  internal static bool HasReferentialIntegrity(System.Type type, bool checkBaseTypes)
  {
    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
    if (!checkBaseTypes)
      bindingAttr |= BindingFlags.DeclaredOnly;
    return ((IEnumerable<PropertyInfo>) type.GetProperties(bindingAttr)).Any<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (attr => attr.AttributeType == typeof (PXReferentialIntegrityCheckAttribute)))));
  }
}
